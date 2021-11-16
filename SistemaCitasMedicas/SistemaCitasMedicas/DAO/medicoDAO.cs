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
    }
}