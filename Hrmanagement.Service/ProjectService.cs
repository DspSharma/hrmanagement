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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Service
{
    public class ProjectService : IProjectService
    {
        public IUnitOfWork _unitOfWork;
        private readonly HrManagementContext _context;
        public IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, HrManagementContext context, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
        }


        public async Task<ApiResponseModel<ProjectOutput>> AddUpdateProject(ProjectInput value)
        {
            try
            {
                Project formValue = _mapper.Map<Project>(value);
                Project isProjectExists = _unitOfWork.Project.GetWhere(x => x.Id != formValue.Id && (x.Title == formValue.Title && x.Url == formValue.Url)).FirstOrDefault();
                if (isProjectExists != null)
                {
                    return new ApiResponseModel<ProjectOutput>
                    {
                        succeed = false,
                        message = "Project with the same title already exists in the same year"
                    };
                }
                if (formValue.Id != 0)
                {
                    Project existingProject = await _unitOfWork.Project.GetByIdAsync(formValue.Id);
                    if (existingProject == null)
                    {
                        throw new Exception($"Project with ID {formValue.Id} was not found.");
                    }
                    // Update properties
                    existingProject.Title = formValue.Title;
                    existingProject.Description = formValue.Description;
                    existingProject.Url = formValue.Url;
                    existingProject.Status = formValue.Status;
                    existingProject.IsActive = formValue.IsActive;
                    existingProject.UpdatedAt = DateTime.UtcNow;
                   // await _unitOfWork.SaveAsync();
                }
                else
                {
                    formValue.IsActive = false;
                    formValue.CreatedAt = DateTime.UtcNow;
                    await _unitOfWork.Project.AddAsync(formValue);
                  
                }

                await _unitOfWork.SaveAsync();
                ProjectOutput projectOutputs = _mapper.Map<ProjectOutput>(formValue);
                return new ApiResponseModel<ProjectOutput>
                {
                    succeed = true,
                    message = "Success",
                    data = projectOutputs
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<List<ProjectOutput>>> GetAllProject()
        {
            try
            {
                List<Project> project = (await _unitOfWork.Project.GetAllAsync()).ToList();
                var projectList = _mapper.Map<List<ProjectOutput>>(project);
                return new ApiResponseModel<List<ProjectOutput>>
                {
                    succeed = true,
                    message = "success",
                    data = projectList,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<ProjectOutput>> GetProjectByid(int id)
        {
            try
            {
                Project project = await _unitOfWork.Project.GetByIdAsync(id);
                if (project == null)
                {
                    throw new Exception($"project was not found.");
                }
                var projectList = _mapper.Map<ProjectOutput>(project);
                return new ApiResponseModel<ProjectOutput>
                {
                    succeed = true,
                    message = "success",
                    data = projectList
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<bool>> DeleteProjectByid(int id)
        {
            try
            {
                Project project = await _unitOfWork.Project.GetByIdAsync(id);
                if (project == null)
                {
                    throw new Exception($"project was not found.");
                }
                _unitOfWork.Project.Remove(project);
                await _unitOfWork.SaveAsync();
                return new ApiResponseModel<bool>
                {
                    succeed = true,
                    message = "success",

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
                Project project = await _unitOfWork.Project.GetByIdAsync(id);
                if (project == null)
                {
                    throw new Exception($"project was not found.");
                }
                project.IsActive = !project.IsActive;
                await _unitOfWork.SaveAsync();
                return new ApiResponseModel<bool>
                {
                    succeed = true,
                    message = "success",
                    data = project.IsActive

                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
