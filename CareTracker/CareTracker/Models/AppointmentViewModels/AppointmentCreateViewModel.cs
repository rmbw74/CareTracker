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
        public int AppointmentId { get; set; }

        public ICollection<Dependent> Dependents { get; set; }

        [Required]
        [Display(Name = "Who is this appointment for?")]
        public List<SelectListItem> DependentList { get; set; }

        [Required]
        [Display(Name = "Please Select Doctor")]
        public List<SelectListItem> DoctorList { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Day")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Time")]
        public DateTime AppointmentTime { get; set; }

        [Required]
        [Display(Name = "Reason For Visit")]
        public string AppointmentReason { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string AppointmentAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string AppointmentPhoneNumber { get; set; }

        [Display(Name = "Notes")]
        public string AppointmentNotes { get; set; }

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

            this.Dependents = (from d in ctx.Dependent
                               join du in ctx.DependentUser
                               on d.DependentId equals du.DependentId
                               where du.User == user
                               select new Dependent
                               {
                                   FirstName = d.FirstName,
                                   LastName = d.LastName,
                                   Birthday = d.Birthday
                                   
                               }).ToList();

            this.DependentList = Dependents
                                 .OrderBy(d => d.LastName)
                                 .AsEnumerable()
                                 .Select(li => new SelectListItem
                                 {
                                     Text = li.LastName + "," + li.FirstName,
                                     Value = li.DependentId.ToString()

                                 }).ToList();

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
