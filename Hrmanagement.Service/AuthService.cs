using AutoMapper;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Misc;
using Hrmanagement.Core.Models;
using Hrmanagement.Data.Entities;
using Hrmanagement.Data.UnitOfWork;
using Hrmanagement.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Service
{
    public class AuthService : IAuthService
    {

        public IMapper _mapper;
        public IConfiguration _Configuration { get; }
        private IUnitOfWork _unitOfWork;
        public IEmailService _emailService;

        public AuthService(IMapper mapper, IConfiguration configuration, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _mapper = mapper;
            _Configuration = configuration;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<ApiResponseModel<LoggedInUserModel>> login(UserLoginInput model)
        {
            User? userData = _unitOfWork.User.GetWhere(x => x.Email == model.Email && x.Password == model.Password && x.IsActive).FirstOrDefault();
            if (userData == null)
            {
                return new ApiResponseModel<LoggedInUserModel>
                {
                    succeed = false,
                    message = "No user found with these credentials",
                };
            }

            // Map user data to LoggedInUserModel
            LoggedInUserModel user = _mapper.Map<LoggedInUserModel>(userData);

            // Generate JWT token
            string token = await BuildAdminToken(user);
            user.Token = token;

            var rslt = new ApiResponseModel<LoggedInUserModel>
            {
                succeed = true,
                message = "Login successful.",
                data = user
            };
            return rslt;
        }
       
        public async Task<string> BuildAdminToken(LoggedInUserModel user)
        {
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration.GetSection("Jwt:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // Create JWT claims
            Claim[] claims = new Claim[]
            {
        new Claim("id", user.Id.ToString()),
        new Claim("name", user.FirstName),
        new Claim(ClaimTypes.Email, user.Email ?? ""),
        new Claim("mobile", user.Mobile ?? ""),
        new Claim(ClaimTypes.Role, user.Role ?? ""),
         new Claim(ClaimTypes.NameIdentifier,user.FirstName),

            };

            int expireInMinutes = Convert.ToInt32(_Configuration.GetSection("Jwt:TokenValidity").Value);

            // Create JWT token
            var token = new JwtSecurityToken(
                issuer: _Configuration.GetSection("Jwt:Issuer").Value,
                audience: _Configuration.GetSection("Jwt:Audience").Value,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(expireInMinutes),
                signingCredentials: creds
            );

            // Return JWT token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public async Task<ApiResponseModel<bool>> forgotPassword(ForgotPasswordInput model)
        {
            var user = _unitOfWork.User.GetWhere(x => x.Email == model.Email).FirstOrDefault();
            if (user == null)
            {
                return new ApiResponseModel<bool>
                {
                    succeed = false,
                    message = "Your Email Not Registered!"
                };
            }
            else
            {
                var email = user.Email;
                var chars = "0123456789";
                var random = new Random();
                var result = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
                var password = result.ToString();
                DateTime currentTime = DateTime.Now;
                DateTime expireTime = currentTime.AddMinutes(5);
                user.TokenExpiredOn = expireTime;
                user.PasswordResetToken = password;
                var ResetTkenPassword = user.PasswordResetToken;
                await _unitOfWork.SaveAsync();
                string subject = "We have received a request to reset your password";
                //string subject = "We are so Thankful for YOU";
                var forgot = "ForgotPassword";
                string Message = $"<html><body><a href='http://hrm.tagosys.com/Admin/Auth/resetPassword/?Token={ResetTkenPassword}&ToEmail={email}'>Click here to view the page {forgot}</a></body></html>";
                //string Message = $"<html><body><a href='http://192.168.0.32:5001/Auth/resetPassword/?Token={ResetTkenPassword}&ToEmail={email}'>Click here to view the page {forgot}</a></body></html>";
                string toEmail = user.Email;
                var rslt = _emailService.SendEmail(subject, Message, toEmail);
            }
            return new ApiResponseModel<bool>
            {
                succeed = true,
                message = "success",
            };
        }

        public async Task<ApiResponseModel<ResetPasswordOutput>> resetPassword(string Token, string toEmail)
        {
            var resetPassword = _unitOfWork.User.GetWhere(x => x.PasswordResetToken == Token && x.Email == toEmail).FirstOrDefault();
            ResetPasswordOutput PassordReset = new ResetPasswordOutput();
            if (resetPassword != null)
            {
                var resetTokenPassword = resetPassword.TokenExpiredOn;
                DateTime curentTime = DateTime.Now;
                if (resetTokenPassword < curentTime)
                {
                    return new ApiResponseModel<ResetPasswordOutput>
                    {
                        succeed = false,
                        message = "Your  email Link Expire",
                    };
                }
                else
                {
                    PassordReset.Token = resetPassword.PasswordResetToken;
                    PassordReset.ToEmail = resetPassword.Email;
                }
            }
            if (resetPassword == null)
            {
                return new ApiResponseModel<ResetPasswordOutput>
                {
                    succeed = false,
                    message = "Your Token or email not matched",
                };
            }
            else
            {
                PassordReset.Token = resetPassword.PasswordResetToken;
                PassordReset.ToEmail = resetPassword.Email;
            }

            var rslt = _mapper.Map<ResetPasswordOutput>(PassordReset);

            return new ApiResponseModel<ResetPasswordOutput>
            {
                succeed = true,
                message = "success",
                data = rslt
            };

        }

        public async Task<ApiResponseModel<ResetPasswordOutput>> resetPasswordUpdate(ResetPasswordInput value)
        {
            var resetPassword = _unitOfWork.User.GetWhere(x => x.PasswordResetToken == value.Token && x.Email == value.ToEmail).FirstOrDefault();
            if (resetPassword != null)
            {
                var resetTokenPassword = resetPassword.TokenExpiredOn;
                DateTime curentTime = DateTime.Now;
                if (resetTokenPassword < curentTime)
                {
                    return new ApiResponseModel<ResetPasswordOutput>
                    {
                        succeed = false,
                        message = "Your  email Link Expire",
                    };
                }
                else if (value.Newpassword == value.Confirmpassword)
                {
                    resetPassword.PasswordResetToken = null;
                    resetPassword.Password = value.Newpassword;
                }
            }

            else
            {
                return new ApiResponseModel<ResetPasswordOutput>
                {
                    succeed = false,
                    message = "Your Token or email not matched"
                };
            }
            await _unitOfWork.SaveAsync();

            return new ApiResponseModel<ResetPasswordOutput>
            {
                succeed = true,
                message = "success",
            };
        }

    }
}
