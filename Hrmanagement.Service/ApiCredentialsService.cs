using AutoMapper;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Data.DBContext;
using Hrmanagement.Data.Entities;
using Hrmanagement.Data.UnitOfWork;
using Hrmanagement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Service
{
    public class ApiCredentialsService : IApiCredentialsService
    {
        public IUnitOfWork _unitOfWork;
        private readonly HrManagementContext _context;
        public IMapper _mapper;

        public ApiCredentialsService(HrManagementContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<ApiResponseModel<ApiCredentialsOutput>> AddUpdateApiCredential(ApiCredentialsInput value)
        {
            try
            {
                ApiCredentials formvalue = _mapper.Map<ApiCredentials>(value);
                if (formvalue.Id != 0)
                {
                    ApiCredentials apiCredentials = await _unitOfWork.ApiCredentials.GetByIdAsync(value.Id);
                    if (apiCredentials == null)
                        return new ApiResponseModel<ApiCredentialsOutput>
                        {
                            succeed = false,
                            message = "ApiCredentials was not found for updating."
                        };

                    apiCredentials.ProjectId = value.ProjectId;
                    apiCredentials.ApiHost = value.ApiHost;
                    apiCredentials.Service = value.Service;
                    apiCredentials.Password = value.Password;
                    apiCredentials.Description = value.Description;
                    apiCredentials.ApiKey = value.ApiKey;
                    apiCredentials.ClientId = value.ClientId;
                    apiCredentials.ClientSecret = value.ClientSecret;
                    apiCredentials.ConsumedLimit = value.ConsumedLimit;
                    apiCredentials.AllowLimit = value.AllowLimit;
                    apiCredentials.Status = value.Status;
                    apiCredentials.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.SaveAsync();
                }
                else
                {
                    formvalue.CreatedAt = DateTime.UtcNow;
                    formvalue.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.ApiCredentials.AddAsync(formvalue);
                    await _unitOfWork.SaveAsync();

                }
                ApiCredentialsOutput result = _mapper.Map<ApiCredentialsOutput>(formvalue);
                return new ApiResponseModel<ApiCredentialsOutput>()
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

        public async Task<ApiResponseModel<List<ApiCredentialsOutput>>> GetAllApiCredential()
        {
            try
            {
                List<ApiCredentials> apiCredentials = (await _unitOfWork.ApiCredentials.GetAllAsync()).ToList();
                List<int> listProjectIds = apiCredentials.Select(x => x.ProjectId).Distinct().ToList();
                List<Project> projects = _unitOfWork.Project.GetWhere(x => listProjectIds.Contains(x.Id)).ToList();
                var apiCredentialslists = from ap in apiCredentials
                                         join p in projects on ap.ProjectId equals p.Id
                                         select new ApiCredentialsOutput
                                         {
                                             Id = ap.Id,
                                             ProjectId = p.Id,
                                             ProjectName = p.Title,
                                             Service = ap.Service,
                                             Description = ap.Description,
                                             ApiHost = ap.ApiHost,
                                             ApiKey = ap.ApiKey,
                                             ClientId = ap.ClientId,
                                             ClientSecret = ap.ClientSecret,
                                             Password = ap.Password,
                                             AllowLimit = ap.AllowLimit,
                                             ConsumedLimit = ap.ConsumedLimit,
                                             Status = ap.Status,
                                             CreatedAt = ap.CreatedAt,
                                             UpdatedAt = ap.UpdatedAt,
                                         };
                var result = _mapper.Map<List<ApiCredentialsOutput>>(apiCredentialslists);
                return new ApiResponseModel<List<ApiCredentialsOutput>>
                {
                    succeed = true,
                    message = "Successfully retrieved  All Credentials.",
                    data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseModel<List<ApiCredentialsOutput>>
                {
                    succeed = false,
                    message = $"Error retrieving API credentials: {ex.Message}",
                    data = null
                };
            }
        }

        public async Task<ApiResponseModel<bool>> DeleteById(int id)
        {
            try
            {
                ApiCredentials apiCredentials = await _unitOfWork.ApiCredentials.GetByIdAsync(id);
                if (apiCredentials == null)
                {
                    throw new Exception("Credentials not found for deletion.");
                }
                _unitOfWork.ApiCredentials.Remove(apiCredentials);
                await _unitOfWork.SaveAsync();
                return new ApiResponseModel<bool>
                {
                    succeed = true,
                    message = "Credentials successfully deleted.",
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseModel<bool>
                {
                    succeed = false,
                    message = $"Error deleting credentials: {ex.Message}",
                };
            }

        }


        public async Task<ApiResponseModel<ApiCredentialsOutput>> getApiCredentiaById(int id)
        {
            try
            {
                ApiCredentials apiCredentials = await _unitOfWork.ApiCredentials.GetByIdAsync(id);
                if (apiCredentials == null)
                {
                    throw new Exception($"Api Credentials was not found.");
                }
                var apiCredentialsList = _mapper.Map<ApiCredentialsOutput>(apiCredentials);
                return new ApiResponseModel<ApiCredentialsOutput>
                {
                    succeed = true,
                    message = "success",
                    data = apiCredentialsList
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
