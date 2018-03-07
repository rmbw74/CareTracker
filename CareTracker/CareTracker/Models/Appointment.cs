using System;
using System.ComponentModel.DataAnnotations;

namespace CareTracker.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public DateTime AppointmentTime { get; set; }

        [Required]
        public string AppointmentReason { get; set; }

        [Required]
        public string AppointmentAddress { get; set; }

       
        public string AppointmentPhoneNumber { get; set; }

        public string AppointmentNotes { get; set; }

        public virtual Dependent Dependent { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}