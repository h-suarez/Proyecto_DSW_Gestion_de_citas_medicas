using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SistemaCitasMedicas.Models
{
    public class Usuario
    {
        public int idusuario { get; set; }
        public int dniusu { get; set; }
        public string nombreusu { get; set; }
        public int idsexo { get; set; }
        public int iddistrito { get; set; }
        public string celularusu { get; set; }
        public string email { get; set; }
        public string clave { get; set; }
        [DataType(DataType.Date)] public DateTime fechaRegusu { get; set; }
        public int idtipo { get; set; }
        public int idestado { get; set; }
    }
}