using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wsad_app.Models.Business;
using wsad_app.Models.DataAccess;
using wsad_app.Models.Schedule;

namespace wsad_app.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult Index()
        {
            //Build ScheduleManager
            ScheduleManager schMgr = new ScheduleManager();

            //Get Sessions from Manager
            IQueryable<Session> allSessions;
            allSessions = schMgr.GetAllSessions();

            //Build ViewModels of Sessions
            List<SessionViewModel> sessionVM = new List<SessionViewModel>();
            foreach (Session p in allSessions)
            {
                sessionVM.Add(new SessionViewModel(p));
            }

            //Send Session View Models to View
            return View(sessionVM);
        }

        [HttpPost]
        public ActionResult Index(List<SessionViewModel> allSessionsVM)
        {
            //Filter all Products View Model to be selected only
            allSessionsVM = allSessionsVM.Where(p => p.IsSelected == true).ToList();

            //Build ShoppingCart Manager
            SessionCartManager cartMgr = new SessionCartManager();

            //Add Selected Products to the ShoppingCart
            foreach (SessionViewModel sessVM in allSessionsVM)
            {
                cartMgr.AddToCart(User.Identity.Name, sessVM.Id);
            }

            //Redirect to Shopping Cart View
            return RedirectToAction("Index", "SessionCart");
        }
    }
}