using CareTracker.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CareTracker.Models.AppointmentViewModels
{
    public class AppointmentCreateViewModel
    {


        [Display(Name = "Who is this appointment for?")]
        public List<SelectListItem> DependentList { get; set; }

        [Display(Name = "Please Select Doctor")]
        public List<SelectListItem> DoctorList { get; set; }

        public Appointment Appointment { get; set; }

      

        public AppointmentCreateViewModel(ApplicationDbContext ctx, ApplicationUser user)
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

            //this.DependentList = Dependents
            //                     .OrderBy(d => d.LastName)
            //                     .AsEnumerable()
            //                     .Select(li => new SelectListItem
            //                     {
            //                         Text = li.LastName + "," + li.FirstName,
            //                         Value = li.DependentId.ToString()

            //                     }).ToList();

            this.DoctorList.Insert(0, new SelectListItem
            {
                Text = "Choose Doctor...",
                Value = "0"
            });

            this.DependentList.Insert(0, new SelectListItem
            {
                Text = "Choose Dependent...",
                Value = "0"
            });
        }
    }
}
