using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wsad_app.Models.DataAccess;
using wsad_app.Models.SessionCart;

namespace wsad_app.Controllers
{
    public class SessionCartController : Controller
    {
        [Authorize]
        // GET: SessionCart
        public ActionResult Index()
        {
            SessionCartManager cartMgr = new SessionCartManager();

            IQueryable<SessionCart> allItems = cartMgr.GetAllItems(User.Identity.Name);

            List<SessionCartViewModel> cartVM = new List<SessionCartViewModel>();
            foreach (var item in allItems)
            {
                cartVM.Add(new SessionCartViewModel(item));
            }

            return View(cartVM);
        }
    }
}