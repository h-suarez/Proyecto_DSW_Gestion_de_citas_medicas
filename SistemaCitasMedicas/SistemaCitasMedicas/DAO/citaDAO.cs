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
    }
}