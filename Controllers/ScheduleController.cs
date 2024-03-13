using Microsoft.AspNetCore.Mvc;

namespace JiuJitsuWebApp.Controllers
{
    public class ScheduleController : Controller
    {
        public ActionResult Timetable()
        {
            return View();
        }
    }

}
