using System;
using System.ComponentModel.DataAnnotations;

namespace CareTracker.Models
{
    public class Appointment
    {
        [Key]
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

        //foreign keys 
        [Required]
        public int DependentId { get; set; }
        public virtual Dependent Dependent { get; set; }
        [Required]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}