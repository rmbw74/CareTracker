using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareTracker.Models
{
    public class UserDependent
    {
        public int DependentUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }
}
