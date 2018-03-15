using CareTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareTracker.Models.DoctorsViewModels
{
    public class DependentDoctorsViewModel
    {
        public Dependent Dependent { get; set; }
     

        public ICollection<Doctor> Doctors {get; set;}

        public DependentDoctorsViewModel(int id, ApplicationDbContext ctx)
        {
            this.Dependent = (from d in ctx.Dependent
                              where d.DependentId == id
                              select new Dependent
                              {DependentId = d.DependentId,
                               FirstName = d.FirstName
                              }).Single();
            this.Doctors = (from d in ctx.Doctor
                            join dep in ctx.DependentDoctor
                            on d.DoctorId equals dep.DoctorId
                            where dep.DependentId == id
                            select new Doctor
                            {
                                DoctorId = d.DoctorId,
                                FirstName = d.FirstName,
                                LastName = d.LastName,
                                PhoneNumber = d.PhoneNumber,
                                Hospital = d.Hospital,
                                Specialty = d.Specialty
                            }).ToList();
        }
    }
}
