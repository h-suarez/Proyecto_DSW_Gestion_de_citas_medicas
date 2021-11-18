using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCitasMedicas.Models
{
    public class MedicoList1
    {
        public int idmedico { get; set; }
        public string fotomed { get; set; }
        public string especialidad { get; set; }
        public TimeSpan horaini { get; set; }
        public TimeSpan horafin { get; set; }
    }
}