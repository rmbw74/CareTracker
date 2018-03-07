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
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string SocialSecurityNumber { get; set; }

        public string DependentNotes { get; set; }
        

        //forign key connections

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<DependentDoctor> DependentDoctors { get; set; }
        public virtual ICollection<DependentUser> DependentUsers { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}