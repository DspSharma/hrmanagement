using Hrmanagement.Core.Constants;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Services.Interfaces;
using Newtonsoft.Json;

namespace Hrmanagement.Services
{
    public class MvcProjectServices : IMvcProjectServices
    {
        public IApiServices _apiServices;

        public MvcProjectServices(IApiServices apiServices)
        {
            _apiServices = apiServices;
        }
        public async Task<ApiResponseModel<ProjectOutput>> addUpdateProject(ProjectInput value)
        {
            var postApi = ProjectEndPoint.projectAddUpdate;
            var response = await _apiServices.postApi(postApi, value);
            ApiResponseModel<ProjectOutput> rslt = JsonConvert.DeserializeObject<ApiResponseModel<ProjectOutput>>(response);
            return rslt;
        }
        public async Task<ApiResponseModel<List<ProjectOutput>>> projectList()
        {
            var getApi = ProjectEndPoint.projectGetAll;
            var response = await _apiServices.getApiWeb(getApi);
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<List<ProjectOutput>> projectData = JsonConvert.DeserializeObject<ApiResponseModel<List<ProjectOutput>>>(rslt);
            return new ApiResponseModel<List<ProjectOutput>>
            {
                succeed = true,
                message = "success",
                data = projectData.data,
            };
        }
        public async Task<ApiResponseModel<ProjectOutput>> editProject(int id)
        {
            var getByIdApi = ProjectEndPoint.projectGetById;
            var response = await _apiServices.getApiWeb($"{getByIdApi}/{id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<ProjectOutput> project = JsonConvert.DeserializeObject<ApiResponseModel<ProjectOutput>>(rslt);
            return new ApiResponseModel<ProjectOutput>
            {
                succeed = true,
                message = "success",
                data = project.data
            };
        }
        public async Task<ApiResponseModel<bool>> deleteByIdProject(int id)
        {
            var deleteApi = ProjectEndPoint.projectDeleteById;
            var response = await _apiServices.deleteApi($"{deleteApi}/{id}");
            return new ApiResponseModel<bool>
            {
                succeed = true,
                message = "success",
                data = true
            };
        }
        public async Task<ApiResponseModel<bool>> activeInActiveHoliday(int id)
        {
            var activeInActiveApi = ProjectEndPoint.projectActiveInActive;
            var response = await _apiServices.putApi($"{activeInActiveApi}/{id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<bool> stateId = JsonConvert.DeserializeObject<ApiResponseModel<bool>>(rslt);
            return new ApiResponseModel<bool>
            {
                succeed = true,
                message = "success",
                data = stateId.data
            };
        }

    }
}
