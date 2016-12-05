using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wsad_app.Models.DataAccess;
using wsad_app.Areas.Admin.Models.SessionManager;

namespace wsad_app.Areas.Admin.Controllers
{
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
    }
}