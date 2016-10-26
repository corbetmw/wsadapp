using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wsad_app.Models.Business;
using wsad_app.Models.DataAccess;
using wsad_app.Models.UserManager;

namespace wsad_app.Controllers
{
    [Authorize(Roles ="SystemAdmin,UserManager")]
    public class UserManagerController : Controller
    {
        // GET: UserManager
        public ActionResult Index()
        {
            List<UserManager_UserViewModel> allUserViewModel = new List<UserManager_UserViewModel>();
            //CreateDB Context

            UserManager userMgr = new UserManager();

            //Query all users from the DbContext
            IEnumerable<User> allUsers = userMgr.GetAllUsers();

            //Convert the user DTO into user ViewModel
            foreach (User userDTO in allUsers)
            {
                //Create User ViewModel from DTO
                UserManager_UserViewModel userVM;
                userVM = new UserManager_UserViewModel(userDTO);

                //Add User ViewModel to List of ViewModels
                allUserViewModel.Add(userVM);
            }

            //Return Collection of Users to the View
            return View(allUserViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserManager_CreateEditViewModel newUserVM)
        {
            //Validate password and username are defined
            if (string.IsNullOrWhiteSpace(newUserVM.UserName) == true)
            {
                ModelState.AddModelError("", "Username is required");
                return View(newUserVM);
            }

            if (string.IsNullOrWhiteSpace(newUserVM.Password) == true)
            {
                ModelState.AddModelError("", "Password is required");
                return View(newUserVM);
            }

            if (newUserVM.Password != newUserVM.PasswordConfirm)
            {
                ModelState.AddModelError("", "Password Confirm does not match");
                return View(newUserVM);
            }


            //Create a user dto template
            User userTemplate = new Models.DataAccess.User()
            {
                EmailAddress = newUserVM.EmailAddress,
                EmailOpt = newUserVM.EmailOpt,
                FirstName = newUserVM.FirstName,
                LastName = newUserVM.LastName,
                Password = newUserVM.Password,
                UserName = newUserVM.UserName
            };

            //Create a user manager
            UserManager userMngr = new UserManager();

            //Send new user into manager
            try
            {
                userMngr.AddUser(userTemplate);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error occured while saving user to database. " + ex.Message);
                Debug.WriteLine(ex.Message);
                return View(newUserVM);
            }

            //Redirect to Index
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            UserManager userMgr = new UserManager();
            User userDTO = userMgr.GetUser(id);
            if (userDTO == null)
            {
                return HttpNotFound("Invalid Id provided");
            }

            //Convert to View Model
            UserManager_CreateEditViewModel userViewModel = new UserManager_CreateEditViewModel(userDTO);

            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult Edit(UserManager_CreateEditViewModel editUserVM)
        {
            //Validate password and username are defined
            if (string.IsNullOrWhiteSpace(editUserVM.UserName) == true)
            {
                ModelState.AddModelError("", "Username is required");
                return View(editUserVM);
            }

            if (editUserVM.Password != editUserVM.PasswordConfirm)
            {
                ModelState.AddModelError("", "Password Confirm does not match");
                return View(editUserVM);
            }


            //Create a user dto template
            User userTemplate = new Models.DataAccess.User()
            {
                Id = editUserVM.Id,
                EmailAddress = editUserVM.EmailAddress,
                EmailOpt = editUserVM.EmailOpt,
                FirstName = editUserVM.FirstName,
                LastName = editUserVM.LastName,
                Password = editUserVM.Password,
                UserName = editUserVM.UserName
            };

            //Create a user manager
            UserManager userMngr = new UserManager();

            //Send new user into manager
            try
            {
                userMngr.UpdateUser(userTemplate);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error occured while saving user to database. " + ex.Message);
                return View(editUserVM);
            }

            //Redirect to Index
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            UserManager userMgr = new UserManager();
            User userDTO = userMgr.GetUser(id);

            if (userDTO == null) { return HttpNotFound("Invalid Id"); }

            UserManager_UserViewModel userVM = new UserManager_UserViewModel(userDTO);

            return View(userVM);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            UserManager userMgr = new UserManager();
            User userDTO = userMgr.GetUser(id);

            if (userDTO == null) { return HttpNotFound("Invalid Id"); }

            UserManager_UserViewModel userVM = new UserManager_UserViewModel(userDTO);

            //Returns a Confirm Delete Screen
            return View(userVM);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            UserManager userMgr = new UserManager();
            userMgr.DeleteUser(id);

            //Does the Delete
            return RedirectToAction("Index");
        }
    }
}