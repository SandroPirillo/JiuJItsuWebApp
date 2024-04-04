using System.ComponentModel.DataAnnotations.Schema;

namespace JiuJitsuWebApp.Models
{
	public class BookingModel
	{
		public int Id { get; set; }
		public DateTime TimeOfBooking { get; set; }


		[ForeignKey("User")]
		public int UserId { get; set; }
		public User person { get; set; }

		[ForeignKey("Class")]
		public int ClassId { get; set; }
		public TimeTableModel Class { get; set; } 
	}
}
