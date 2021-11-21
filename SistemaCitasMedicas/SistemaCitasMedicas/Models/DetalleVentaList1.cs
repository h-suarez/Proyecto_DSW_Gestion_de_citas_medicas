using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCitasMedicas.Models
{
    public class DetalleVentaList1
    {
        public int idventa { get; set; }
        public string farmaceutico { get; set; }
        public int cantidaddet { get; set; }
        public decimal precioUnidaddet { get; set; }
    }
}