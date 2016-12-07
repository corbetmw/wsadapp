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

            IQueryable<SessionCart> allItems = cartMgr.GetAllSessionsByUser(User.Identity.Name,null);

            List<SessionCartViewModel> cartVM = new List<SessionCartViewModel>();
            foreach (var item in allItems)
            {
                cartVM.Add(new SessionCartViewModel(item));
            }

            return View(cartVM);
        }

        public ActionResult Delete(int id)
        {
            SessionCartManager sessCrtMgr = new SessionCartManager();

            SessionCart sessCart = sessCrtMgr.GetSessionCart(id);

            SessionCartViewModel sessCartVM = new SessionCartViewModel(sessCart);

            return View(sessCartVM);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            SessionCartManager sessCartMgr = new SessionCartManager();
            sessCartMgr.DeleteRegistration(id);

            //Does the Delete
            return RedirectToAction("Index");
        }
    }
}