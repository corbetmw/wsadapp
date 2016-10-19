using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsad_app.Models.Inventory;

namespace wsad_app.Models.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel(DataAccess.ShoppingCart item)
        {
            Id = item.Id;
            UserId = item.UserId;
            ProductId = item.ProductId;
            Quantity = item.Quantity;
            DateAdded = item.DateAdded;
            IsActive = item.IsActive;
            Product = new ProductViewModel(item.Product);
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsActive { get; set; }

        public ProductViewModel Product { get; set; }
    }
}