using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiftlikYonetimSistemi.Business.DTO
{
    public class AnimalSubTypeDTO
    {
        public int Id { get; set; }

        public int? Animaltypeid { get; set; }

        public string? Animalsubtypename { get; set; }

        public byte[]? Logo { get; set; }

        public int? Isactive { get; set; }
    }
}
