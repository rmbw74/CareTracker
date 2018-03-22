using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareTracker.Models.AppointmentViewModels
{
    public class DependentAppointmentViewModel

    {
        public Dependent Dependent { get; set; }
        public ICollection<DependentAppointment> Appointments { get; set; }
    }
}
