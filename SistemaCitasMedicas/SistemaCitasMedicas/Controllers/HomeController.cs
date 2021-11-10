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
        public ActionResult SessionTests()
        {
            return View(Session["usuario"] as List<Usuario>);
        }
        usuarioDAO usuarios = new usuarioDAO();
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