using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Hrmanagement.ViewComponents
{
    [ViewComponent(Name = "Toaster")]
    public class ToastrComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("~/Views/Component/Toaster/Toaster.cshtml");
        }
    }
}
