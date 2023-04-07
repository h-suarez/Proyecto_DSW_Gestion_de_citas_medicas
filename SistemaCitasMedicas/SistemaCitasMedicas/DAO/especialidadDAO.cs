using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SistemaCitasMedicas.Models;

namespace SistemaCitasMedicas.DAO
{
    public class especialidadDAO
    {
        public IEnumerable<Especialidad> listarEspecialidades()
        {
            List<Especialidad> temporal = new List<Especialidad>();
            conexionDAO cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("select*from tb_especialidad",cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    temporal.Add(new Especialidad
                    {
                        idespecialidad = (int)dr[0],
                        fotoesp = (string)dr[1],
                        nombreesp = (string)dr[2],
                        descripcionesp = (string)dr[3],
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return temporal;
        }
        public Especialidad BuscarId(int idesp)
        {
            return listarEspecialidades().Where(e => e.idespecialidad == idesp).FirstOrDefault();
        }
        public string CRUDESPECIALIDAD(string SP, SqlParameter[] pars = null, int op = 0)
        {
            conexionDAO cn = new conexionDAO();
            string mensaje = "";
            try
            {
                SqlCommand cmd = new SqlCommand(SP, cn.getcn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (pars != null) cmd.Parameters.AddRange(pars.ToArray());
                cn.getcn.Open();
                int c = cmd.ExecuteNonQuery();
                if (op == 1) mensaje = c + " Especialidad agregada";
                else if (op == 2) mensaje = c + " Especialidad actualizada";
            }
            catch(SqlException ex) { mensaje = ex.Message; }
            finally { cn.getcn.Close(); }
            return mensaje;
        }
    }
}