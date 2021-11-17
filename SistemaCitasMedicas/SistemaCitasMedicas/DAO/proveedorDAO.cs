using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SistemaCitasMedicas.Models;

namespace SistemaCitasMedicas.DAO
{
    public class proveedorDAO
    {
        conexionDAO cn = new conexionDAO();
        public IEnumerable<Proveedor> listarProveedores()
        {
            List<Proveedor> lista = new List<Proveedor>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand(
                    "exec usp_listar_proveedores",
                    cn.getcn
                );
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Proveedor
                    {
                        idproveedor = (int)dr[0],
                        rucprov = (string)dr[1],
                        nombreprov = (string)dr[2],
                        telefonoprov = (string)dr[3],
                        razonprov = (string)dr[4],
                        iddistrito = (int)dr[5],
                        direccionprov = (string)dr[6],
                        fechaRegprov = (DateTime)dr[7],
                        idestado = (int)dr[8],
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return lista;
        }
        public IEnumerable<ProveedorList1> listadoManProveedor()
        {
            List<ProveedorList1> lista = new List<ProveedorList1>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand(
                    "exec usp_listar_man_proveedores",
                    cn.getcn
                );
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ProveedorList1
                    {
                        idproveedor = (int)dr[0],
                        nombreprov = (string)dr[1],
                        telefonoprov = (string)dr[2],
                        distrito = (string)dr[3],
                        estado = (string)dr[4],
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return lista;
        }
        public string CRUDPROVEEDOR(string sp, SqlParameter[] pars = null, int op = 0)
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
                if (op == 1) mensaje = c + " Se creo el proveedor exitosamente";
                if (op == 2) mensaje = c + " Se actualizo el proveedor exitosamente";
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