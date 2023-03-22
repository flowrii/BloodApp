using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodAppTry.Data;
using BloodAppTry.Models;
using Microsoft.AspNetCore.Http;

namespace BloodAppTry.Controllers
{
    public class DonorsController : Controller
    {
        private readonly BloodContext _context;

        public DonorsController(BloodContext context)
        {
            _context = context;
        }

        // GET: Donors
        public async Task<IActionResult> Index()
        {
            return _context.Donors != null ?
                        View(await _context.Donors.ToListAsync()) :
                        Problem("Entity set 'BloodContext.Donors'  is null.");
        }

        // GET: Donors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Donors == null)
            {
                return NotFound();
            }

            var donor = await _context.Donors
                .FirstOrDefaultAsync(m => m.DonorID == id);
            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }

        // GET: Donors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Donors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonorID,Username,Password,FirstName,LastName,Email,Area,BloodGroup")] Donor donor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donor);
        }

        // GET: Donors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Donors == null)
            {
                return NotFound();
            }

            var donor = await _context.Donors.FindAsync(id);
            if (donor == null)
            {
                return NotFound();
            }
            return View(donor);
        }

        // POST: Donors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonorID,Username,Password,FirstName,LastName,Email,Area,BloodGroup")] Donor donor)
        {
            if (id != donor.DonorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donor);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetString("Username", donor.Username);
                }
                catch (DbUpdateConcurrencyException)

                {
                    if (!DonorExists(donor.DonorID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (HttpContext.Session.GetString("Type") == "Admin")
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction("Index", "Home");
            }
            return View(donor);
        }

        // GET: Donors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Donors == null)
            {
                return NotFound();
            }

            var donor = await _context.Donors
                .FirstOrDefaultAsync(m => m.DonorID == id);
            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }

        // POST: Donors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Donors == null)
            {
                return Problem("Entity set 'BloodContext.Donors'  is null.");
            }
            var donor = await _context.Donors.FindAsync(id);
            if (donor != null)
            {
                _context.Donors.Remove(donor);
            }

            await _context.SaveChangesAsync();
            if (HttpContext.Session.GetString("Type") == "Donor")
            {
                HttpContext.Session.Remove("Username");
                HttpContext.Session.Remove("Type");
                HttpContext.Session.Remove("ID");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DonorExists(int id)
        {
            return (_context.Donors?.Any(e => e.DonorID == id)).GetValueOrDefault();
        }
    }
}
