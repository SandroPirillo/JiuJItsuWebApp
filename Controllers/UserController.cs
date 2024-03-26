using JiuJitsuWebApp.Data;
using JiuJitsuWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

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
            return View();
        }

        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost ("UserRegistration")]
        public IActionResult Register(UserRegisterRequests request)
        {
            if (_context.Users.Any(user => user.Email == request.Email))
            {
                return BadRequest("User already exists");
            }
            if (!ModelState.IsValid){ 
                return BadRequest("Invalid input");
            }
            else {

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

            return RedirectToAction("UserRegistration");
            }
        }


		[HttpPost ("UserLogin")]
		public IActionResult Login(UserLoginRequests request)
		{
            var user = _context.Users.FirstOrDefault(user => user.Email == request.Email);
			if(user == null)
            {
				return BadRequest("User does not exist");
			}
            if (user.Verified == null) {
				return BadRequest("User not verified");
			}
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Invalid password");
            }
            else {
                    return RedirectToAction("UserLogin");
            }
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
