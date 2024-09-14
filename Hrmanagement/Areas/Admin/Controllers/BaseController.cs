using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hrmanagement.Areas.Admin.Controllers
{
    public class BaseController : ActionFilterAttribute
    {
        //List<string> authenitcateAction { get; set; }
        //List<string> authorizeAction { get; set; } = new List<string>();
        //public void authenticate(string actions)
        //{
        //    string[] actionsArr = actions.Split(",");
        //    this.authenitcateAction = actionsArr.ToList();
        //}
        //public void authorize(string actions)
        //{
        //    string[] actionsArr = actions.Split(",");
        //    this.authorizeAction = actionsArr.ToList();
        //}


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var httpContextAccessor = new HttpContextAccessor();

           // string id = httpContextAccessor.HttpContext.Session.GetString("token");
            //string userType = httpContextAccessor.HttpContext.Session.GetString("role");
            //var controller = httpContextAccessor.HttpContext.Request.RouteValues["controller"].ToString();
            //string currentAction = httpContextAccessor.HttpContext.Request.RouteValues["action"].ToString();


            if (filterContext.HttpContext == null || filterContext.HttpContext.Session.GetString("token") == null)
            {
                //return RedirectToAction("Index", "Login");
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "Login",
                    controller = "Auth",
                    area = "Admin"
       
                }));
            }

            //if (this.authorizeAction.Contains(currentAction) && string.IsNullOrEmpty(id))
            //{

            //        Response.Redirect("../Auth/Login");
            //}
            //else if (this.authorizeAction.Contains(currentAction) && !string.IsNullOrEmpty(id))
            //{
            //    if (userType == "Public")
            //        Response.Redirect("../Auth/Login");
            //}
        }
    }
}
