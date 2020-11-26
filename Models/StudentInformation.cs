using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CRUDUsingADOApp.CustomValidation;

namespace CRUDUsingADOApp.Models {
    public class StudentInformation {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name ="Name")]
        public string FullName { get; set; }

        [Required]
        public int Batch { get; set; }

        [Required]
        [FacultyValidate(Allowed = new string[] { "CSIT","BCA","BCE"}, ErrorMessage = "Faculty does not exist.")]
        public string Faculty { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [Phone]
        [StringLength(10)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
