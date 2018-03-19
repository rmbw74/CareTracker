using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareTracker.Data;
using CareTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CareTracker.Models.DoctorsViewModels;

namespace CareTracker.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DoctorsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: Doctors/ShowAll
        [Authorize]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = sortOrder == "name_desc" ? "name" : "name_desc";
            ViewData["LastNameSortParam"] = sortOrder == "LastName_desc" ? "LastName" : "LastName_desc";
            ViewData["HospitalSortParam"] = sortOrder == "Hospital_desc" ? "Hospital" : "Hospital_desc";
            ViewData["SpecialtySortParam"] = sortOrder == "Specialty_desc" ? "Specialty" : "Specialty_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            // define a variable to hold doctors
            var doctors = from d in _context.Doctor
                           select d;
            //check to see if the user has entered anything in seach..
            if (!String.IsNullOrEmpty(searchString))
            {
                //if searchString is not null, check to see if any af the following columns contain the searchString
                doctors = doctors.Where(d => d.FirstName.Contains(searchString)|| d.LastName.Contains(searchString)
                                            || d.Hospital.Contains(searchString) || d.Specialty.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    doctors = doctors.OrderByDescending(d => d.FirstName);
                    break;
                case "name":
                    doctors = doctors.OrderBy(d => d.FirstName);
                    break;
                case "LastName_desc":
                    doctors = doctors.OrderByDescending(d => d.LastName);
                    break;
                case "LastName":
                    doctors = doctors.OrderBy(d => d.LastName);
                    break;
                case "Hospital_desc":
                    doctors = doctors.OrderByDescending(d => d.Hospital);
                    break;
                case "Hospital":
                    doctors = doctors.OrderBy(d => d.Hospital);
                    break;
                case "Specialty_desc":
                    doctors = doctors.OrderByDescending(d => d.Specialty);
                    break;
                case "Specialty":
                    doctors = doctors.OrderBy(d => d.Specialty);
                    break;
                default:
                    doctors = doctors.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Doctor>.CreateAsync(doctors.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Doctors/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor
                .SingleOrDefaultAsync(m => m.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorId,FirstName,LastName,Hospital,Specialty,Address,PhoneNumber")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor.SingleOrDefaultAsync(m => m.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorId,FirstName,LastName,Hospital,Specialty,Address,PhoneNumber")] Doctor doctor)
        {
            if (id != doctor.DoctorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.DoctorId))
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
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor
                .SingleOrDefaultAsync(m => m.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.Doctor.SingleOrDefaultAsync(m => m.DoctorId == id);
            _context.Doctor.Remove(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctor.Any(e => e.DoctorId == id);
        }

        //GET: Doctors/DependentDoctors/5
        [Authorize]
        public IActionResult DependentDoctors(int id)
        {
          //instantiate a new view model
          var model = new DependentDoctorsViewModel(id, _context );
            
            

            return View(model);
        }

        public ICollection<Doctor> GetDependentDoctors(int id)
        {
            return (from d in _context.Doctor
                    join dep in _context.DependentDoctor
                    on d.DoctorId equals dep.DoctorId
                    where dep.DependentId == id
                    select new Doctor
                    {
                        DoctorId = d.DoctorId,
                        FirstName = d.FirstName,
                        LastName = d.LastName,
                        Hospital = d.Hospital,
                        Specialty = d.Specialty,
                        Address= d.Address,
                        PhoneNumber = d.PhoneNumber
                    }).ToList();
        }
    }
}
