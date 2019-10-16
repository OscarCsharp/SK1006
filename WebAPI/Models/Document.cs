using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Document
    {
        [Key]
        public int Applicant { get; set; }
        public byte[] IdDoc { get; set; }
        public byte[] ProofDoc { get; set; }
        public byte[] Results { get; set; }
    }
}
