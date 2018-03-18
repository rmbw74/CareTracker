using CareTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareTracker.Models.AppointmentViewModels
{
    public class AllAppointmentsViewModel
    {

        public IEnumerable<DependentAppointment> Appointments { get; set; }

       
        
    }
}
