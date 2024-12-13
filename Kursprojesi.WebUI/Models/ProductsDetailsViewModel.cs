using Eticaret.Core.Entities;

namespace Kursprojesi.WebUI.Models
{
    public class ProductsDetailsViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Product>? RelatedProducts {  get; set; }

    }
}
