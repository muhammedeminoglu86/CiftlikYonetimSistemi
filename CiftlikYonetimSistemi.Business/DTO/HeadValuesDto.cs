using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Business.DTO
{
	public class HeadValuesDto
	{
		public int Id { get; set; }
		public int HeadId { get; set; }
		public int OrderNumber { set; get; }
		public string Value { get; set; }
		public int IsActive { get; set; }
		public DateTime CreationDate { get; set; }
		public HeadDto head { set; get; } = new HeadDto();
	}
}
