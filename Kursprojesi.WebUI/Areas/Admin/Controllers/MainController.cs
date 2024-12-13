using Eticaret.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Kursprojesi.WebUI.Areas.Admin.Controllers
{
    public class MainController : Controller
    {
        [Area("Admin"), Authorize(Policy ="AdminPolicy")]
        public IActionResult Index()
        {
            return View();
            
        }

        //private readonly DatabaseContext _context;
        //public MainController(DatabaseContext context)
        //{
        //    _context = context;
        //}
        //public IActionResult Grafik()
        //{
        //    ArrayList deger = new ArrayList();
        //    ArrayList deger1 = new ArrayList();
        //    var urunler = _context.Products.ToList();
        //    urunler.ToList().ForEach(d => deger.Add(d.Name));
        //    urunler.ToList().ForEach(d => deger.Add(d.Stock));
        //    var grafik = new 


        //}
    }
}
