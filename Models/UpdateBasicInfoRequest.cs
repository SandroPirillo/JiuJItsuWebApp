namespace JiuJitsuWebApp.Models
{
	public class UpdateBasicInfoRequest
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public DateTime DateOfBirth { get; set; }
	}
}
