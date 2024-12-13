using Eticaret.Core.Entities;

namespace Kursprojesi.WebUI.Models
{
    public class SiparislerimViewModel
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Product>? RelatedProducts { get; set; }
    }
}
