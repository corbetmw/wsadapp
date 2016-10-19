using wsad_app.Models.DataAccess;
using wsad_app.Models.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wsad_app.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            ShoppingCartManager cartMgr = new ShoppingCartManager();

            IQueryable<ShoppingCart> allItems = cartMgr.GetAllItems(User.Identity.Name);

            List<ShoppingCartViewModel> cartVM = new List<ShoppingCartViewModel>();
            foreach (var item in allItems)
            {
                cartVM.Add(new ShoppingCartViewModel(item));
            }

            return View(cartVM);
        }
    }
}