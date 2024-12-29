using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;
using Kursprojesi.WebUI.ExtensionsMethods;
using Microsoft.AspNetCore.Mvc;

namespace Kursprojesi.WebUI.Controllers
{
    public class FavoriController : Controller
    {
        private readonly IService<Product> _service;

        public FavoriController(IService<Product> service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var favoriler = GetFavori();
            return View(favoriler);
        }
        private List<Product> GetFavori()
        {
            return HttpContext.Session.GetJson<List<Product>>("GetFavori") ?? [];

        }
        public IActionResult Add(int ProductId)
        {
            var favoriler = GetFavori();
            var product = _service.Find(ProductId);
            if (product != null && !favoriler.Any(p=>p.Id == ProductId))
            {
                favoriler.Add(product);
                HttpContext.Session.SetJson("GetFavori",favoriler);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int ProductId)
        {
            var favoriler = GetFavori();
            var product = _service.Find(ProductId);
            if (product != null && favoriler.Any(p=>p.Id == ProductId))
            {
                favoriler.RemoveAll(i=>i.Id == product.Id);
                HttpContext.Session.SetJson("GetFavori",favoriler);
            }
            return RedirectToAction("Index");
        }
    }
}
