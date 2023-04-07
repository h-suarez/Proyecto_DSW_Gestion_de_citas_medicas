using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SistemaCitasMedicas.Models;

namespace SistemaCitasMedicas.DAO
{
    public class farmaceuticoDAO
    {
        conexionDAO cn = new conexionDAO();
        public IEnumerable<Farmaceutico> listarFarmaceuticos()
        {
            List<Farmaceutico> lista = new List<Farmaceutico>();
            cn = new conexionDAO();
            using (cn.getcn)
            {
                SqlCommand cmd = new SqlCommand("exec usp_listar_farmaceutico", cn.getcn);
                cn.getcn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Farmaceutico
                    {
                        idfarmaceutico = (int)dr[0],
                        fotofarm = (string)dr[1],
                        nombrefarm = (string)dr[2],
                        stockfarm = (int)dr[3],
                        preciofarm = (decimal)dr[4],
                        descripcionfarm = (string)dr[5],
                        idproveedor = (int)dr[6],
                        idestado = (int)dr[7],
                        fechaReg = (DateTime)dr[8],
                    });
                }
                dr.Close();
                cn.getcn.Close();
            }
            return lista;
        }
        public string CRUDFARMACEUTICOS(string sp, SqlParameter[] pars = null, int op = 0)
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
                if (op == 1) mensaje = c + " Se registro el farmaceutico correctamente";
                if (op == 2) mensaje = c + " Se actualizo el farmaceutico correctamente";
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