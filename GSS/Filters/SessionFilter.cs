using GSS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSS.Filters
{
    public class SessionFilter : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // check session is null
            if (SessionManager.NewsEventsList == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }
            if (SessionManager.NewsEvents == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }
            if (SessionManager.ContactUsinfo == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}