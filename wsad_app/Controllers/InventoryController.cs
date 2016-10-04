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
            //Build Inventory Mangaer
            InventoryManager invMgr = new InventoryManager();

            //egt products from mangfere
            IQueryable<Product> allProducts;
            allProducts = InventoryManager.GetAllProducts();
            //Build viewmodesl fo cprocuts
            List<ProductViewModel> productVM = new List<ProductViewModel>();
            foreach (Product p in allProducts)
            {
                productVM.Add(new ProductViewModel(p));
            }
            //Send procut viewmodels to view

            return View();
        }
        
        [HttpPost]
        public ActionResult Index(List<ProductViewModel> allProductsVM)
        {
            //filter all procuts biewmodel to be slecetony only
            allProductsVM = allProductsVM.Where(p => p.IsSelected == true).ToList();
            //build shpping car manger
            ShoppingCartManager cartMgr = new ShoppingCartManager();
            //add selected productss to shpping cart
            foreach (ProductViewModel prodVM in allProductsVM)
            {
                cartMgr.AddToCart(User.Identity.Name, prodVM.Id);
            }
            //redirect ot shopping cart view
            return RedirectToAction("Index", "Index")
        }
    }
}