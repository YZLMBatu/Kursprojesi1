using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;

namespace Eticaret.Service.Concrate
{
    public class CartService : ICartService
    {
        public List<CartLine> CartLines = new ();
        public void AddProduct(Product product, int quantity)
        {
            var urun = CartLines.FirstOrDefault(p=>p.Product.Id == product.Id);
            if (urun != null)
            {
                urun.Quantity += quantity;
            }
            else
            {
                CartLines.Add(new CartLine
                { 
                    Quantity = quantity,
                    Product = product
                });
            }
        }

        public void ClerAll()
        {
            CartLines.Clear();
        }

        public void RemoveProduct(Product product)
        {
            CartLines.RemoveAll(p=> p.Product.Id == product.Id);
        }

        public decimal TotalPrice()
        {
           return CartLines.Sum(c => c.Product.Price * c.Quantity);
        }

        public void UpdateProduct(Product product, int quantity)
        {
             var urun = CartLines.FirstOrDefault(p=>p.Product.Id == product.Id);
            if (urun != null)
            {
                urun.Quantity = quantity;
            }
            else
            {
                CartLines.Add(new CartLine
                { 
                    Quantity = quantity,
                    Product = product
                });
            }
        }
    }
}
