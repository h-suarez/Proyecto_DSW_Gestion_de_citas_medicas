using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SistemaCitasMedicas.Models;

namespace SistemaCitasMedicas.DAO
{
    public class medicoDAO
    {
        conexionDAO cn = new conexionDAO();
        public IEnumerable<Medico> listadoMedicos()
        {
            List<Medico> temporal = new List<Medico>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_listar_medicos", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Medico()
                    {
                        idmedico = dr.GetInt32(0),
                        fotomed = dr.GetString(1),
                        idespecialidad = dr.GetInt32(2),
                        horaini = dr.GetTimeSpan(3),
                        horafin = dr.GetTimeSpan(4),
                        idcuenta = dr.GetInt32(5),
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }

        public IEnumerable<MedicoList1> listadoManteniminetoMedicos()
        {
            List<MedicoList1> temporal = new List<MedicoList1>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_listar_man_medicos", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new MedicoList1()
                    {
                        idmedico = dr.GetInt32(0),
                        fotomed = dr.GetString(1),
                        especialidad = dr.GetString(2),
                        horaini = dr.GetTimeSpan(3),
                        horafin = dr.GetTimeSpan(4),
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }
        public string CRUDMEDICO(string sp, SqlParameter[] pars = null, int op = 0)
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
                if (op == 1) mensaje = c + " El medico se registro exitosamente";
                if (op == 2) mensaje = c + " El medico se actualizo exitosamente";
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