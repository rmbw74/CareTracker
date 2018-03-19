using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CareTracker.Models.PrescriptionViewModels
{
    public class DependentAllPrescriptionsViewModel
    {
        public int PrescriptionId { get; set; }


        [Display(Name = "Drug Name")]
        public string DrugName { get; set; }


        public string Dosage { get; set; }

        public string Frequency { get; set; }

        [Display(Name = "Pharmacy Address")]
        public string PharmacyAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Pharmacy Phone Number")]
        public string PharmacyPhoneNumber { get; set; }

        [Display(Name = "Active?")]
        public bool PrescriptionActive { get; set; }

        public int DependentId { get; set; }
        [Display(Name = "Dependent First Name")]
        public string DependentFirstName { get; set; }
        [Display(Name = "Dependent Last Name")]
        public string DependentLastName { get; set; }

        public int DoctorId { get; set; }
        [Display(Name = "Doctor")]
        public string DoctorLastName { get; set; }

        public string DoctorFirstName { get; set; }


    }

}
