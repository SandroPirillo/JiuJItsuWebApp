using JiuJitsuWebApp.Data;
using JiuJitsuWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Azure.Core;

namespace JiuJitsuWebApp.Controllers
{
	public class UserController : Controller
	{
		private readonly ApplicationDbContext _context;

		public UserController(ApplicationDbContext dataContext)
		{
			_context = dataContext;
		}

		public IActionResult UserRegistration()
		{
			ViewBag.SuccessfulRegistrationMessage = TempData["SuccessfulRegistrationMessage"] as string;
			ViewBag.RegistrationErrorMessage = TempData["RegistrationErrorMessage"] as string;
			ViewBag.IncorrectVerificationMessage = TempData["IncorrectVerificationMessage"] as string;
			return View();
		}

		public IActionResult UserLogin()
		{
			ViewBag.LoginError = TempData["LoginError"] as string;
			ViewBag.PasswordResetMessage = TempData["PasswordResetMessage"] as string;
			return View();
		}

		public IActionResult UserLogout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}

		public IActionResult ForgotPassword()
		{
			ViewBag.PasswordResetMessage = TempData["PasswordResetMessage"] as string;
			ViewBag.PasswordResetErrorMessage = TempData["PasswordResetErrorMessage"] as string;
			return View();
		}

        public IActionResult UserAccountManagement()
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
            {
                return RedirectToAction("UserLogin");
            }
            else
            {
                var userEmail = HttpContext.Session.GetString("UserEmail");
                var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    // Handle the case when the user is not found in the database
                    // You can redirect to an error page or display an appropriate message
                    return RedirectToAction("UserLogin");
                }
            }
        }

		public IActionResult EditUserBasicInfo() {

			var userEmail = HttpContext.Session.GetString("UserEmail");
			var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
			if (user != null)
			{
				return View(user);
			}
			else
			{
				// Handle the case when the user is not found in the database
				// You can redirect to an error page or display an appropriate message
				return RedirectToAction("UserLogin");
			}
		}

		public IActionResult UpdateBasicInfo(UpdateBasicInfoRequest request)
		{
			var userEmail = HttpContext.Session.GetString("UserEmail");
			var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
			if (user != null)
			{
				user.FirstName = request.FirstName;
				user.LastName = request.LastName;
				user.DateOfBirth = request.DateOfBirth;
				_context.SaveChanges();
				return RedirectToAction("UserAccountManagement");
			}
			else
			{
				// Handle the case when the user is not found in the database
				// You can redirect to an error page or display an appropriate message
				return RedirectToAction("UserLogin");
			}
		}

		public IActionResult EditContactInfo() {
			var userEmail = HttpContext.Session.GetString("UserEmail");
			var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
			if (user != null)
			{
				return View(user);
			}
			else
			{
				// Handle the case when the user is not found in the database
				// You can redirect to an error page or display an appropriate message
				return RedirectToAction("UserLogin");
			}
		}

		public IActionResult UpdateContactInfo(UpdateContactInfoRequest request) {
			var userEmail = HttpContext.Session.GetString("UserEmail");
			var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
			if (user != null)
			{
				user.PhoneNumber = request.PhoneNumber;
				user.Email = request.Email;
				_context.SaveChanges();
				return RedirectToAction("UserAccountManagement");
			}
			else
			{
				// Handle the case when the user is not found in the database
				// You can redirect to an error page or display an appropriate message
				return RedirectToAction("UserLogin");
			}
		}
		public IActionResult DeleteAccount() {
			var userEmail = HttpContext.Session.GetString("UserEmail");
			var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
			if (user != null)
			{
				_context.Users.Remove(user);
				_context.SaveChanges();
				HttpContext.Session.Clear();
				return RedirectToAction("Index", "Home");
			}
			else
			{
				// Handle the case when the user is not found in the database
				// You can redirect to an error page or display an appropriate message
				return RedirectToAction("UserLogin");
			}
		}

        public IActionResult Register(UserRegisterRequests request)
        {
            if (_context.Users.Any(user => user.Email == request.Email))
            {
                TempData["RegistrationErrorMessage"] = "An account with this email already exists";
                return RedirectToAction("UserRegistration");
            }
            if (!ModelState.IsValid)
            {
                var errorMessage = "Something went wrong please try again. Invalid fields: ";
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    {
                        if (key == "Password")
                        {
                            errorMessage += "Your password must contain a minimum of 6 characters, ";
                        }
                        else if (key == "ConfirmPassword")
                        {
                            errorMessage += "Your password did not match, ";
                        }
                        else if (key == "Email")
                        {
                            errorMessage += "Please provide a valid email address, ";
                        }
                        else if (key == "FirstName")
                        {
                            errorMessage += "Your first name must contain a minimum of 2 characters, ";
                        }
                        else if (key == "LastName")
                        {
                            errorMessage += "Your last name must contain a minimum of 2 characters, ";
                        }
                    }
                }
                errorMessage = errorMessage.TrimEnd(',', ' ');
                TempData["RegistrationErrorMessage"] = errorMessage;
                return RedirectToAction("UserRegistration");
            }
            else
            {
                CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
                var user = new User
                {
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    VerificationToken = CreateRandomToken(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    DateOfBirth = request.DateOfBirth
                };

                _context.Users.Add(user);
                _context.SaveChanges();
                var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("61e896bbc7725f", "99881ecf294538"),
                    EnableSsl = true
                };
                string emailMessage = "Here is your verification code:" + user.VerificationToken;
                client.Send("sandropirillo@hotmail.com", request.Email, "Verification Email Lakeside Jiu Jitsu Academy", emailMessage);
                TempData["SuccessfulRegistrationMessage"] = "Thank you for creating an account, please check your email for a verification code your account";
                return RedirectToAction("UserRegistration");
            }
        }



		public IActionResult Login(UserLoginRequests request)
		{
			var user = _context.Users.FirstOrDefault(user => user.Email == request.Email);
			if (user == null)
			{
				TempData["LoginError"] = "User does not exist";
				return RedirectToAction("UserLogin");
			}
			if (user.Verified == null)
			{
				TempData["LoginError"] = "User not verified";
				return RedirectToAction("UserLogin");
			}
			if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
			{
				TempData["LoginError"] = "Invalid password";
				return RedirectToAction("UserLogin");
			}
			else
			{
				HttpContext.Session.SetString("UserEmail", user.Email);
				return RedirectToAction("Index", "Home");
			}
		}


		public IActionResult Verify(string token)
		{
			var user = _context.Users.FirstOrDefault(user => user.VerificationToken == token);
			if (user == null)
			{
				TempData["IncorrectVerificationMessage"] = "Sorry Incorrect Verification token";
				return RedirectToAction("UserRegistration");
			}
			user.Verified = DateTime.Now;
			_context.SaveChanges();

			return RedirectToAction("Index", "Home");
		}


		public IActionResult PasswordResetRequest(string email)
		{
			var user = _context.Users.FirstOrDefault(user => user.Email == email);
			if (user == null)
			{
				TempData["PasswordResetErrorMessage"] = "User does not exist";
				return RedirectToAction("ForgotPassword");
			}
			user.PasswordResetToken = CreateRandomToken();
			user.PasswordResetExpires = DateTime.Now.AddHours(1);

			_context.SaveChanges();
			var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
			{
				Credentials = new NetworkCredential("61e896bbc7725f", "99881ecf294538"),
				EnableSsl = true
			};
			string emailMessage = "Here is your reset token:" + user.PasswordResetToken;
			client.Send("sandropirillo@hotmail.com", email, "Password reset Lakeside Jiu Jitsu Academy", emailMessage);
			TempData["PasswordResetMessage"] = "Please check your email for a password reset code";
			return RedirectToAction("ForgotPassword");


		}

		public IActionResult ResetPassword(ResetPasswordRequest request)
		{
			var user = _context.Users.FirstOrDefault(user => user.PasswordResetToken == request.PasswordResetToken);
			if (user == null)
			{
				TempData["PasswordResetMessage"] = "Invalid reset token";
				return RedirectToAction("ForgotPassword");
			}
			if (user.PasswordResetExpires < DateTime.Now)
			{
				TempData["PasswordResetMessage"] = "Reset token has expired";
				return RedirectToAction("ForgotPassword");
			}
			user.PasswordResetToken = null;
			user.PasswordResetExpires = null;
			CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;
			_context.SaveChanges();
			TempData["PasswordResetMessage"] = "Password reset successful";
			return RedirectToAction("UserLogin");
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		private Boolean VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(passwordHash);

			}
		}

		private string CreateRandomToken()
		{
			return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
		}
	}
}
