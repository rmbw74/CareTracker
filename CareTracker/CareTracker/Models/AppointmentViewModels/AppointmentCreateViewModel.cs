using CareTracker.Data;
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
    }
}
