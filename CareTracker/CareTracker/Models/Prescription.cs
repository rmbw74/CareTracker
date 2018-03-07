using System.ComponentModel.DataAnnotations;

namespace CareTracker.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }

        [Required]
        [Display(Name = "Drug Name")]
        public string DrugName { get; set; }
        
        [Required]
        public string Dosage { get; set; }

        [Required]
        public string Frequency { get; set; }

        [Required]
        [Display(Name = "Pharmacy Address")]
        public string PharmacyAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Pharmacy Phone Number")]
        public string PharmacyPhoneNumber { get; set; }

        [Display(Name = "Notes")]
        public string PrescriptionNotes { get; set; }

        [Required]
        [Display(Name = "Prescription Is Active?")]
        public bool PrescriptionActive { get; set; }

        [Required]
        public int DependentId { get; set; }
        public virtual Dependent Dependent { get; set; }
        [Required]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        //when a new Prescription is created, set PrescriptionActive to true by default
        public Prescription() => PrescriptionActive = true;




    }
}