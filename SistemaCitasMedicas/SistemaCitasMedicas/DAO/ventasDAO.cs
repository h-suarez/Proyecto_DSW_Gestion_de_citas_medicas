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
                        //idventa = dr.GetInt32(0),
                        idpaciente = dr.GetInt32(1),
                        fechaReg = dr.GetDateTime(2)
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
                        //idventa = dr.GetInt32(0),
                        idpaciente = dr.GetInt32(1),
                        fechaReg = dr.GetDateTime(2)
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }

        public IEnumerable<DetalleVenta> listarIngresosMesActual()
        {
            List<DetalleVenta> temporal = new List<DetalleVenta>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_ingresos_este_mes", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new DetalleVenta()
                    {
                        idventa = dr.GetInt32(0),
                        idfarmaceutico = dr.GetInt32(1),
                        cantidaddet = dr.GetInt32(2),
                        precioUnidaddet = dr.GetDecimal(3)
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }
        public IEnumerable<DetalleVenta> listarIngresosMesPasado()
        {
            List<DetalleVenta> temporal = new List<DetalleVenta>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_ingresos_este_mes_pasado", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new DetalleVenta()
                    {
                        idventa = dr.GetInt32(0),
                        idfarmaceutico = dr.GetInt32(1),
                        cantidaddet = dr.GetInt32(2),
                        precioUnidaddet = dr.GetDecimal(3)
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }

        public string Transaccion(Venta reg,List<Item> carrito) {
            string mensaje = "";
            conexionDAO cn = new conexionDAO();
            cn.getcn.Open();
            SqlTransaction tr = cn.getcn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                // Ejecutar el SP de agregar venta, con su transacción
                SqlCommand cmd = new SqlCommand("usp_agregar_venta", cn.getcn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@paciente", SqlDbType.Int).Value = reg.idpaciente;
                cmd.Parameters.Add("@n", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                // Recuperar el valor de @n de tipo output y almacenarlo en una variable
                int n = (int)cmd.Parameters["@n"].Value;

                // Ejecutar el SP agregar detalle, leyendo cada elemento de carrito
                foreach(Item it in carrito)
                {
                    cmd = new SqlCommand("usp_agregar_detalle_venta",cn.getcn, tr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idventa", SqlDbType.Int).Value = n;
                    cmd.Parameters.Add("@farm", SqlDbType.Int).Value = it.codigo;
                    cmd.Parameters.Add("@cant", SqlDbType.Int).Value = it.cantidad;
                    cmd.Parameters.Add("@preunit", SqlDbType.Decimal).Value = it.precio;
                    cmd.ExecuteNonQuery();
                }
                // Ejecutar el SP Actualizar stock del farmaceutico, leyendo cada registro de carrito
                foreach(Item it in carrito)
                {
                    cmd = new SqlCommand("usp_actualiza_unidades", cn.getcn, tr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idfarm",SqlDbType.Int).Value = it.codigo;
                    cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = it.cantidad;
                    cmd.ExecuteNonQuery();
                }
                // Si todo esta OK
                tr.Commit();
                mensaje = string.Format("Se ha registrado la orden con número {0}", n);
            }catch(SqlException ex)
            {
                mensaje = ex.Message;
                tr.Rollback();
            }
            finally { cn.getcn.Close(); }
            return mensaje;
        }
        public IEnumerable<DetalleVentaList1> listarReporteVentas()
        {
            List<DetalleVentaList1> temporal = new List<DetalleVentaList1>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_listar_detalle_venta", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new DetalleVentaList1()
                    {
                        idventa = dr.GetInt32(0),
                        farmaceutico = dr.GetString(1),
                        cantidaddet = dr.GetInt32(2),
                        precioUnidaddet = dr.GetDecimal(3)
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }
    }
}