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
    public class DonationCentersController : Controller
    {
        private readonly BloodContext _context;

        public DonationCentersController(BloodContext context)
        {
            _context = context;
        }

        // GET: DonationCenters
        public async Task<IActionResult> Index()
        {
            var bloodContext = _context.DonationCenters.Include(d => d.BloodBank);
            return View(await bloodContext.ToListAsync());
        }

        // GET: DonationCenters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DonationCenters == null)
            {
                return NotFound();
            }

            var donationCenter = await _context.DonationCenters
                .Include(d => d.BloodBank)
                .FirstOrDefaultAsync(m => m.DonationCenterID == id);
            if (donationCenter == null)
            {
                return NotFound();
            }

            return View(donationCenter);
        }

        // GET: DonationCenters/Create
        public IActionResult Create()
        {
            ViewData["BloodBankID"] = new SelectList(_context.BloodBanks, "BloodBankID", "BloodBankID");
            return View();
        }

        // POST: DonationCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonationCenterID,Area,Address,IsActive,OpensAt,ClosesAt,BloodBankID,maxDayAppointments")] DonationCenter donationCenter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donationCenter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BloodBankID"] = new SelectList(_context.BloodBanks, "BloodBankID", "BloodBankID", donationCenter.BloodBankID);
            return View(donationCenter);
        }

        // GET: DonationCenters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DonationCenters == null)
            {
                return NotFound();
            }

            var donationCenter = await _context.DonationCenters.FindAsync(id);
            if (donationCenter == null)
            {
                return NotFound();
            }
            ViewData["BloodBankID"] = new SelectList(_context.BloodBanks, "BloodBankID", "BloodBankID", donationCenter.BloodBankID);
            return View(donationCenter);
        }

        // POST: DonationCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonationCenterID,Area,Address,IsActive,OpensAt,ClosesAt,BloodBankID,maxDayAppointments")] DonationCenter donationCenter)
        {
            if (id != donationCenter.DonationCenterID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donationCenter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationCenterExists(donationCenter.DonationCenterID))
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
            ViewData["BloodBankID"] = new SelectList(_context.BloodBanks, "BloodBankID", "BloodBankID", donationCenter.BloodBankID);
            return View(donationCenter);
        }

        // GET: DonationCenters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DonationCenters == null)
            {
                return NotFound();
            }

            var donationCenter = await _context.DonationCenters
                .Include(d => d.BloodBank)
                .FirstOrDefaultAsync(m => m.DonationCenterID == id);
            if (donationCenter == null)
            {
                return NotFound();
            }

            return View(donationCenter);
        }

        // POST: DonationCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DonationCenters == null)
            {
                return Problem("Entity set 'BloodContext.DonationCenters'  is null.");
            }
            var donationCenter = await _context.DonationCenters.FindAsync(id);
            if (donationCenter != null)
            {
                _context.DonationCenters.Remove(donationCenter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationCenterExists(int id)
        {
          return _context.DonationCenters.Any(e => e.DonationCenterID == id);
        }
    }
}
