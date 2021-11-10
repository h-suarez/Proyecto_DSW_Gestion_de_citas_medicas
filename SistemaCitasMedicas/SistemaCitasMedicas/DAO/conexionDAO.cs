using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SistemaCitasMedicas.DAO
{
    public class conexionDAO
    {
        SqlConnection cn = new SqlConnection(@"server=LAPTOP-8KQCUPOO; database=SISTEMACONSULTASMEDICAS; integrated security=true");
        public SqlConnection getcn { get { return cn; } }
    }
}