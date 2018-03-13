using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareTracker.Data;
using CareTracker.Models;
using CareTracker.Models.DependentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareTracker.Controllers
{
    public class SummaryController : Controller
    {
        //create property for database context
        private readonly ApplicationDbContext _context;
       
        //contsructor for controller -> injecting database context
        public SummaryController(ApplicationDbContext context)
        {
            //set database context to private variable
            _context = context;
        }

        public IEnumerable<SummaryAppointment> GetSummaryAppointments(int id)
        {
            return (from a in _context.Appointment
                    join d in _context.Doctor
                    on a.DoctorId equals d.DoctorId
                    where a.DependentId == id && a.AppointmentDate >= DateTime.Now
                    select new SummaryAppointment
                    {
                        AppointmentId = a.AppointmentId,
                        AppointmentDate = a.AppointmentDate,
                        AppointmentTime = a.AppointmentTime,
                        AppointmentReason = a.AppointmentReason,
                        AppointmentDoctor = d.LastName,
                        AppointmentPhoneNum = a.AppointmentPhoneNumber,
                        AppointmentDoctorId = d.DoctorId
                    }).ToList().OrderBy(a => a.AppointmentDate).Take(3);
        }

        public IEnumerable<SummaryPrescription> GetSummaryPrescriptions(int id)
        {
            return (from p in _context.Prescription
                    join d in _context.Doctor
                    on p.DoctorId equals d.DoctorId
                    where p.DependentId == id && p.PrescriptionActive == true
                    select new SummaryPrescription
                    {
                        DrugName = p.DrugName,
                        DoctorId = p.DoctorId,
                        Dosage = p.Dosage,
                        Frequency = p.Frequency,
                        PharmacyPhoneNumber = p.PharmacyPhoneNumber, 
                        PrescriptionNotes = p.PrescriptionNotes,
                        PrescribingDoctor = d.LastName,
                        PrescriptionId = p.PrescriptionId

                    }).ToList();
        }



        //GET: Summary/Show/5
        [Authorize]
        public async Task<IActionResult> Show(int id)
        {
          //get current dependent using id
            var dependent = await _context.Dependent
                .SingleOrDefaultAsync(m => m.DependentId == id);
            //instantiate new model
            var model = new DependentSummaryViewModel();

            //set model dependent
            model.Dependent = dependent;

            //calculate age for dependent
            int currentyear = DateTime.Now.Year;
            int Dependent_BirthYear = dependent.Birthday.Year;
            model.Age = currentyear - Dependent_BirthYear;

            //get dependent appointments by id
            model.Appointments = GetSummaryAppointments(id);

            //get model appointments by id
            model.Prescriptions = GetSummaryPrescriptions(id);

            return View(model);
        }
    }
}