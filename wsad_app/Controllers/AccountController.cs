using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult Create()
        {
            return View();
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
            return Content("Hello " + login.Username + "! welcome to our application. Please have a nice time. Your password is " + login.Password);

            //Validate a username and password(no empties)

            //Open database connection

            //Query for username and passowrd hash

            //If invalid, send error

            //If valid, redirect to user profile
        }
    }
}