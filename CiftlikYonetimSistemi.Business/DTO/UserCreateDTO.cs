using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Business.DTO
{
    public class UserCreateDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }  // Kullanıcı oluştururken şifre bilgisinin alınması gerekiyorsa

        public string Email { get; set; }

        public int UserTypeId { get; set; }  // Kullanıcının tipini belirleyen ID


    }
}
