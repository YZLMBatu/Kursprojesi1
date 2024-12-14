using System.ComponentModel.DataAnnotations;

namespace Eticaret.Core.Entities
{
    public class OdemeLine :IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Sipariş No")]
        public int  OrderId { get; set; }
        [Display(Name = "Sipariş ")]
        public Odeme? Odeme { get; set; }
        [Display(Name = "Ürün No")]
        public int ProductId { get; set; }
        [Display(Name = "Ürün")]
        public Product? Product { get; set; }
        [Display(Name = "Tutar")]
        public int Quantity { get; set; }
        [Display(Name = "Birim Fiyat")]
        public decimal UnitPrice { get; set; }
}
}
