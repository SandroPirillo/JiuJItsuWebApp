using JiuJitsuWebApp.Data;
using JiuJitsuWebApp.Models;
using JiuJitsuWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace JiuJitsuWebApp.Controllers
{
    public class ScheduleController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ScheduleController(ApplicationDbContext dataContext)
        {
            _context = dataContext;
        }

		public ActionResult Timetable()
		{
			DateTime currentDate = DateTime.Today.AddDays(4);
			DateTime previousMonday = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);
			DateTime nextSunday = previousMonday.AddDays(7);

			var classes = _context.TimeTables
				.Where(t => t.StartTime >= previousMonday && t.StartTime <= nextSunday)
				.ToList();

			return View(classes);
		}


		private void GenerateWeeklyTimetable(DateTime startDate)
        {
            // Ensure the start date is a Monday
            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(1);
            }

            var timetable = new List<TimeTableModel>();

            // Loop over each day of the week, including Saturday and Sunday
            for (int i = 0; i < 7; i++) // 7 for Monday to Sunday
            {
                var date = startDate.AddDays(i);

                // Add a Kids class at 9 AM
                timetable.Add(new TimeTableModel
                {
                    StartTime = date.AddHours(9),
                    ClassType = ClassType.Kids,
                    Day = (Day)i
                });

                // Add a Beginners class at 12 PM
                timetable.Add(new TimeTableModel
                {
                    StartTime = date.AddHours(12),
                    ClassType = ClassType.Beginners,
                    Day = (Day)i
                });

                // Add an Advanced class at 3 PM
                timetable.Add(new TimeTableModel
                {
                    StartTime = date.AddHours(15),
                    ClassType = ClassType.Advanced,
                    Day = (Day)i
                });
            }

            // Add the timetable to the database
            _context.TimeTables.AddRange(timetable);
            _context.SaveChanges();
        }


        public IActionResult GenerateTimetable()
        {
            // Get the start date of the next week
            DateTime startDate = DateTime.Today.AddDays(7 - (int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);

            // Generate the timetable for the next 4 weeks
            for (int i = 0; i < 4; i++)
            {
                GenerateWeeklyTimetable(startDate.AddDays(i * 7));
            }

            return RedirectToAction("Timetable");
        }


    }

}
