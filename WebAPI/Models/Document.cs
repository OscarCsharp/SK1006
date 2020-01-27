using Microsoft.AspNetCore.Http;
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
		public int FileId { get; set; }
		public string FileName { get; set; }
		public int ApplicantId { get; set; }
		public byte [] File { get; set; }
	}


}
