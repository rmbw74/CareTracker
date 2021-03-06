﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareTracker.Data;
using CareTracker.Models;
using CareTracker.Models.PrescriptionViewModels;
using Microsoft.AspNetCore.Identity;

namespace CareTracker.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PrescriptionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Prescriptions
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var model = new DependentAllPrescriptionsViewModel();

            model.DependentPrescriptions = GetDependentUserPrescriptions(_context, user);
            
            return View(model);
        }

       

        // GET: Prescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescription
                .Include(p => p.Dependent)
                .Include(p => p.Doctor)
                .SingleOrDefaultAsync(m => m.PrescriptionId == id);
            if (prescription == null)
            {
                return NotFound();
            }
            var model = new PrescriptionDetailsViewModel();
            model.Prescription = prescription;
            return View(model);
        }

        // GET: Prescriptions/Create
        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var model = new PrescriptionCreateViewModel(_context, user);

            return View(model);
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrescriptionId,DrugName,Dosage,Frequency,PharmacyAddress,PharmacyPhoneNumber,PrescriptionNotes,PrescriptionActive,DependentId,DoctorId")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DependentId"] = new SelectList(_context.Dependent, "DependentId", "FirstName", prescription.DependentId);
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "LastName", prescription.DoctorId);
            return View(prescription);
        }

        // GET: Prescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescription.SingleOrDefaultAsync(m => m.PrescriptionId == id);
            if (prescription == null)
            {
                return NotFound();
            }
            var model = new PrescriptionEditViewModel();
            model.Prescription = prescription;
            return View(model);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrescriptionId,DrugName,Dosage,Frequency,PharmacyAddress,PharmacyPhoneNumber,PrescriptionNotes,PrescriptionActive,DependentId,DoctorId")] Prescription prescription)
        {
            if (id != prescription.PrescriptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescriptionExists(prescription.PrescriptionId))
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
            ViewData["DependentId"] = new SelectList(_context.Dependent, "DependentId", "FirstName", prescription.DependentId);
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "LastName", prescription.DoctorId);
            return View(prescription);
        }

        // GET: Prescriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescription
                .Include(p => p.Dependent)
                .Include(p => p.Doctor)
                .SingleOrDefaultAsync(m => m.PrescriptionId == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prescription = await _context.Prescription.SingleOrDefaultAsync(m => m.PrescriptionId == id);
            _context.Prescription.Remove(prescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrescriptionExists(int id)
        {
            return _context.Prescription.Any(e => e.PrescriptionId == id);
        }

        public ICollection<DependentPrescription> GetDependentUserPrescriptions (ApplicationDbContext context, ApplicationUser user)
        {
            return (from p in context.Prescription
                    join doc in context.Doctor
                    on p.DoctorId equals doc.DoctorId
                    join dep in context.Dependent
                    on p.DependentId equals dep.DependentId
                    join du in context.DependentUser
                    on p.DependentId equals du.DependentId
                    where du.User == user
                    select new DependentPrescription
                    {
                        DependentFirstName = dep.FirstName,
                        DependentLastName = dep.LastName,
                        DependentId = dep.DependentId,
                        DoctorFirstName = doc.FirstName,
                        DoctorLastName = doc.LastName,
                        DoctorId = doc.DoctorId,
                        PrescriptionId = p.PrescriptionId,
                        DrugName = p.DrugName,
                        Dosage = p.Dosage,
                        Frequency = p.Frequency,
                        PharmacyPhoneNumber = p.PharmacyPhoneNumber,
                        PrescriptionActive = p.PrescriptionActive
                    }).OrderBy(p => p.DependentId).OrderBy(p => p.PrescriptionActive).ToList();
        }
        public async Task<IActionResult> PrescriptionHistory(int id)
        {
            var model = new PrescriptionHistoryViewModel();

            model.Dependent = await _context.Dependent.Where(d => d.DependentId == id).SingleOrDefaultAsync();

            model.Prescriptions = await _context.Prescription
                                                .Include("Doctor")
                                                .Where(p => p.DependentId == id).ToListAsync();

            return View(model);
        }
    }
}
