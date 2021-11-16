using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SistemaCitasMedicas.Models;

namespace SistemaCitasMedicas.DAO
{
    public class ventasDAO
    {
        conexionDAO cn = new conexionDAO();
        public IEnumerable<Venta> listarVentasMesActual()
        {
            List<Venta> temporal = new List<Venta>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_ventas_este_mes", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Venta()
                    {
                        idventa = dr.GetInt32(0),
                        idpaciente = dr.GetInt32(1),
                        idvendedor = dr.GetInt32(2),
                        cantidadtot = dr.GetInt32(3),
                        preciotot = dr.GetDecimal(4),
                        fechaReg = dr.GetDateTime(5)
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }
        public IEnumerable<Venta> listarVentasMesPasado()
        {
            List<Venta> temporal = new List<Venta>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_ventas_este_mes_pasado", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Venta()
                    {
                        idventa = dr.GetInt32(0),
                        idpaciente = dr.GetInt32(1),
                        idvendedor = dr.GetInt32(2),
                        cantidadtot = dr.GetInt32(3),
                        preciotot = dr.GetDecimal(4),
                        fechaReg = dr.GetDateTime(5)
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }
    }
}