using SistemaCitasMedicas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using SistemaCitasMedicas.DAO;

namespace SistemaCitasMedicas.Controllers
{
    public class ClienteController : Controller
    {
        medicoDAO medicos = new medicoDAO();
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
        public ActionResult Citas(int idmedico=0)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.medicos = new SelectList(medicos.listadoManteniminetoMedicos(), "idmedico", "especialidad", idmedico);
            return View(medicos.listadoManteniminetoMedicos().Where(m=>m.idmedico == idmedico));
        }
        public ActionResult Solicitar(int idmedico=0)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Medico reg = medicos.listadoMedicos().Where(m => m.idmedico == idmedico).FirstOrDefault();
            if (reg == null) reg = new Medico();
            return View(reg);
        }
        

        [HttpPost]public ActionResult Solicitar(int idmedico = 0,DateTime fecha= new DateTime(),TimeSpan hora = new TimeSpan())
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null)
            {
                return RedirectToAction("Login", "Home");
            }
            
            return View();
        }
    }
}