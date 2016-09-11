using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using wsad_app.Models.Account;

namespace wsad_app.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return this.RedirectToAction("Login");
        }

        /// <summary>
        /// To Create an user account for my application
        /// </summary>
        /// <returns>ViewResult for the create</returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AccountCreateViewModel createdUser)
        {
            //Validate the account information
            if (createdUser == null)
            {
                ModelState.AddModelError("", "No information was given");
            }

            if (string.IsNullOrWhiteSpace(createdUser.FirstName) ||
               string.IsNullOrWhiteSpace(createdUser.LastName) ||
               string.IsNullOrWhiteSpace(createdUser.EmailAddress) ||
               string.IsNullOrWhiteSpace(createdUser.Gender) ||
               string.IsNullOrWhiteSpace(createdUser.UserName) ||
               string.IsNullOrWhiteSpace(createdUser.Password) ||
               string.IsNullOrWhiteSpace(createdUser.ConfirmPassword))
            {
                ModelState.AddModelError("", "All fields are required");
                return View();
            }

            if(createdUser.Password != createdUser.ConfirmPassword)
            {
                ModelState.AddModelError("", "Your password does not match");
                return View();
            }

            //Show user creation page with inforation they gave

            return View("Confirmation", createdUser);
        }

        /// <summary>
        /// Logging users into the website
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login(AccountLoginViewModel login)
        {

            //Validate a username and password(no empties)
            if (login == null)
            {
                ModelState.AddModelError("", "Login is required.");
                return View();
            }

            if (string.IsNullOrWhiteSpace(login.Username))
            {
                ModelState.AddModelError("", "Username is required.");
                return View();
            }

            if (string.IsNullOrWhiteSpace(login.Password))
            {
                ModelState.AddModelError("", "Password is required.");
                return View();
            }

            //Open database connection

            //Query for username and passowrd hash

            //If invalid, send error

            //If valid, redirect to user profile
            System.Web.Security.FormsAuthentication.SetAuthCookie(login.Username, login.RememberMe);

            return Redirect(FormsAuthentication.GetRedirectUrl(login.Username, login.RememberMe));
        }
    }
}