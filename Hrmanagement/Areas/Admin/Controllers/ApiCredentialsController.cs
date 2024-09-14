using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [BaseController]
    public class ApiCredentialsController : Controller
    {
        public IMvcApiCredentialsServices _apiCredentialServices;
        public IMvcProjectServices _projectServices;
        public ApiCredentialsController(IMvcApiCredentialsServices apiCredentialServices, IMvcProjectServices projectServices)
        {
            _apiCredentialServices = apiCredentialServices;
            _projectServices = projectServices;
        }

        public async Task<IActionResult> AddUpdateApiCredentialForm(int id)
        {
            if (!ModelState.IsValid)
            {
                var project = await _projectServices.projectList();
                ViewData["projectData"] = project.data;
                return View("~/Areas/Admin/Views/ApiCredentials/AddUpdateApiCredentialForm.cshtml");
            }
            else
            {
                if (id != 0)
                {
                    var rslt = await _apiCredentialServices.editApiCredential(id);
                    ViewData["apicredential"] = rslt.data;
                    var project = await _projectServices.projectList();
                    ViewData["projectData"] = project.data;
                    //ViewData["holiday"] = rslt.data;
                    return View("~/Areas/Admin/Views/ApiCredentials/AddUpdateApiCredentialForm.cshtml");
                }
                else
                {
                    var project = await _projectServices.projectList();
                    ViewData["projectData"] = project.data;
                    return View("~/Areas/Admin/Views/ApiCredentials/AddUpdateApiCredentialForm.cshtml");
                }
            }
        }

        public async Task<IActionResult> addUpdateApiCredential([FromForm] ApiCredentialsInput value)
        {
            var rslt = await _apiCredentialServices.addUpdateApiCredential(value);
            if (rslt.succeed)
            {
                TempData["Success"] = $"{rslt.message}";
                return RedirectToAction("ApiCredentialList", "ApiCredentials", new { area = "Admin" });
            }
            else
            {
                TempData["error"] = $"{rslt.message}";
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
        }
        public async Task<IActionResult>ApiCredentialList()
        {
            var rslt = await _apiCredentialServices.apiCredentialList();
            ViewData["apiCredentialData"] = rslt.data;
            return View("~/Areas/Admin/Views/ApiCredentials/ApiCredentialList.cshtml");
        }
        public async Task<IActionResult> deleteByIdApiCredential(int id)
        {
            var rslt = await _apiCredentialServices.deleteByIdApiCredential(id);

            return Redirect(HttpContext.Request.Headers["Referer"]);
        }
    }
}
