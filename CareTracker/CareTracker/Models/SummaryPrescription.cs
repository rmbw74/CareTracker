using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CareTracker.Models
{
    public class SummaryPrescription
    {
        public int PrescriptionId { get; set; }

        public int DoctorId { get; set; }

        [Display(Name="Name")]
        public String DrugName { get; set; }

        public string Dosage { get; set; }

        public string Frequency { get; set; }

        [Display(Name ="Notes")]
        public string PrescriptionNotes { get; set; }

        [Display(Name = "Doctor")]
        public string PrescribingDoctor { get; set; }

        [Display(Name = "Pharmacy Phone #")]
        public string PharmacyPhoneNumber { get; set; } 

    }
}
