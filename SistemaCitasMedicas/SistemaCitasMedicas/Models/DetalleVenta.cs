using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCitasMedicas.Models
{
    public class DetalleVenta
    {
        public int idventa { get; set; }
        public int idfarmaceutico { get; set; }
        public int cantidaddet { get; set; }
        public decimal precioUnidaddet { get; set; }
    }
}