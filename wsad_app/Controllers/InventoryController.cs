using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wsad_app.Models.Business;
using wsad_app.Models.DataAccess;
using wsad_app.Models.Inventory;

namespace wsad_app.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Index()
        {
            //Build ScheduleManager
            InventoryManager invMgr = new InventoryManager();

            //Get Products from Manager
            IQueryable<Product> allProducts;
            allProducts = invMgr.GetAllProducts();

            //Build ViewModels of Products
            List<ProductViewModel> productVM = new List<ProductViewModel>();
            foreach (Product p in allProducts)
            {
                productVM.Add(new ProductViewModel(p));
            }

            //Send Product View Models to View
            return View(productVM);
        }
        
        [HttpPost]
        public ActionResult Index(List<ProductViewModel> allProductsVM)
        {
            //Filter all Products View Model to be selected only
            allProductsVM = allProductsVM.Where(p => p.IsSelected == true).ToList();

            //Build ShoppingCart Manager
            ShoppingCartManager cartMgr = new ShoppingCartManager();

            //Add Selected Products to the ShoppingCart
            foreach (ProductViewModel prodVM in allProductsVM)
            {
                cartMgr.AddToCart(User.Identity.Name, prodVM.Id);
            }

            //Redirect to Shopping Cart View
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}