using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JiuJitsuWebApp.Controllers
{
	public class BaseController : Controller
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			ViewBag.TrialLinkUrl = "/Contact/TrialClass";
			base.OnActionExecuting(filterContext);
		}
	}

}
