using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Service.Interfaces
{
    public interface IProjectService
    {
        Task<ApiResponseModel<ProjectOutput>> AddUpdateProject(ProjectInput value);
        Task<ApiResponseModel<List<ProjectOutput>>> GetAllProject();
        Task<ApiResponseModel<ProjectOutput>> GetProjectByid(int id);
        Task<ApiResponseModel<bool>> DeleteProjectByid(int id);
        Task<ApiResponseModel<bool>> ActiveInActive(int id);
    }
}
