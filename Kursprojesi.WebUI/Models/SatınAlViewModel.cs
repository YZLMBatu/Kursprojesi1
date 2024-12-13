using Eticaret.Core.Entities;

namespace Kursprojesi.WebUI.Models
{
    public class SatınAlViewModel
    {
        public List<CartLine>? CartProducts { get; set; }
        public decimal TotalPrice { get; set; }
        public List<Address>? Addresses { get; set; }
    }
}
