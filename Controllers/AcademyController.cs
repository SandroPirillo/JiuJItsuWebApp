using Microsoft.AspNetCore.Mvc;

namespace JiuJitsuWebApp.Controllers
{
    public class AcademyController : BaseController
	{
        public ActionResult OurTeam()
        {
            return View();
        }

        public ActionResult KidsClasses()
        {
            return View();
        }
        public ActionResult BeginnersClasses()
        {
            return View();
        }
        public ActionResult IntermediateClasses()
        {
            return View();
        }
        public ActionResult AdvancedClasses()
        {
            return View();
        }
    }
}
