using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SistemaCitasMedicas.Models
{
    public class CitaList2
    {
        public int idcita { get; set; }
        public string especialidad { get; set; }
        public decimal preciocita { get; set; }
        public decimal pagocita { get; set; }
        public string estado { get; set; }
        [DataType(DataType.Date)] public DateTime fechacita { get; set; }
    }
}