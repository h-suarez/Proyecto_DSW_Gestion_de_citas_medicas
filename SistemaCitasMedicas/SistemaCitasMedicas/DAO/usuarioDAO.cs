using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SistemaCitasMedicas.Models;

namespace SistemaCitasMedicas.DAO
{
    public class usuarioDAO
    {
        conexionDAO cn = new conexionDAO();
        public Usuario validarLogin(string email, string clave)
        {
            return listado().Where(u => u.email == email && u.clave == clave).FirstOrDefault();
        }
        public IEnumerable<Usuario> listado()
        {
            List<Usuario> lista = new List<Usuario>();
            cn = new conexionDAO();
            using(cn.getcn)
            {
                SqlCommand cmd = new SqlCommand(
                    "exec usp_listar_usuarios",
                    cn.getcn
                );
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    lista.Add(new Usuario
                    {
                        idusuario   = (int)dr[0],
                        dniusu      = (int)dr[1],
                        nombreusu   = (string)dr[2],
                        idsexo      = (int)dr[3],
                        iddistrito  = (int)dr[4],
                        celularusu  = (string)dr[5],
                        email       = (string)dr[6],
                        clave       = (string)dr[7],
                        fechaRegusu = (DateTime)dr[8],
                        idtipo      = (int)dr[9],
                        idestado    = (int)dr[10]
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return lista;
        }
    }
}