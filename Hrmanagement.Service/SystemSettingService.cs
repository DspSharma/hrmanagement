using AutoMapper;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Data.Entities;
using Hrmanagement.Data.UnitOfWork;
using Hrmanagement.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Service
{
    public class SystemSettingService : ISystemSettingService
    {

        public IMapper _mapper;
        public IWebHostEnvironment _environment;
        public IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
      //  public string LocalImagEndPoint { get; set; }

        public SystemSettingService(IMapper mapper, IWebHostEnvironment environment, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _mapper = mapper;
            _environment = environment;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        

        public async Task<ApiResponseModel<SystemSettingOutput>> AddUpdateSystemSetting(SystemSettingInput value)
        {
            SystemSettingOutput systemSettingOutputs;
            
            SystemSetting formvalue = _mapper.Map<SystemSetting>(value);

            if (formvalue.Id != 0)
            {
                SystemSetting isKeyExists = _unitOfWork.SystemSetting.GetWhere(x => x.Id != formvalue.Id && x.Key == formvalue.Key).FirstOrDefault();
                if (isKeyExists != null)
                {
                    return new ApiResponseModel<SystemSettingOutput>
                    {
                        succeed = false,
                        message = $"{value.Key}  Already Exists."
                    };

                }
                SystemSetting systemSetting = await _unitOfWork.SystemSetting.GetByIdAsync(value.Id);
                if (systemSetting == null)
                    throw new Exception($"SystemSetting was not Found.");

                systemSetting.Key = value.Key;
                systemSetting.Value = value.Value;
                systemSetting.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.SaveAsync();
            }
            else
            {
                SystemSetting isKeyExists = _unitOfWork.SystemSetting.GetWhere(x => x.Id != formvalue.Id && x.Key == formvalue.Key).FirstOrDefault();
                if (isKeyExists != null)
                {
                    return new ApiResponseModel<SystemSettingOutput>
                    {
                        succeed = false,
                        message = $"{value.Key}  Already Exists."
                    };
                }

                formvalue.IsActive = value.IsActive;
                formvalue.CreatedAt = DateTime.UtcNow;
                await _unitOfWork.SystemSetting.AddAsync(formvalue);
                await _unitOfWork.SaveAsync();
            }
            systemSettingOutputs = _mapper.Map<SystemSettingOutput>(formvalue);
            return new ApiResponseModel<SystemSettingOutput>
            {
                succeed = true,
                message = "Success",
                data = systemSettingOutputs
            };
        }

        public async Task<ApiResponseModel<bool>> DeleteSystemSettingById(int id)
        {
            try
            {
                SystemSetting systemSettings = await _unitOfWork.SystemSetting.GetByIdAsync(id);
                if (systemSettings == null)
                    throw new Exception("not Found.");
                _unitOfWork.SystemSetting.Remove(systemSettings);
                await _unitOfWork.SaveAsync();
                return new ApiResponseModel<bool>
                {
                    succeed = true,
                    message = "Success"
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<List<SystemSettingOutput>>> GetAllSystemSetting()
        {
            try
            {
                List <SystemSetting> systemSettings = (await _unitOfWork.SystemSetting.GetAllAsync()).ToList();
                var systemSettingOutputs = _mapper.Map<List<SystemSettingOutput>>(systemSettings);
                return new ApiResponseModel<List<SystemSettingOutput>>
                {
                    succeed = true,
                    message = "Success",
                    data = systemSettingOutputs,
                    
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseModel<List<SystemSettingOutput>>
                {
                    succeed = false,
                    message = $"Error: {ex.Message}",
                    data = null

                };
            }
        }

        public async Task<ApiResponseModel<SystemSettingOutput>> GetSystemSettingById(int id)
        {
            try
            {
                SystemSetting systemSetting = await _unitOfWork.SystemSetting.GetByIdAsync(id);
                if (systemSetting == null)
                {
                    return new ApiResponseModel<SystemSettingOutput>
                    {
                        succeed = false,
                        message = $"system Setting record not found for user"

                    };
                }
                var result = _mapper.Map<SystemSettingOutput>(systemSetting);
                return new ApiResponseModel<SystemSettingOutput>
                {
                    succeed = true,
                    message = "success",
                    data = result
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



        public async Task<ApiResponseModel<bool>> ActiveInActive(int id)
        {
            try
            {
                SystemSetting systemSettings = await _unitOfWork.SystemSetting.GetByIdAsync(id);
                if (systemSettings == null)
                    throw new Exception("not Found.");
                if (systemSettings.IsActive == false)
                {
                    systemSettings.IsActive = true;
                }
                else
                {
                    systemSettings.IsActive = false;
                }

                await _unitOfWork.SaveAsync();
                return new ApiResponseModel<bool>
                {
                    succeed = true,
                    message = "Success"
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //public async Task<ImageEndpointModel> getLocalEndpoint()
        //{
        //    var setting = (await _unitOfWork.SystemSetting.GetAllAsync()).FirstOrDefault();
        //    return new ImageEndpointModel()
        //    {
        //        LocalImagEndPoint = LocalImagEndPoint,

        //    };

        //}
        public async Task<ImageEndpointModel> getLocalEndpoint()
        {
            var setting = (await _unitOfWork.SystemSetting.GetAllAsync()).FirstOrDefault();

            // Get the LocalImagEndPoint value from appsettings.json
            var localImageEndPoint = _configuration["ImageSettings:LocalImagEndPoint"];

            return new ImageEndpointModel()
            {
                LocalImagEndPoint = localImageEndPoint
            };
        }

    }
}
