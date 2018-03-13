using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CareTracker.Models
{
    public class SummaryAppointment
    {
        public int AppointmentDoctorId { get; set; }
        public int AppointmentId { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }
        [Display(Name = "Time")]
        [DataType(DataType.Time)]
        public DateTime AppointmentTime { get; set; }
        [Display(Name = "Reason")]
        public String AppointmentReason { get; set; }
        [Display(Name = "Doctor")]
        public String AppointmentDoctor { get; set; }
        [Display(Name = "Phone")]
        public String AppointmentPhoneNum { get; set; }
    }
}
