using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Azure;
using Azure.Communication.Email;

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
			string connectionString = "endpoint=https://lakesidejiujitsucommunicationservice.australia.communication.azure.com/;accesskey=y8h50R2R3z1L4Au9MIXadwO78yohyFR/Bq9oYtb2VqR+w+oQCgecgazTwWjmajx6ZGXTAnksS1OBrWoOB03siQ==";
			var emailClient = new EmailClient(connectionString);

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
			var HTMLInquiry = $"<html><body><h2>Inquiry Details:</h2><p><strong>First Name:</strong> {firstname}</p><p><strong>Last Name:</strong> " +
				$"{lastname}</p><p><strong>Email:</strong> {email}</p><p><strong>Phone:</strong> {phone}</p><p><strong>Message:</strong> {message}</p></body></html>";
			EmailSendOperation emailSendOperation = emailClient.Send(
			WaitUntil.Completed,
			senderAddress: "DoNotReply@ba9ab7b6-a594-4ae9-997a-8313fc05ccc4.azurecomm.net",
			recipientAddress: email,
			subject: subject,
			htmlContent: HTMLInquiry,
			plainTextContent: Inquiry);
			TempData["ThankYouMessage"] = "Thank you for your inquiry. We will get back to you soon.";
			return RedirectToAction("ContactUs");
		}

		[HttpPost]
		public ActionResult TrialClass(string firstname, string lastname, string parentname, string email, string phone, string classtype, string source, string message)
		{
			string connectionString = "endpoint=https://lakesidejiujitsucommunicationservice.australia.communication.azure.com/;accesskey=y8h50R2R3z1L4Au9MIXadwO78yohyFR/Bq9oYtb2VqR+w+oQCgecgazTwWjmajx6ZGXTAnksS1OBrWoOB03siQ==";
			var emailClient = new EmailClient(connectionString);

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

			var HTMLInquiry = $"<html><body><h2>Inquiry Details:</h2><p><strong>First Name:</strong> {firstname}</p><p><strong>Last Name:</strong> " + $"{lastname}</p><p><strong>Email:</strong> {email}</p><p><strong>Phone:</strong> {phone}</p><p><strong>Message:</strong> {message}</p></body></html>";
			EmailSendOperation emailSendOperation = emailClient.Send(
			WaitUntil.Completed,
			senderAddress: "DoNotReply@ba9ab7b6-a594-4ae9-997a-8313fc05ccc4.azurecomm.net",
			recipientAddress: email,
			subject: "Trail Class Booking",
			htmlContent: HTMLInquiry,
			plainTextContent: Inquiry);
			TempData["ThankYouMessage"] = "Thank you for choosing to start your jiu jitsu journey, we look forward to meeting you";
			return RedirectToAction("TrialClass");
		}

	}

}

