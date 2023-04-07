using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SistemaCitasMedicas.Models
{
    public class CitaList1
    {
        public int idcita { get; set; }
        public string paciente { get; set; }
        [DataType(DataType.Date)] public DateTime fechacita { get; set; }
        public TimeSpan horacita { get; set; }
        public string estado { get; set; }
    }
}