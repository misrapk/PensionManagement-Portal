using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementPortalAPP.Models
{
	public class Login
	{
		[Required]
		public string Username { get; set; }
		
		[Required]
		public string Password { get; set; }
	}
}
