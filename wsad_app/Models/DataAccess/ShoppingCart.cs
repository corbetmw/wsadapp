using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wsad_app.Models.DataAccess
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        [Column("User_Id")]
        public int UserId { get; set; }
        [Column("Product_Id")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("UserId")]
        public virtual User User{ get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}