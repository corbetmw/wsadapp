using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using wsad_app.Models.Account;
using wsad_app.Models.DataAccess;

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
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(AccountCreateViewModel createdUser)
        {
            //Validate the account information
            if (!ModelState.IsValid)
            {
                return View(createdUser);
            }

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

            if(!createdUser.Password.Equals(createdUser.ConfirmPassword))
            {
                ModelState.AddModelError("", "Your password does not match");
                return View();
            }

            //Create Database connectoin
            using (wsadDbContext context = new wsadDbContext())
            {
                if (context.Users.Any(
                    row => row.UserName.Equals(createdUser.UserName)
                    ))
                {
                    ModelState.AddModelError("", "Username " + createdUser.UserName + " already exists. Please select another.");
                    createdUser.UserName = "";
                    return View(createdUser);
                }

                //Setup insert into database
                Models.DataAccess.User newUserObj;
                newUserObj = new Models.DataAccess.User()
                {
                    FirstName = createdUser.FirstName,
                    LastName = createdUser.LastName,
                    EmailAddress = createdUser.EmailAddress,
                    Gender = createdUser.Gender,
                    UserName = createdUser.UserName,
                    Password = createdUser.Password,
                    EmailOpt = createdUser.EmailOpt
                };

                //Commit the insert
                newUserObj = context.Users.Add(newUserObj);
                context.SaveChanges();
            }

            //Show user creation page with inforation they gave
            TempData["Message"] = "Account Creation Successful";
            return RedirectToAction("Login");
            //return View("Confirmation", createdUser);
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

        [HttpPost]
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
            wsadDbContext context = new wsadDbContext();
            //Query for username and passowrd hash
            bool isValidLogin = context.Users.Any(row => row.UserName == login.Username && row.Password == login.Password);
            //If invalid, send error
            if (isValidLogin)
            {
                //Create a security ticket
                FormsAuthentication.SetAuthCookie(login.Username, login.RememberMe);
                //Store the ticket in a cookie
                string redirectToUrl = FormsAuthentication.GetRedirectUrl(
                    login.Username,
                    login.RememberMe
                );
                //Redirect to home page, or wherever
                return Redirect(redirectToUrl);
            }
            else
            {
                TempData["Message"] = "That's not a proper username or password!";
                    return View();
            }
            //If valid, redirect to user profile
            //System.Web.Security.FormsAuthentication.SetAuthCookie(login.Username, login.RememberMe);

            //return Redirect(FormsAuthentication.GetRedirectUrl(login.Username, login.RememberMe));
        }

        public ActionResult UserInformationPartial()
        {
            return PartialView();
        }

        public ActionResult UserProfile()
        {
            //Build a DbContext
            wsadDbContext context = new wsadDbContext();

            //Get my user DTO from database
            User userDTO = context.Users.FirstOrDefault(row => row.UserName == User.Identity.Name);

            //Build UserProfile ViewModel
            UserProfileViewModel userProfileVM = new UserProfileViewModel(userDTO);

            //Return View with ViewModel
            return View(userProfileVM);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}