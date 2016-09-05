using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Login(string username, string password)
        {
            return Content("Hello " + username + "! welcome to our application. Please have a nice time.");

            //Validate a username and password(no empties)

            //Open database connection

            //Query for username and passowrd hash

            //If invalid, send error

            //If valid, redirect to user profile
        }
    }
}