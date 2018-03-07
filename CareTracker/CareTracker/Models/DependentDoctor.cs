using System.ComponentModel.DataAnnotations;

namespace CareTracker.Models
{
    public class DependentDoctor
    {
        [Key]
        public int DependentDoctorId { get; set; }

        [Required]
        public int DependentId { get; set; }
        public virtual Dependent Dependent { get; set; }
        [Required]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}