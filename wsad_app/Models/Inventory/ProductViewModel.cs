using wsad_app.Models.DataAccess;

namespace wsad_app.Models.Inventory
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {

        }
        public ProductViewModel(Product p)
        {
            Id = p.Id;
            Name = p.Name;
            UnitPrice = p.UnitPrice;
            Description = p.Description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }
    }
}