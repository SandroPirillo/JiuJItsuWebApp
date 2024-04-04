namespace JiuJitsuWebApp.Models
{
	public enum ClassType
	{
		Kids,
		Beginners,
		Advanced
	}

	public class TimeTableModel
	{
		public int Id { get; set; }
		public DateTime StartTime { get; set; }
		public ClassType ClassType { get; set; } = ClassType.Beginners;
	}
}
