using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;
using Eticaret.Service.Concrate;
using Kursprojesi.WebUI.ExtensionsMethods;
using Kursprojesi.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kursprojesi.WebUI.Controllers
{
    public class SepetController : Controller
    {
        private readonly IService<Product> _serviceProduct;
        private readonly IService<Address> _serviceAddress;
        private readonly IService<AppUser> _serviceAppUser;
        private readonly IService<Odeme> _serviceOdeme;
        private readonly IService<OdemeLine> _serviceOdemeLine;

        public SepetController(IService<Product> serviceProduct, IService<Address> serviceAddress, IService<AppUser> serviceAppUser, IService<Odeme> serviceOdeme, IService<OdemeLine> serviceOdemeLine)
        {
            _serviceProduct = serviceProduct;
            _serviceAddress = serviceAddress;
            _serviceAppUser = serviceAppUser;
            _serviceOdeme = serviceOdeme;
            _serviceOdemeLine = serviceOdemeLine;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            var model = new CartViewModel()
            {
                CartLines = cart.CartLines,
                TotalPrice = cart.TotalPrice()
            };
            return View(model);
        }
        public IActionResult Add(int ProductId ,int quantity = 1)
        {
            var product = _serviceProduct.Find(ProductId);
            if (product != null)
            {
                var cart = GetCart();
                cart.AddProduct(product,quantity);
                HttpContext.Session.SetJson("Cart",cart);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return RedirectToAction("Index");
        }
        public IActionResult Update(int ProductId)
        {
            var product = _serviceProduct.Find(ProductId);
            if (product != null)
            {
                var cart = GetCart();
                cart.RemoveProduct(product);
                HttpContext.Session.SetJson("Cart",cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int ProductId)
        {
            var product = _serviceProduct.Find(ProductId);
            if (product != null)
            {
                var cart = GetCart();
                cart.RemoveProduct(product);
                HttpContext.Session.SetJson("Cart",cart);
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public  async Task<IActionResult> SatınAlAsync()
        {
            var cart = GetCart();
            var appUser = await _serviceAppUser.GetAsync(u=> u.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            var addresses = await _serviceAddress.GetAllAsync(d =>d.AppUserId == appUser.Id && d.IsActive);
            var model = new SatınAlViewModel()
            {
                CartProducts = cart.CartLines,
                TotalPrice = cart.TotalPrice(),
                Addresses = addresses
            };
            return View(model);
        }
        [Authorize,HttpPost]
        public  async Task<IActionResult> SatınAlAsync(string CardNumber, string CardMonth, string CardYear,string CVV,string DeliveryAddress, string BillingAddress)
        {
            var cart = GetCart();
            var appUser = await _serviceAppUser.GetAsync(u=> u.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (appUser == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            var addresses = await _serviceAddress.GetAllAsync(d =>d.AppUserId == appUser.Id && d.IsActive);
            var model = new SatınAlViewModel()
            {
                CartProducts = cart.CartLines,
                TotalPrice = cart.TotalPrice(),
                Addresses = addresses
            };
            if (string.IsNullOrWhiteSpace(CardNumber) || string.IsNullOrWhiteSpace(CardYear) || string.IsNullOrWhiteSpace(CVV) || string.IsNullOrWhiteSpace(CardMonth) || string.IsNullOrWhiteSpace(DeliveryAddress) || string.IsNullOrWhiteSpace(BillingAddress))
            {
                return View(model);
            }
            var teslimadresi = addresses.FirstOrDefault(b=> b.AddressGuid.ToString() == DeliveryAddress);
            var faturaadres = addresses.FirstOrDefault(b=> b.AddressGuid.ToString() == BillingAddress);

            // Ödeme işlemi

            var siparis = new Odeme
            {
                AppUserId = appUser.Id,
                BillingAddress = $"{faturaadres}",//BillingAddress,
                CustomerId = appUser.UserGuid.ToString(),
                DeliveryAddress = DeliveryAddress,
                OrderDate = DateTime.Now,
                TotalPrice = cart.TotalPrice(),
                OrderNo = Guid.NewGuid().ToString(),
                OdemeLines = []
            };

            foreach (var item in cart.CartLines)
            {
                siparis.OdemeLines.Add(new OdemeLine
                {
                    ProductId = item.Product.Id,
                    OrderId = siparis.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price,
                });
            }

            try
            {
                await _serviceOdeme.AddAsync(siparis);
                var sonuc = await _serviceOdeme.SaveChangesAsync();
                if (sonuc > 0)
                {
                    HttpContext.Session.Remove("Cart");
                    return RedirectToAction("Thanks");  
                }
            }
            catch (Exception)
            {

                TempData["Message"] = "Hata";
            }
            return View(model);
        }
        public IActionResult Thanks()
        {
            
            return View();
        }
        private CartService GetCart()
        {
            return HttpContext.Session.GetJson<CartService>("Cart")?? new CartService();
        }
    }
}
