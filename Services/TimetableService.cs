using JiuJitsuWebApp.Data;
using JiuJitsuWebApp.Models;


namespace JiuJitsuWebApp.Services
{
	public class TimetableService
	{
		private readonly ApplicationDbContext _context;

		public TimetableService(ApplicationDbContext context)
		{
			_context = context;
		}

		public void GenerateWeeklyTimetable(DateTime startDate)
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
					ClassType = ClassType.Kids
				});

				// Add a Beginners class at 12 PM
				timetable.Add(new TimeTableModel
				{
					StartTime = date.AddHours(12),
					ClassType = ClassType.Beginners
				});

				// Add an Advanced class at 3 PM
				timetable.Add(new TimeTableModel
				{
					StartTime = date.AddHours(15),
					ClassType = ClassType.Advanced
				});
			}

			// Add the timetable to the database
			_context.TimeTables.AddRange(timetable);
			_context.SaveChanges();
		}
	}

}
