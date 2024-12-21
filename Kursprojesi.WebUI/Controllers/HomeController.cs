using Eticaret.Core.Entities;
using Eticaret.Service.Abstract;
using Kursprojesi.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace Kursprojesi.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Product> _serviceProduct;
        private readonly IService<Slide> _serviceSlide;
        private readonly IService<News> _serviceNews;
        private readonly IService<Contact> _serviceContact;

        public HomeController(IService<Product> serviceProduct, IService<Slide> serviceSlide, IService<News> serviceNews, IService<Contact> serviceContact)
        {
            _serviceProduct = serviceProduct;
            _serviceSlide = serviceSlide;
            _serviceNews = serviceNews;
            _serviceContact = serviceContact;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel()
            {
                Slides = await _serviceSlide.GetAllAsync(),
                News = await _serviceNews.GetAllAsync(),
                Products = await _serviceProduct.GetAllAsync(p => p.IsActive && p.IsHome)

            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("AccessDenied")]
        public IActionResult AccessDenied() 
        {
            return View();
        }
       

        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult SifremiUnuttum()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactUs(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceContact.AddAsync(contact);
                    var sonuc = await _serviceContact.SaveChangesAsync();
                    if (sonuc > 0)
                    {
                        TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>MESAJINIZ GÖNDERİLMİŞTİR</strong> 
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div>";
                        //await MailHelper.SendMailAsync(contact);
                        return RedirectToAction("ContactUs");

                    }
                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            return View(contact);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
