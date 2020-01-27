using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Applicant
    {
        [Key]
        public int ReferenceNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int IdNumber { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string HomeAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Zip { get; set; }
        public string CellNo { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Disability { get; set; }
        public string AppliedGrade { get; set; }
        public string EnrollYear { get; set; }
        public string Status { get; set; }
    }
}
