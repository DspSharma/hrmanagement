using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [BaseController]
    public class UserMemoController : Controller
    {
        public IMvcUserMemoServices _userMemoServices;
        public UserMemoController(IMvcUserMemoServices userMemoServices)
        {
            _userMemoServices = userMemoServices;
        }
        public async Task<IActionResult> userMemoList(int id)
        {
            var rslt = await _userMemoServices.getUserMemoPublic();
            ViewData["userMemo"] = rslt.data;
            return View("~/Areas/Admin/Views/UserMemo/userMemoList.cshtml");
            //return View("~/Areas/Public/Views/UserMemo/userMemoList.cshtml");
        }
    }
}
