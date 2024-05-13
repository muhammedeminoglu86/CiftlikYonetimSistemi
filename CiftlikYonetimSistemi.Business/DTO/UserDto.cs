using System;

namespace CiftlikYonetimSistemi.Domain.Models
{
	public class UserDto
	{
		public int id { set; get; }
		public string username { set; get; }
		public string password { set; get; }

		public string email { set; get; }
        public int usertypeid { set; get; }

    }
}
