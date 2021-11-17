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
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand(
                    "exec usp_listar_usuarios",
                    cn.getcn
                );
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Usuario
                    {
                        idusuario = (int)dr[0],
                        dniusu = (int)dr[1],
                        nombreusu = (string)dr[2],
                        idsexo = (int)dr[3],
                        iddistrito = (int)dr[4],
                        celularusu = (string)dr[5],
                        email = (string)dr[6],
                        clave = (string)dr[7],
                        fechaRegusu = (DateTime)dr[8],
                        idtipo = (int)dr[9],
                        idestado = (int)dr[10]
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return lista;
        }

        public string registrarUsuPaciente(string sp, SqlParameter[] pars = null, int op = 0)
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
                if (op == 1) mensaje = "Su cuenta se creo correctamente";
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

        public IEnumerable<Usuario> listarUsuariosMesActual()
        {
            List<Usuario> lista = new List<Usuario>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand(
                    "exec usp_usuarios_este_mes",
                    cn.getcn
                );
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Usuario
                    {
                        idusuario = (int)dr[0],
                        dniusu = (int)dr[1],
                        nombreusu = (string)dr[2],
                        idsexo = (int)dr[3],
                        iddistrito = (int)dr[4],
                        celularusu = (string)dr[5],
                        email = (string)dr[6],
                        clave = (string)dr[7],
                        fechaRegusu = (DateTime)dr[8],
                        idtipo = (int)dr[9],
                        idestado = (int)dr[10]
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return lista;
        }
        public IEnumerable<Usuario> listarUsuariosMesPasado()
        {
            List<Usuario> lista = new List<Usuario>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand(
                    "exec usp_usuarios_este_mes_pasado",
                    cn.getcn
                );
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Usuario
                    {
                        idusuario = (int)dr[0],
                        dniusu = (int)dr[1],
                        nombreusu = (string)dr[2],
                        idsexo = (int)dr[3],
                        iddistrito = (int)dr[4],
                        celularusu = (string)dr[5],
                        email = (string)dr[6],
                        clave = (string)dr[7],
                        fechaRegusu = (DateTime)dr[8],
                        idtipo = (int)dr[9],
                        idestado = (int)dr[10]
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return lista;
        }
        public string CRUDUSUCUENTAS(string sp, SqlParameter[] pars = null, int op = 0)
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
                if (op == 1) mensaje = c + " Se creo la cuenta exitosamente";
                if (op == 2) mensaje = c + " Se actualizo la cuenta exitosamente";
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
        public IEnumerable<UsuarioList1> listadoManUsuarios()
        {
            List<UsuarioList1> lista = new List<UsuarioList1>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand(
                    "exec usp_listar_man_usuarios",
                    cn.getcn
                );
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new UsuarioList1
                    {
                        idusuario = (int)dr[0],
                        nombreusu = (string)dr[1],
                        sexo = (string)dr[2],
                        email = (string)dr[3],
                        tipo = (string)dr[4],
                        estado = (string)dr[5],
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return lista;
        }
    }
}