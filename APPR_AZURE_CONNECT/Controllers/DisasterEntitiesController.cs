using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APPR_AZURE_CONNECT.Data;
using APPR_AZURE_CONNECT.Models;

namespace APPR_AZURE_CONNECT.Controllers
{
    public class DisasterEntitiesController : Controller
    {
        private readonly AppDbContext _context;

        public DisasterEntitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DisasterEntities
        public async Task<IActionResult> Index()
        {
              return _context.Disaster != null ? 
                          View(await _context.Disaster.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Disaster'  is null.");
        }

        // GET: DisasterEntities/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null || _context.Disaster == null)
            {
                return NotFound();
            }

            var disasterEntity = await _context.Disaster
                .FirstOrDefaultAsync(m => m.StartDate == id);
            if (disasterEntity == null)
            {
                return NotFound();
            }

            return View(disasterEntity);
        }

        // GET: DisasterEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DisasterEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartDate,EndDate,Location,Description,RequiredAidTypes")] DisasterEntity disasterEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disasterEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disasterEntity);
        }

        // GET: DisasterEntities/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null || _context.Disaster == null)
            {
                return NotFound();
            }

            var disasterEntity = await _context.Disaster.FindAsync(id);
            if (disasterEntity == null)
            {
                return NotFound();
            }
            return View(disasterEntity);
        }

        // POST: DisasterEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("StartDate,EndDate,Location,Description,RequiredAidTypes")] DisasterEntity disasterEntity)
        {
            if (id != disasterEntity.StartDate)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disasterEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisasterEntityExists(disasterEntity.StartDate))
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
            return View(disasterEntity);
        }

        // GET: DisasterEntities/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null || _context.Disaster == null)
            {
                return NotFound();
            }

            var disasterEntity = await _context.Disaster
                .FirstOrDefaultAsync(m => m.StartDate == id);
            if (disasterEntity == null)
            {
                return NotFound();
            }

            return View(disasterEntity);
        }

        // POST: DisasterEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            if (_context.Disaster == null)
            {
                return Problem("Entity set 'AppDbContext.Disaster'  is null.");
            }
            var disasterEntity = await _context.Disaster.FindAsync(id);
            if (disasterEntity != null)
            {
                _context.Disaster.Remove(disasterEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisasterEntityExists(DateTime id)
        {
          return (_context.Disaster?.Any(e => e.StartDate == id)).GetValueOrDefault();
        }
    }
}
