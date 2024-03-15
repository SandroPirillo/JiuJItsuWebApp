using Microsoft.AspNetCore.Mvc;

namespace JiuJitsuWebApp.Controllers
{
    public class ContactController : BaseController
	{
        public ActionResult ContactUs()
        {
            return View();
        }

		public ActionResult TrialClass()
		{
			return View();
		}
	}

}
