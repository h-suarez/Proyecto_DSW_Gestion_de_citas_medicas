using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaCitasMedicas.DAO;
using SistemaCitasMedicas.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SistemaCitasMedicas.Controllers
{
    public class HomeController : Controller
    {
        //DAOs
        usuarioDAO usuarios = new usuarioDAO();
        sexoDAO sexos = new sexoDAO();
        distritoDAO distritos = new distritoDAO();
        tipoCuentaDAO tipocuentas = new tipoCuentaDAO();
        estadoDAO estados = new estadoDAO();

        public ActionResult SessionTests()
        {
            return View(Session["usuario"] as List<Usuario>);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string clave)
        {
            Usuario usuario = usuarios.validarLogin(email, clave);
            
            if(usuario!=null)
            { 
                Session["usuario"] = usuario;
                return View("SessionTests");
            }
            
            ViewBag.msj = "Error: combinación de correo y clave errónea.";
            
            return View("Login");
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgregarCuentaPaciente()
        {
            Usuario reg = new Usuario();
            ViewBag.valor = "invisible";
            ViewBag.sexos = new SelectList(sexos.listadoSexo(), "idsexo", "descripcion", reg.idsexo);
            ViewBag.distritos = new SelectList(distritos.listadoDistrito(), "iddistrito", "descripcion", reg.iddistrito);
            return View(reg);
        }
        [HttpPost]public ActionResult AgregarCuentaPaciente(string btncrud,Usuario reg)
        {
            switch (btncrud)
            {
                case "Registrar": return RegistrarCuentaPaciente(reg);
                default: return RedirectToAction("AgregarCuentaPaciente", new { id = "" });
            }
        }

        public ActionResult RegistrarCuentaPaciente(Usuario reg)
        {
            SqlParameter[] pars =
            {
                new SqlParameter(){ParameterName="@dni",Value=reg.dniusu},
                new SqlParameter(){ParameterName="@nom",Value=reg.nombreusu},
                new SqlParameter(){ParameterName="@sexo",Value=reg.idsexo},
                new SqlParameter(){ParameterName="@distrito",Value=reg.iddistrito},
                new SqlParameter(){ParameterName="@cel",Value=reg.celularusu},
                new SqlParameter(){ParameterName="@email",Value=reg.email},
                new SqlParameter(){ParameterName="@clave",Value=reg.clave},
            };
            ViewBag.valor = "visible";
            ViewBag.mensaje = usuarios.registrarUsuPaciente("usp_agregar_cliente", pars, 1);
            ViewBag.sexos = new SelectList(sexos.listadoSexo(), "idsexo", "descripcion", reg.idsexo);
            ViewBag.distritos = new SelectList(distritos.listadoDistrito(), "iddistrito", "descripcion", reg.iddistrito);
            return View(reg);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}