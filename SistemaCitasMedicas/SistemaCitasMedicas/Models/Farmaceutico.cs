using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SistemaCitasMedicas.Models
{
    public class Farmaceutico
    {
        public int idfarmaceutico { get; set; }
        public string fotofarm { get; set; }
        public string nombrefarm { get; set; }
        public int stockfarm { get; set; }
        public decimal preciofarm { get; set; }
        public string descripcionfarm { get; set; }
        public int idproveedor { get; set; }
        public int idestado { get; set; }
        [DataType(DataType.Date)] public DateTime fechaReg { get; set; }
    }
}