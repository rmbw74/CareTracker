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
using CareTracker.Models.AppointmentViewModels;

namespace CareTracker.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // This task retrieves the currently authenticated user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Appointment.Include(a => a.Dependent).Include(a => a.Doctor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Dependent)
                .Include(a => a.Doctor)
                .SingleOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var model = new AppointmentCreateViewModel(_context, user);
           
            return View(model);
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,AppointmentDate,AppointmentTime,AppointmentReason,AppointmentAddress,AppointmentPhoneNumber,AppointmentNotes,DependentId,DoctorId")] Appointment appointment)
        {
       
            if (ModelState.IsValid)
            {
                //grab the DependentId and the DoctorId for the current appointment
                int CurrentAppointmentDependent = appointment.DependentId;
                int CurrentAppointmentDoctor = appointment.DoctorId;

                //check to see if dependent is already a patient of the doctor on the appointment
                if(!IsDependentAPatient(CurrentAppointmentDependent, CurrentAppointmentDoctor))
                {
                    //Create a new DependentDoctor Entry
                    DependentDoctor DepDoctor = new DependentDoctor()
                    {
                        DependentId = CurrentAppointmentDependent,
                        DoctorId = CurrentAppointmentDoctor
                    };
                    //add new Dependent Doctor to the DB
                    _context.Add(DepDoctor);
                    await _context.SaveChangesAsync();
                }

                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.SingleOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["DependentId"] = new SelectList(_context.Dependent, "DependentId", "FirstName", appointment.DependentId);
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "LastName", appointment.DoctorId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,AppointmentDate,AppointmentTime,AppointmentReason,AppointmentAddress,AppointmentPhoneNumber,AppointmentNotes,DependentId,DoctorId")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
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
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["DependentId"] = new SelectList(_context.Dependent, "DependentId", "FirstName", appointment.DependentId);
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "DoctorId", "Address", appointment.DoctorId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Dependent)
                .Include(a => a.Doctor)
                .SingleOrDefaultAsync(m => m.AppointmentId == id);
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
            var appointment = await _context.Appointment.SingleOrDefaultAsync(m => m.AppointmentId == id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.AppointmentId == id);
        }

        public bool IsDependentAPatient(int _dependentId, int _doctorId )
        {
            var IsPatient = _context.DependentDoctor
                 .Where(DD => DD.DependentId == _dependentId && DD.DoctorId == _doctorId)
                 .FirstOrDefault();

            if(IsPatient == null)
            {
                return false;
            }
            return true;
        }
       
        public async Task<IActionResult> AllAppointments()
        {

            ApplicationUser user = await GetCurrentUserAsync();
            var model = new AllAppointmentsViewModel();


            model.Appointments = GetDependentAppointments(user);
                                           


                return View(model);
            
        }
            public IEnumerable<DependentAppointment> GetDependentAppointments(ApplicationUser user)
            {
            return (from a in _context.Appointment
                    join doc in _context.Doctor on a.DoctorId equals doc.DoctorId
                    join d in _context.Dependent on a.DependentId equals d.DependentId
                    join du in _context.DependentUser on a.DependentId equals du.DependentId
                    where du.User == user && a.AppointmentDate >= DateTime.Now
                    select new DependentAppointment
                    {
                        AppointmentAddress = a.AppointmentAddress,
                        AppointmentDate = a.AppointmentDate,
                        AppointmentId = a.AppointmentId,
                        AppointmentNotes = a.AppointmentNotes,
                        AppointmentPhone = a.AppointmentPhoneNumber,
                        AppointmentReason = a.AppointmentReason,
                        AppointmentTime = a.AppointmentTime,
                        DependentId = d.DependentId,
                        DependentFirstName = d.FirstName,
                        DependentLastName = d.LastName,
                        DoctorFirstName = doc.FirstName,
                        DoctorLastName = doc.LastName,
                        DoctorId = doc.DoctorId


                    }).ToList().OrderBy(x => x.AppointmentDate);
            }
    }
}
