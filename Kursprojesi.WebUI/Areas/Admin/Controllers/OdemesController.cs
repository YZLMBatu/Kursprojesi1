using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eticaret.Core.Entities;
using Eticaret.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kursprojesi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class OdemesController : Controller
    {
        private readonly DatabaseContext _context;

        public OdemesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Odemes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Odemes.Include(x=>x.AppUser).ToListAsync());
        }

        // GET: Admin/Odemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odeme = await _context.Odemes.Include(s=>s.AppUser).Include(s => s.OdemeLines).ThenInclude(p=>p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (odeme == null)
            {
                return NotFound();
            }

            return View(odeme);
        }

        // GET: Admin/Odemes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Odemes/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Odeme odeme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(odeme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(odeme);
        }

        // GET: Admin/Odemes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odeme = await _context.Odemes.Include(s => s.AppUser).Include(s => s.OdemeLines).ThenInclude(p => p.Product)
                 .FirstOrDefaultAsync(m => m.Id == id);
            if (odeme == null)
            {
                return NotFound();
            }
            ViewBag.OrderStates = new SelectList(Enum.GetValues(typeof(EnumOrderState)));
            return View(odeme);
        }

        // POST: Admin/Odemes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Odeme odeme)
        {
            if (id != odeme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(odeme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OdemeExists(odeme.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("","Hata");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.OrderStates = new SelectList(Enum.GetValues(typeof(EnumOrderState)));
            return View(odeme);
        }

        // GET: Admin/Odemes/Delete/5
        public  async Task<IActionResult> Delete(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           var odeme = await _context.Odemes.Include(s => s.AppUser).FirstOrDefaultAsync(m=> m.Id == id);
            if (odeme == null)
            {
                return NotFound();
            }
            return View(odeme);
        }

        // POST: Admin/Odemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        

        private bool OdemeExists(int id)
        {
            return _context.Odemes.Any(e => e.Id == id);
        }
    }
}
