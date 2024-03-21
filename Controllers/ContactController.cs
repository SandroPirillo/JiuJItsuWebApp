using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace JiuJitsuWebApp.Controllers
{
    public class ContactController : BaseController
	{
        public ActionResult ContactUs()
        {
			ViewBag.ThankYouMessage = TempData["ThankYouMessage"] as string;
			return View();
        }

		public ActionResult TrialClass()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ContactUs(string firstname, string lastname, string email, string phone, string subject, string message)
		{
			var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
			{
				Credentials = new NetworkCredential("61e896bbc7725f", "99881ecf294538"),
				EnableSsl = true
			};
			//validation of the form
			if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(email) 
				|| string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message))
			{
				RedirectToAction("ContactUs");
			}

			string Inquiry = "First Name: " + firstname + "\n" +
								 "Last Name: " + lastname + "\n" +
								 "Email: " + email + "\n" +
								 "Phone: " + phone + "\n" +
								 "Message: " + message;

			client.Send(email, "sandropirillo@hotmail.com", subject, Inquiry);
			TempData["ThankYouMessage"] = "Thank you for your inquiry. We will get back to you soon.";
			return RedirectToAction("ContactUs");
		}

	}

}
