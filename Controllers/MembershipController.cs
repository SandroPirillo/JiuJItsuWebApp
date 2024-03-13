using Microsoft.AspNetCore.Mvc;

namespace JiuJitsuWebApp.Controllers
{
    public class MembershipController : Controller
    {
        public ActionResult MembershipOptions()
        {
            return View();
        }
    }

}
