using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CiftlikYonetimSistemi.Business.DTO
{
    public class AnimalTypeDTO
    {
        public int id { get; set; }
        public string? animaltype { get; set; }

        public string? typedesc { get; set; }


        public byte[]? Logo { get; set; }

    }
}
