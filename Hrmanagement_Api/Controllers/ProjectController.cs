using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.Misc;
using Hrmanagement.Service;
using Hrmanagement.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        public IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        private object? Json(string message)
        {
            throw new NotImplementedException();
        }

        [HttpPost("AddUpdateProject")]
        public async Task<IActionResult> addUpdateProject([FromBody] ProjectInput model)
        {
            try
            {
               //var u = MiscMethods.getLoginDetailByToken(HttpContext);
                // if (model.Id == 0)
                //model.Id = u.Id;
                var res = await _projectService.AddUpdateProject(model);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }
        }

        [HttpGet("GetAllProject")]
        public async Task<IActionResult>getAllProject()
        {
            try
            {
                var res = await _projectService.GetAllProject();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult>getProjectById(int id)
        {
            try
            {
                var res = await _projectService.GetProjectByid(id);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }
        }

        [HttpDelete("Get/{id}")]
        public async Task<IActionResult>deleteProjectById(int id)
        {
            try
            {
                var res = await _projectService.DeleteProjectByid(id);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }
        }

        [HttpPut("ActiveInActive/{id}")]
        public async Task<IActionResult> ActiveInActive(int id)
        {
            try
            {
                var res = await _projectService.ActiveInActive(id);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }


    }
}
