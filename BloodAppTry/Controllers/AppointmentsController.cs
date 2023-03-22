using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodAppTry.Data;
using BloodAppTry.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BloodAppTry.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly BloodContext _context;

        public AppointmentsController(BloodContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var bloodContext = _context.Appointments.Include(a => a.DonationCenter).Include(a => a.Donor);
            return View(await bloodContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.DonationCenter)
                .Include(a => a.Donor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["DonationCenterID"] = new SelectList(_context.DonationCenters, "DonationCenterID", "DonationCenterID");
            ViewData["DonorID"] = new SelectList(_context.Donors, "DonorID", "DonorID");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DonorID,DonationCenterID,Date,StatusA")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.StatusA = Status.Pending;
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["DonationCenterID"] = new SelectList(_context.DonationCenters, "DonationCenterID", "DonationCenterID", appointment.DonationCenterID);
            ViewData["DonorID"] = new SelectList(_context.Donors, "DonorID", "DonorID", appointment.DonorID);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["DonationCenterID"] = new SelectList(_context.DonationCenters, "DonationCenterID", "DonationCenterID", appointment.DonationCenterID);
            ViewData["DonorID"] = new SelectList(_context.Donors, "DonorID", "DonorID", appointment.DonorID);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DonorID,DonationCenterID,Date,StatusA")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
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
            ViewData["DonationCenterID"] = new SelectList(_context.DonationCenters, "DonationCenterID", "DonationCenterID", appointment.DonationCenterID);
            ViewData["DonorID"] = new SelectList(_context.Donors, "DonorID", "DonorID", appointment.DonorID);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.DonationCenter)
                .Include(a => a.Donor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'BloodContext.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                if (appointment.Date > DateTime.Now)
                    _context.Appointments.Remove(appointment);
                else
                {
                    TempData["ErrorD"] = "You can only delete future appointments!";
                }
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return _context.Appointments.Any(e => e.Id == id);
        }
    }
}
