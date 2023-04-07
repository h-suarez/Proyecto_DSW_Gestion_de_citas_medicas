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
        SqlConnection cn = new SqlConnection(@"server=DESKTOP-0SJQF5N\SQLEXPRESS; database=SISTEMACONSULTASMEDICAS; integrated security=true");
        public SqlConnection getcn { get { return cn; } }
    }
}