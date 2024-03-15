using Microsoft.AspNetCore.Mvc;

namespace JiuJitsuWebApp.Controllers
{
    public class MembershipController : BaseController
	{
        public ActionResult MembershipOptions()
        {
            return View();
        }
    }

}
