using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace JiuJitsuWebApp.Controllers
{
    public class ContactController : BaseController
	{
        public ActionResult ContactUs()
        {
            return View();
        }

		public ActionResult TrialClass()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ContactUs(string firstname, string lastname, string email, string phone, string subject, string message)
		{
			System.Diagnostics.Debug.WriteLine("First Name: " + firstname);

			// Here you can handle the form submission
			// For example, you can save the data to a database or send an email
			try
			{
				MailMessage mail = new MailMessage();
				SmtpClient SmtpServer = new SmtpClient("your_smtp_server");

				mail.From = new MailAddress("your_email@your_domain.com");
				mail.To.Add(email);
				mail.Subject = subject;
				mail.Body = message;

				SmtpServer.Port = 587; // or your port
				SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
				SmtpServer.EnableSsl = true;

				SmtpServer.Send(mail);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}

			// After handling the form submission, you can redirect the user to a 'Thank You' page or back to the form page
			// return RedirectToAction("ThankYou");
			// or
			return View();
		}

	}

}
