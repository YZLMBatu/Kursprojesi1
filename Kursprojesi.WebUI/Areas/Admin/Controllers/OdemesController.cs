using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eticaret.Core.Entities;
using Eticaret.Data;
using Microsoft.AspNetCore.Authorization;

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

            var odeme = await _context.Odemes.FindAsync(id);
            if (odeme == null)
            {
                return NotFound();
            }
            return View(odeme);
        }

        // POST: Admin/Odemes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderNo,TotalPrice,AppUserId,CustomerId,BillingAddress,DeliveryAddress,OrderDate")] Odeme odeme)
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
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(odeme);
        }

        // GET: Admin/Odemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odeme = await _context.Odemes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (odeme == null)
            {
                return NotFound();
            }

            return View(odeme);
        }

        // POST: Admin/Odemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var odeme = await _context.Odemes.FindAsync(id);
            if (odeme != null)
            {
                _context.Odemes.Remove(odeme);
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
