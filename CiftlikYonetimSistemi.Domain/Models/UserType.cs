using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Domain.Models
{
	public class UserType
	{
		public int Id { get; set; }
		public string usertypename { set; get; }
		public string usertypedescription { set; get; }
		public int isactive { get; set; }

	}
}
