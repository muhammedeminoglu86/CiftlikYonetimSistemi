using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Models
{
	public class Javascript
	{
		public int Id { get; set; }
		public string ControllerName { get; set; }
		public string ActionName { get; set; }
		public string Value { get; set; }
		public int IsActive { get; set; }
		public int OrderNumber { get; set; }
	}
}
