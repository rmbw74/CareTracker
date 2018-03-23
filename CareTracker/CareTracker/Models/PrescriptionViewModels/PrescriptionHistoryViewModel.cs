using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareTracker.Models.PrescriptionViewModels
{
    public class PrescriptionHistoryViewModel
    {
        public ICollection <Prescription> Prescriptions { get; set; }
        public Dependent Dependent { get; set; }
    }
}
