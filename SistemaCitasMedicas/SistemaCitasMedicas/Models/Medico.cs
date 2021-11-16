using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCitasMedicas.Models
{
    public class Medico
    {
        public int idmedico { get; set; }
        public string fotomed { get; set; }
        public int idespecialidad { get; set; }
        public TimeSpan horaini { get; set; }
        public TimeSpan horafin { get; set; }
        public int idcuenta { get; set; }
    }
}