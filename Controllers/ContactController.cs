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
			ViewBag.ThankYouMessage = TempData["ThankYouMessage"] as string;
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
				return View("Error");
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

		[HttpPost]
		public ActionResult TrialClass(string firstname, string lastname, string parentname, string email, string phone, string classtype, string source, string message)
		{
			var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
			{
				Credentials = new NetworkCredential("61e896bbc7725f", "99881ecf294538"),
				EnableSsl = true
			};

			//validation of the form
			if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(email)
				|| string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(classtype) || (classtype.Contains("Kids") && string.IsNullOrEmpty(parentname)))
			{
				return View("Error");
			}
			string Inquiry = "First Name: " + firstname + "\n" +
							 "Last Name: " + lastname + "\n" +
							 "Email: " + email + "\n" +
							 "Phone: " + phone + "\n" +
							 "Class Type: " + classtype;

			if (!string.IsNullOrEmpty(parentname))
			{
				Inquiry += "\nParent Name: " + parentname;
			}

			if (!string.IsNullOrEmpty(source))
			{
				Inquiry += "\nHow did you hear about us: " + source;
			}

			if (!string.IsNullOrEmpty(message))
			{
				Inquiry += "\nMessage: " + message;
			}

			client.Send(email, "sandropirillo@hotmail.com", "Trial class booking", Inquiry);
			TempData["ThankYouMessage"] = "Thank you for choosing to start your jiu jitsu journey, we look forward to meeting you";
			return RedirectToAction("TrialClass");
		}

	}

}

