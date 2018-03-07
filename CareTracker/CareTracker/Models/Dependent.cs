using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CareTracker.Models
{
    public class Dependent
    {
        [Key]
        public int DependentId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime Birthday { get; set; }

        [Required]
        [Display(Name = "Social Security Number")]
        public string SocialSecurityNumber { get; set; }

        [Display(Name = "Notes")]
        public string DependentNotes { get; set; }
        

        //forign key connections

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<DependentDoctor> DependentDoctors { get; set; }
        public virtual ICollection<DependentUser> DependentUsers { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}