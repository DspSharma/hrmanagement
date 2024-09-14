using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement.Areas.Public.Controllers
{
    [Area("Public")]
    [CheckSessionIsAvailableController]
    public class UserMemoController : Controller
    {
        public IMvcUserMemoServices _userMemoServices;
        public UserMemoController(IMvcUserMemoServices userMemoServices)
        {
            _userMemoServices = userMemoServices;
        }
        public async Task<IActionResult> addUpdateUsermemo([FromBody] UserMemoInput model)
        {
            var rslt = await _userMemoServices.addUpdateUserMemo(model);
            if (rslt.succeed)
            {
                TempData["Success"] = $"{rslt.message}";
                return RedirectToAction("userMemoList", "UserMemo", new { area = "Public" });
            }
            else
            {
                TempData["error"] = $"{rslt.message}";
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
        }
        public async Task<IActionResult> getEditById(int id)
        {
            var rslt = await _userMemoServices.editUserMemo(id);
            return Ok(rslt);
        }
        public async Task<IActionResult> userMemoList()
        {
            int id = 0;
            var rslt = await _userMemoServices.getUserMemoById(id);
            ViewData["userMemo"] = rslt.data;
            return View("~/Areas/Public/Views/UserMemo/userMemoList.cshtml");
        }
        public async Task<IActionResult> publicUserMemo()
        {
            var rslt = await _userMemoServices.getUserMemoPublic();
            ViewData["userMemo"] = rslt.data;
            return View("~/Areas/Public/Views/UserMemo/publicUserMemo.cshtml");
        }
        public async Task<IActionResult> deleteByIdUserMemo(int id)
        {
            var rslt = await _userMemoServices.deleteByIdUserMemo(id);
            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

        
    }
}
