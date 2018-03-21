using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CareTracker.Models.DependentViewModels
{
    public class DependentSummaryViewModel
    {
        public Dependent Dependent { get; set; }
        public int Age { get; set; }

        public ICollection<SummaryAppointment> Appointments{ get; set; }
        public ICollection<SummaryPrescription> Prescriptions { get; set; }

    

    }
}
