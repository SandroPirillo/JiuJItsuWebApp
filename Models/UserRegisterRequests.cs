﻿using System.ComponentModel.DataAnnotations;

namespace JiuJitsuWebApp.Models
{
	public class UserRegisterRequests
	{
		[Required, EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required, MinLength(6)]
		public string Password { get; set; } = string.Empty;

		[Required, Compare(nameof(Password))]
		public string? ConfirmPassword { get; set; } = string.Empty;

	}
}
