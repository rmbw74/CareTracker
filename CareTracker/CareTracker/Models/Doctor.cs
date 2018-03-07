using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CareTracker.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Hospital AFfiliation")]
        public string Hospital { get; set; }

        [Required]
        [Display(Name = "Type or Specialty")]
        public string Specialty { get; set; }

        [Required]
        [Display(Name = "Main Address")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public virtual ICollection<DependentDoctor> DependentDoctors { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }

    }
}