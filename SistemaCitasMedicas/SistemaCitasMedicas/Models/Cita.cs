using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SistemaCitasMedicas.Models
{
    public class Cita
    {
        public int idcita { get; set; }
        public int idmedico { get; set; }
        public int idusuario { get; set; }
        [DataType(DataType.Date)] public DateTime fechacita { get; set; }
        public TimeSpan horacita { get; set; }
        public decimal preciocita { get; set; }
        public decimal pagocita { get; set; }
        public string observaciones { get; set; }
        public string prescripcion { get; set; }
        public int idestado { get; set; }
        [DataType(DataType.Date)] public DateTime fechaReg { get; set; }
    }
}