using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Models
{
	public class HeadValues
	{
		public int Id { get; set; }
		public int HeadId { get; set; }
		public int OrderNumber { set; get; }
		public string Value { get; set; }
		public int IsActive { get; set; }
		public DateTime CreationDate { get; set; }
		public Head head { set; get; } = new Head();
	}
}
