using JiuJitsuWebApp.Data;
using JiuJitsuWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

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
            return View();
        }

        public IActionResult UserLogin()
        {
            ViewBag.LoginError = TempData["LoginError"] as string;
            return View();
        }

        public IActionResult UserLogout()
        {
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}

        [HttpPost("UserRegistration")]
        public IActionResult Register(UserRegisterRequests request)
        {
            if (_context.Users.Any(user => user.Email == request.Email))
            {
                return BadRequest("User already exists");
            }
            if (!ModelState.IsValid)
            {

                return BadRequest("Invalid input");
            }
            else
            {

                CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
                var user = new User
                {
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    VerificationToken = CreateRandomToken()
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


        [HttpPost("UserLogin")]
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

        [HttpPost("verify")]
        public IActionResult Verify(string token)
        {
            var user = _context.Users.FirstOrDefault(user => user.VerificationToken == token);
			if (user == null)
            {
                return BadRequest("Invalid Token");
            }
            user.Verified = DateTime.Now;
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
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
