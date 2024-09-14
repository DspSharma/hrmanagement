using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hrmanagement.Areas.Public.Controllers
{
    public class CheckSessionIsAvailableController : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.HttpContext == null || filterContext.HttpContext.Session.GetString("token") == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "Login",
                    controller = "Auth",
                    area = "Admin"
                }));
            }
        }
    }
}
