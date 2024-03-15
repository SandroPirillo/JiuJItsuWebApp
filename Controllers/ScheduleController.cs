using Microsoft.AspNetCore.Mvc;

namespace JiuJitsuWebApp.Controllers
{
    public class ScheduleController : BaseController
	{
        public ActionResult Timetable()
        {
            return View();
        }
    }

}
