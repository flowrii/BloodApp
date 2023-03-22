using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodAppTry.Data;
using BloodAppTry.Models;

namespace BloodAppTry.Controllers
{
    public class BloodBanksController : Controller
    {
        private readonly BloodContext _context;

        public BloodBanksController(BloodContext context)
        {
            _context = context;
        }

        // GET: BloodBanks
        public async Task<IActionResult> Index()
        {
              return View(await _context.BloodBanks.ToListAsync());
        }

        // GET: BloodBanks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BloodBanks == null)
            {
                return NotFound();
            }

            var bloodBank = await _context.BloodBanks
                .FirstOrDefaultAsync(m => m.BloodBankID == id);
            if (bloodBank == null)
            {
                return NotFound();
            }

            return View(bloodBank);
        }

        // GET: BloodBanks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BloodBanks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BloodBankID,Area")] BloodBank bloodBank)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bloodBank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bloodBank);
        }

        // GET: BloodBanks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BloodBanks == null)
            {
                return NotFound();
            }

            var bloodBank = await _context.BloodBanks.FindAsync(id);
            if (bloodBank == null)
            {
                return NotFound();
            }
            return View(bloodBank);
        }

        // POST: BloodBanks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BloodBankID,Area")] BloodBank bloodBank)
        {
            if (id != bloodBank.BloodBankID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloodBank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BloodBankExists(bloodBank.BloodBankID))
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
            return View(bloodBank);
        }

        // GET: BloodBanks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BloodBanks == null)
            {
                return NotFound();
            }

            var bloodBank = await _context.BloodBanks
                .FirstOrDefaultAsync(m => m.BloodBankID == id);
            if (bloodBank == null)
            {
                return NotFound();
            }

            return View(bloodBank);
        }

        // POST: BloodBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BloodBanks == null)
            {
                return Problem("Entity set 'BloodContext.BloodBanks'  is null.");
            }
            var bloodBank = await _context.BloodBanks.FindAsync(id);
            if (bloodBank != null)
            {
                _context.BloodBanks.Remove(bloodBank);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BloodBankExists(int id)
        {
          return _context.BloodBanks.Any(e => e.BloodBankID == id);
        }
    }
}
