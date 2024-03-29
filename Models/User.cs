namespace JiuJitsuWebApp.Models
{
	public enum MembershipType
	{
		NotActive,
		Basic,
		Unlimited,
		Family,
		Kids
	}

	public class User
	{
		public int Id { get; set; }
		public string Email { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public DateTime DateOfBirth { get; set; }
		public MembershipType MembershipType { get; set; } = MembershipType.NotActive;
		public byte[] PasswordHash { get; set; } = new byte[32];
		public byte[] PasswordSalt { get; set; } = new byte[32];
		public string? VerificationToken { get; set; }
		public DateTime? Verified { get; set; }
		public string? PasswordResetToken { get; set; }
		public DateTime? PasswordResetExpires { get; set; }
	}
}
