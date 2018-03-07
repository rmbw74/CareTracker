using System.ComponentModel.DataAnnotations;

namespace CareTracker.Models
{
    public class SharedDependent
    {
        [Key]
        public int SharedDependentId { get; set; }

        [Required]
        public ApplicationUser ToUser { get; set; }

        [Required]
        public int DependentUserId { get; set; }
        public DependentUser DependentUser { get; set; }
    }
}