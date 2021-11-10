using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SistemaCitasMedicas.Models;

namespace SistemaCitasMedicas.DAO
{
    public class sexoDAO
    {
        conexionDAO cn = new conexionDAO();
        public IEnumerable<Sexo> listadoSexo()
        {
            List<Sexo> temporal = new List<Sexo>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_listar_sexo", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Sexo()
                    {
                        idsexo = dr.GetInt32(0),
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