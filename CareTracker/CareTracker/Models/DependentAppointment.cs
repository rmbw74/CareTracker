using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CareTracker.Models
{
    public class DependentAppointment
    {
        public int AppointmentId { get; set; }
        public int DependentId { get; set; }
        public int DoctorId { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string DependentFirstName { get; set; }
        public string DependentLastName { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }
        [Display(Name = "Time")]
        [DataType(DataType.Time)]
        public DateTime AppointmentTime { get; set; }
        public string AppointmentReason { get; set; }
        public string AppointmentAddress { get; set; }
        public string AppointmentPhone { get; set; }
        public string AppointmentNotes { get; set; }

    }
}
