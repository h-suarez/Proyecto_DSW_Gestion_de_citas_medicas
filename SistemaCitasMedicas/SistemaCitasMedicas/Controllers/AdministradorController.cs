using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using SistemaCitasMedicas.DAO;
using SistemaCitasMedicas.Models;

namespace SistemaCitasMedicas.Controllers
{
    public class AdministradorController : Controller
    {
        // DAOs: Administrador
        usuarioDAO usuarios = new usuarioDAO();
        medicoDAO medicos = new medicoDAO();
        citaDAO citas = new citaDAO();
        ventasDAO ventas = new ventasDAO();
        especialidadDAO especialidades = new especialidadDAO();

        public ActionResult HomeAdmin()
        {
            Usuario usuario = Session["usuario"] as Usuario;
            //Panel de control
            ViewBag.cantidadusu = usuarios.listado().Count().ToString();
            ViewBag.cantidamed = medicos.listadoMedicos().Count().ToString();
            ViewBag.cantidcita= citas.citasAtendidas(8).Count().ToString();
            //Panel de control - Ventas
            ViewBag.mesactualventas = ventas.listarVentasMesActual().Count().ToString();
            ViewBag.mespasadoventas = ventas.listarVentasMesPasado().Count().ToString();
            //Panel de control - Usuarios
            ViewBag.mesactualusuarios = usuarios.listarUsuariosMesActual().Count().ToString();
            ViewBag.mespasadousuarios = usuarios.listarUsuariosMesPasado().Count().ToString();
            //Panel de control - Ingresos
            ViewBag.mesactualIngresos = ventas.listarVentasMesActual().Sum(v => v.preciotot).ToString();
            ViewBag.mespasadoIngresos = ventas.listarVentasMesPasado().Sum(v => v.preciotot).ToString();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public ActionResult MantenimientoEspecialidades(int idesp = 0)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Especialidad reg = especialidades.BuscarId(idesp);
            if (reg == null) reg = new Especialidad();
            ViewBag.especialidades = especialidades.listarEspecialidades();
            return View(reg);
        }
        [HttpPost]public ActionResult MantenimientoEspecialidades(string btncrud, Especialidad reg)
        {
            switch (btncrud)
            {
                //case "Create": return Agregar(reg);
                //case "Edit": return Actualizar(reg);
                //case "Delete": return Eliminar(reg);
                default: return RedirectToAction("Index", new { id = "" });
            }
        }
    }
}