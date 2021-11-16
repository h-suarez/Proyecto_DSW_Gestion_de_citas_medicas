﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCitasMedicas.Models
{
    public class Proveedor
    {
        public int idproveedor { get; set; }
        public string rucprov { get; set; }
        public string nombreprov { get; set; }
        public string telefonoprov { get; set; }
        public string razonprov { get; set; }
        public int iddistrito { get; set; }
        public string direccionprov { get; set; }
        public DateTime fechaRegprov { get; set; }
        public int idestado { get; set; }
    }
}