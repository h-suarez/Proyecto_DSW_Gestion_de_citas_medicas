using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SistemaCitasMedicas.Models;

namespace SistemaCitasMedicas.DAO
{
    public class usuarioDAO
    {
        conexionDAO cn = new conexionDAO();
        public string registrarUsuPaciente(string sp, SqlParameter[] pars = null, int op = 0)
        {
            cn = new conexionDAO();
            string mensaje = "";
            try
            {
                SqlCommand cmd = new SqlCommand(sp, cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (pars != null) cmd.Parameters.AddRange(pars.ToArray());
                cn.getcn.Open();
                int c = cmd.ExecuteNonQuery();
                if (op == 1) mensaje = "Su cuenta se creo correctamente";
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                cn.getcn.Close();
            }
            return mensaje;
        }
    }
}