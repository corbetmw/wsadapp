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

            bool isValid = false;
            using (wsadDbContext context = new wsadDbContext())
            {
                //hash password

                //Query for the user based on username and password hash
                if (context.Users.Any(
                    row => row.UserName.Equals(login.Username)
                    && row.Password.Equals(login.Password)
                    ))
                {
                    isValid = true;
                }
            }

            //If invalid, send error
            if (!isValid)
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }
            else {
                //Valid, redirect to user profile
                System.Web.Security.FormsAuthentication.SetAuthCookie(login.Username, login.RememberMe);

                return Redirect(FormsAuthentication.GetRedirectUrl(login.Username, login.RememberMe));
            }
        }

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
                    EmailOpt = createdUser.EmailOpt,
                    IsAdmin = createdUser.IsAdmin
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Get the user by Id
            EditViewModel editVM;
            using (wsadDbContext context = new wsadDbContext())
            {
                //Get user from database
                User userDTO = context.Users.Find(id);

                if (userDTO == null)
                {
                    return Content("Invalid Id");
                }
                //Create an EditViewModel
                editVM = new EditViewModel()
                {
                    EmailAddress = userDTO.EmailAddress,
                    FirstName = userDTO.FirstName,
                    Id = userDTO.Id,
                    LastName = userDTO.LastName,
                    UserName = userDTO.UserName,
                    IsAdmin = userDTO.IsAdmin
                };
            }

            
            //Send the ViewModel to the view
            return View(editVM);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel editVM)
        {
            //Varilables
            bool needsPasswordReset = false;
            //Validate the model
            if (!ModelState.IsValid)
            {
                return View(editVM);
            }
            //Check for a password change
            if (string.IsNullOrWhiteSpace(editVM.Password))
            {
                //compare password with password confirm
                if (!editVM.Password.Equals(editVM.PasswordConfirm))
                {
                    ModelState.AddModelError("", "Both Passwords must match!");
                }
                else
                {
                    needsPasswordReset = true;
                }
            }

            //Get user from datbase
            using (wsadDbContext context = new wsadDbContext())
            {
                //Get DTO
                User userDTO = context.Users.Find(editVM.Id);
                if (userDTO == null){ return Content("Invalid User Id"); }

                //Set or update values from the view model
                userDTO.FirstName = editVM.FirstName;
                userDTO.EmailAddress = editVM.EmailAddress;
                userDTO.LastName = editVM.LastName;
                userDTO.UserName = editVM.UserName;
                userDTO.IsAdmin = editVM.IsAdmin;
                if (needsPasswordReset)
                {
                    userDTO.Password = editVM.Password;
                }

                //Save changes
                context.SaveChanges();
            }
                
                return RedirectToAction("UserProfile");
        }
    }
}