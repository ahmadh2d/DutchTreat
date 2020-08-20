﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Models
{
	public class ContactModel
	{
		[Required]
		[MinLength(5)]
		public string Name { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Subject { get; set; }
		[Required]
		[MaxLength(200)]
		public string Message { get; set; }
	}
}
