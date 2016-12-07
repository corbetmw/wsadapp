using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wsad_app.Models.DataAccess;
using wsad_app.Areas.Admin.Models.SessionManager;
using wsad_app.Controllers;
using wsad_app.Models.SessionCart;
using wsad_app.Areas.Admin.Models.ViewModels.SessionManager;
using wsad_app.Models.Business;
using System.Diagnostics;

namespace wsad_app.Areas.Admin.Controllers
{
    [Authorize(Roles = "SystemAdmin,UserManager")]
    public class SessionManagerController : Controller
    {
        // GET: SessionManager
        public ActionResult Index()
        {
            List<SessionManager_SessionViewModel> collectionOfSessionVM = new List<SessionManager_SessionViewModel>();
            //Setup a DbContext
            using (wsadDbContext context = new wsadDbContext())
            {
                //Get all users
                var dbSessions = context.Sessions;
                //Move all users into a ViewModel object
                foreach(var sessionDTO in dbSessions)
                {
                    collectionOfSessionVM.Add(
                        new SessionManager_SessionViewModel(sessionDTO)
                        );
                }
            }
            //Send ViewModel Collection theView              
            return View(collectionOfSessionVM);
        }

        public ActionResult GetRegistrations(int id)
        {
            SessionCartManager cartMgr = new SessionCartManager();

            IQueryable<SessionCart> allItems = cartMgr.GetAllSessionsByUser(null, id);

            List<SessionCartViewModel> cartVM = new List<SessionCartViewModel>();
            foreach (var item in allItems)
            {
                cartVM.Add(new SessionCartViewModel(item));
            }

            return PartialView("_UserSessions", cartVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SessionManager_CreateEditViewModel newSessionVM)
        {
            //Validate password and username are defined
            if (string.IsNullOrWhiteSpace(newSessionVM.Title) == true)
            {
                ModelState.AddModelError("", "Title is required");
                return View(newSessionVM);
            }


            //Create a user dto template
            Session userTemplate = new wsad_app.Models.DataAccess.Session()
            {
                Title = newSessionVM.Title,
                Description = newSessionVM.Description,
                Building = newSessionVM.Building,
                Room = newSessionVM.Room,
                TotalSeats = newSessionVM.TotalSeats,
                AvailableSeats = newSessionVM.AvailableSeats,
                DateAndTime = newSessionVM.DateAndTime
            };

            //Create a user manager
            ScheduleManager sessMngr = new ScheduleManager();

            //Send new user into manager
            try
            {
                sessMngr.AddSession(userTemplate);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error occured while saving user to database. " + ex.Message);
                Debug.WriteLine(ex.Message);
                return View(newSessionVM);
            }

            //Redirect to Index
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ScheduleManager sessMgr = new ScheduleManager();
            Session sessionDTO = sessMgr.GetSession(id);
            if (sessionDTO == null)
            {
                return HttpNotFound("Invalid Id provided");
            }

            //Convert to View Model
            SessionManager_CreateEditViewModel sessionViewModel = new SessionManager_CreateEditViewModel(sessionDTO);

            return View(sessionViewModel);
        }

        [HttpPost]
        public ActionResult Edit(SessionManager_CreateEditViewModel editSessionVM)
        {
            //Validate password and sessionname are defined
            if (string.IsNullOrWhiteSpace(editSessionVM.Title) == true)
            {
                ModelState.AddModelError("", "Title is required");
                return View(editSessionVM);
            }


            //Create a session dto template
            Session sessionTemplate = new wsad_app.Models.DataAccess.Session()
            {
                Id = editSessionVM.Id,
                Title = editSessionVM.Title,
                Description = editSessionVM.Description,
                Building = editSessionVM.Building,
                Room = editSessionVM.Room,
                TotalSeats = editSessionVM.TotalSeats,
                AvailableSeats = editSessionVM.AvailableSeats,
                DateAndTime = editSessionVM.DateAndTime
            };

            //Create a session manager
            ScheduleManager sessMgr = new ScheduleManager();

            //Send new user into manager
            try
            {
                sessMgr.UpdateSession(sessionTemplate);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error occured while saving user to database. " + ex.Message);
            }

            //Redirect to Index
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            ScheduleManager sessMgr = new ScheduleManager();

            Session sessionDTO = sessMgr.GetSession(id);

            if (sessionDTO == null) { return HttpNotFound("Invalid Id"); }

            SessionManager_SessionViewModel sessVM = new SessionManager_SessionViewModel(sessionDTO);

            return View(sessVM);

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            ScheduleManager sessMgr = new ScheduleManager();
            Session sessionDTO = sessMgr.GetSession(id);

            if (sessionDTO == null) { return HttpNotFound("Invalid Id"); }

            SessionManager_SessionViewModel sessionVM = new SessionManager_SessionViewModel(sessionDTO);

            //Returns a Confirm Delete Screen
            return View(sessionVM);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            ScheduleManager sessMgr = new ScheduleManager();
            sessMgr.DeleteSession(id);

            //Does the Delete
            return RedirectToAction("Index");
        }
    }
}