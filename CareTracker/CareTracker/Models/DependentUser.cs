using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareTracker.Models
{
    public class DependentUser
    {
        [Key]
        public int DependentUserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public int DependentId { get; set;  }

        public Dependent Dependent { get; set; }


    }
}