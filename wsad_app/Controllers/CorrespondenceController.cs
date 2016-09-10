using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wsad_app.Models.Correspondence;

namespace wsad_app.Controllers
{
    public class CorrespondenceController : Controller
    {
        // GET: Correspondence
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactEmailViewModel contactMessage)
        {
            //Validate contact message input
            if (contactMessage == null)
            {
                ModelState.AddModelError("", "No Message has been provided!");
                return View();
            }

            if (string.IsNullOrWhiteSpace(contactMessage.Name) || 
                string.IsNullOrWhiteSpace(contactMessage.Email) ||
                string.IsNullOrWhiteSpace(contactMessage.Message))
            {
                ModelState.AddModelError("", "All fields are required!");
                return View();
            }

            //Create an email message object 
            System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();

            //Populate the object
            email.To.Add("corbetmw@mail.uc.edu");
            email.From = new System.Net.Mail.MailAddress(contactMessage.Email);
            email.Subject = "This is our email to you!";
            email.Body = string.Format(
                "Name: {0}\r\nMessage: {1}",
                    contactMessage.Name,
                    contactMessage.Message
                );

            email.IsBodyHtml = false;


            //Setup an SMTP client to send the message
            System.Net.Mail.SmtpClient smptClient = new System.Net.Mail.SmtpClient();
            smptClient.Host = "smtp.fuse.net";

            //Send the message
            smptClient.Send(email);

            //Notify the user that the message was sent
            return View("emailConfirmation");
        }
    }
}