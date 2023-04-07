using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SistemaCitasMedicas.Models;


namespace SistemaCitasMedicas.DAO
{
    public class tipoCuentaDAO
    {
        conexionDAO cn = new conexionDAO();
        public IEnumerable<TipoCuenta> listadoTipoCuenta()
        {
            List<TipoCuenta> temporal = new List<TipoCuenta>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_listar_tipocuenta", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new TipoCuenta()
                    {
                        idtipo = dr.GetInt32(0),
                        descripcion = dr.GetString(1)
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }
    }
}