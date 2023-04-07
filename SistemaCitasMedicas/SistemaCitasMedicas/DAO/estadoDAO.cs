using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SistemaCitasMedicas.Models;

namespace SistemaCitasMedicas.DAO
{
    public class estadoDAO
    {
        conexionDAO cn = new conexionDAO();
        public IEnumerable<Estado> listadoEstadoCuenta()
        {
            List<Estado> temporal = new List<Estado>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_listar_estado_usuario", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Estado()
                    {
                        idestado = dr.GetInt32(0),
                        descripcion = dr.GetString(1)
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }
        public IEnumerable<Estado> listadoEstadoProveedor()
        {
            List<Estado> temporal = new List<Estado>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_listar_estado_proveedor", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Estado()
                    {
                        idestado = dr.GetInt32(0),
                        descripcion = dr.GetString(1)
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }
        public IEnumerable<Estado> listadoEstadoFarmaceutico()
        {
            List<Estado> temporal = new List<Estado>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_listar_estado_farmaceutico", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Estado()
                    {
                        idestado = dr.GetInt32(0),
                        descripcion = dr.GetString(1)
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }
        public IEnumerable<Estado> listadoEstadoCita()
        {
            List<Estado> temporal = new List<Estado>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_listar_estado_cita", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Estado()
                    {
                        idestado = dr.GetInt32(0),
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