using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CareTracker.Models
{
    public class SharedDependentListItem
    {
        public int DependentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public string FromUserName { get; set; }

        public string FromUserEmail { get; set; }
    }
}
