using SistemaCitasMedicas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaCitasMedicas.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Home()
        {
            Usuario usuario = Session["usuario"] as Usuario;

            //Evita que clientes no logeados accedan al home de Cliente
            if(usuario == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }
    }
}