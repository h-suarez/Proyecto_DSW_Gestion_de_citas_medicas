using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCitasMedicas.Models
{
    public class Venta
    {
        public int idventa { get; set; }
        public int idpaciente { get; set; }
        public int idvendedor { get; set; }
        public int cantidadtot { get; set; }
        public decimal preciotot { get; set; }
        public DateTime fechaReg { get; set; }
    }
}