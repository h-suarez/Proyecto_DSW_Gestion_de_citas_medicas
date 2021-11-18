using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SistemaCitasMedicas.Models;

namespace SistemaCitasMedicas.DAO
{
    public class citaDAO
    {
        conexionDAO cn = new conexionDAO();
        public IEnumerable<Cita> citasAtendidas(int estado)
        {
            return listadoCitas().Where(c => c.idestado == estado);
        }
        public IEnumerable<Cita> listadoCitas()
        {
            List<Cita> temporal = new List<Cita>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_listar_citas", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Cita()
                    {
                        idcita = dr.GetInt32(0),
                        idmedico = dr.GetInt32(1),
                        idusuario = dr.GetInt32(2),
                        fechacita = dr.GetDateTime(3),
                        horacita = dr.GetTimeSpan(4),
                        preciocita = dr.GetDecimal(5),
                        pagocita = dr.GetDecimal(6),
                        observaciones = dr.GetString(7),
                        prescripcion = dr.GetString(8),
                        idestado = dr.GetInt32(9),
                        fechaReg = dr.GetDateTime(10)
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }
        //public IEnumerable<CitaList1> listadoConsultaCitaMedico(string SP,SqlParameter[] pars = null)
        public IEnumerable<CitaList1> listadoConsultaCitaMedico(int idcuentamed)
        {
            List<CitaList1> temporal = new List<CitaList1>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_listar_man_citas @idmed", cn.getcn);
                cmd.Parameters.AddWithValue("@idmed",idcuentamed);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new CitaList1()
                    {
                        idcita = dr.GetInt32(0),
                        paciente = dr.GetString(1),
                        fechacita = dr.GetDateTime(2),
                        horacita = dr.GetTimeSpan(3),
                        estado = dr.GetString(4)
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }
        public string METODOSCITA(string sp, SqlParameter[] pars = null, int op = 0)
        {
            //cn = new conexionDAO();
            string mensaje = "";
            try
            {
                SqlCommand cmd = new SqlCommand(sp, cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (pars != null) cmd.Parameters.AddRange(pars.ToArray());
                cn.getcn.Open();
                int c = cmd.ExecuteNonQuery();
                if (op == 1) mensaje = c + " Su cita se reservo exitosamente";
                else if (op == 2) mensaje = c + " La cita se atendio y actualizo con exito";
            }
            catch (SqlException ex) { mensaje = ex.Message; }
            finally { cn.getcn.Close(); }
            return mensaje;
        }
    }
}