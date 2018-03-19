using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CareTracker.Data;

namespace CareTracker.Models.PrescriptionViewModels
{
    public class PrescriptionCreateViewModel
    {
        [Display(Name = "Who is this Prescription for?")]
        public List<SelectListItem> DependentList { get; set; }

        [Display(Name = "Who was the Prescribing Doctor?")]
        public List<SelectListItem> DoctorList { get; set; }

        public Prescription Prescription { get; set; }


        public PrescriptionCreateViewModel(ApplicationDbContext ctx, ApplicationUser user)
        {
            this.DoctorList = ctx.Doctor
                                   .OrderBy(d => d.LastName)
                                   .AsEnumerable()
                                   .Select(li => new SelectListItem
                                   {
                                       Text = li.LastName + "," + li.FirstName,
                                       Value = li.DoctorId.ToString()
                                   }).ToList();

            this.DependentList = (from d in ctx.Dependent
                                  join du in ctx.DependentUser
                                  on d.DependentId equals du.DependentId
                                  where du.User == user
                                  select new Dependent
                                  {
                                      FirstName = d.FirstName,
                                      LastName = d.LastName,
                                      DependentId = d.DependentId

                                  })
                                  .OrderBy(d => d.LastName)
                                  .AsEnumerable()
                                 .Select(li => new SelectListItem
                                 {
                                     Text = li.LastName + "," + li.FirstName,
                                     Value = li.DependentId.ToString()
                                 })
                                 .ToList();

            //Prescription.PrescriptionActive = true;
        }
    }
}
