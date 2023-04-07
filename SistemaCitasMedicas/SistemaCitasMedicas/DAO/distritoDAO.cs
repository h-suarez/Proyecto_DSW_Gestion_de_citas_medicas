using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SistemaCitasMedicas.Models;

namespace SistemaCitasMedicas.DAO
{
    public class distritoDAO
    {
        conexionDAO cn = new conexionDAO();
        public IEnumerable<Distrito> listadoDistrito()
        {
            List<Distrito> temporal = new List<Distrito>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_listar_distrito", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Distrito()
                    {
                        iddistrito = dr.GetInt32(0),
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