using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [BaseController]
    public class ProjectController : Controller
    {
        public IMvcProjectServices _projectService;
        public ProjectController(IMvcProjectServices projectService)
        {
            _projectService = projectService;
        }

        public async Task<IActionResult>AddUpdateProjectForm(int id)
        {
            if(!ModelState.IsValid)
            {
                return View("~/Areas/Admin/Views/Project/AddUpdateProjectForm.cshtml");
            }
            else
            {
                if(id !=0)
                {
                    var rslt = await _projectService.editProject(id);
                    ViewData["project"] = rslt.data;
                    return View("~/Areas/Admin/Views/Project/AddUpdateProjectForm.cshtml");
                }
                else
                {
                    return View("~/Areas/Admin/Views/Project/AddUpdateProjectForm.cshtml");
                }
            }
        }

        public async Task<IActionResult> AddUpdateProject([FromForm] ProjectInput model)
        {
            var rslt = await _projectService.addUpdateProject(model);
            if (rslt.succeed)
            {
                TempData["Success"] = $"{rslt.message}";
                return RedirectToAction("ProjectList", "Project", new { area = "Admin" });
            }
            else
            {
                TempData["error"] = $"{rslt.message}";
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
        }
        public async Task<IActionResult>ProjectList([FromQuery] bool isActive = true)
        {
            var rest = await _projectService.projectList();
            ViewData["projectData"] = rest.data;
            ViewData["isActive"] = isActive;
            return View("~/Areas/Admin/Views/Project/ProjectList.cshtml");
        }

        public async Task<IActionResult> deleteByIdProject(int id)
        {
            var rslt = await _projectService.deleteByIdProject(id);
            return Redirect(HttpContext.Request.Headers["Referer"]);
        }
        public async Task<IActionResult> ActiveInActive(int id)
        {
            var rslt = await _projectService.activeInActiveHoliday(id);
            return Redirect(HttpContext.Request.Headers["Referer"]);
        }


    }
}
