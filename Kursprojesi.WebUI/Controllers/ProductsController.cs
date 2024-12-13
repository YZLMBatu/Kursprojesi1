using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kursprojesi.WebUI.Models;
using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;

namespace Kursprojesi.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IService<Product> _serviceProduct;

        public ProductsController(IService<Product> serviceProduct)
        {
            _serviceProduct = serviceProduct;
        }

        public async Task<IActionResult> Index(string q = "")
        {
            var databaseContext = _serviceProduct.GetAllAsync(p => p.IsActive &&  p.Name.Contains(q) || 
            p.Description.Contains(q));
            return View(await databaseContext);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _serviceProduct.GetQueryable()
                .Include(p => p.Brand)
                .Include(p => p.category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var model = new ProductsDetailsViewModel() { Product = product, 
            RelatedProducts = _serviceProduct.GetQueryable().Where(p => p.IsActive && p.CategoryId== product.CategoryId && p.Id != product.Id)
            };
            return View(model);
        }
    }
}
