using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Business.DTO
{
	public class HeadDto
	{
		public int Id { get; set; }
		public string ControllerName { get; set; }
		public string ActionName { get; set; }
		public string Title { get; set; }
		public string Charset { get; set; }
		public string Description { get; set; }
		public string OgLocale { get; set; }
		public string OgType { get; set; }
		public string OgTitle { get; set; }
		public string OgUrl { get; set; }
		public string OgSiteName { get; set; }
		public string Canonical { get; set; }
		public int? IsActive { get; set; }
		public DateTime? CreationDate { get; set; }
		public List<HeadValuesDto> HeadValues { get; set; } = new List<HeadValuesDto>();
	}
}
