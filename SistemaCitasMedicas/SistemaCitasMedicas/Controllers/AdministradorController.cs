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
        sexoDAO sexos = new sexoDAO();
        tipoCuentaDAO tipos = new tipoCuentaDAO();
        distritoDAO distritos = new distritoDAO();
        estadoDAO estados = new estadoDAO();

        /*******************************COMIENZA LAS FUNCIONES DEL PANEL DE CONTROL*********************************/
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
        /*******************************TERMINA LAS FUNCIONES DEL PANEL DE CONTROL*********************************/
        /*******************************COMIENZA EL CRUD DE ESPECIALIDADES*********************************/
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
        [HttpPost]public ActionResult MantenimientoEspecialidades(string btncrud, Especialidad reg, HttpPostedFileBase archivo)
        {
            switch (btncrud)
            {
                case "Registrar": return Agregar(reg,archivo);
                case "Editar": return Actualizar(reg,archivo);
                default: return RedirectToAction("Index", new { id = "" });
            }
        }
        public ActionResult Agregar(Especialidad reg, HttpPostedFileBase archivo)
        {
            if (archivo == null || archivo.ContentLength <= 0)
            {
                ViewBag.mensaje = "Seleccione una imagen";
                ViewBag.especialidades = especialidades.listarEspecialidades();
                return View(reg);
            }
            string ruta = "~/IMAGENES/ESPECIALIDADES/" + System.IO.Path.GetFileName(archivo.FileName);
            archivo.SaveAs(Server.MapPath(ruta));
            SqlParameter[] pars =
            {
                new SqlParameter(){ParameterName="@img",Value=ruta},
                new SqlParameter(){ParameterName="@nom",Value=reg.nombreesp},
                new SqlParameter(){ParameterName="@funcion",Value=reg.descripcionesp},
            };
            ViewBag.mensaje = especialidades.CRUDESPECIALIDAD("usp_agregar_especialidad", pars, 1);
            ViewBag.especialidades = especialidades.listarEspecialidades();
            return View(reg);
        }
        public ActionResult Actualizar(Especialidad reg, HttpPostedFileBase archivo)
        {
            if (archivo == null || archivo.ContentLength <= 0)
            {
                ViewBag.mensaje = "Seleccione una imagen";
                ViewBag.especialidades = especialidades.listarEspecialidades();
                return View(reg);
            }
            string ruta = "~/IMAGENES/ESPECIALIDADES/" + System.IO.Path.GetFileName(archivo.FileName);
            archivo.SaveAs(Server.MapPath(ruta));
            SqlParameter[] pars =
            {
                new SqlParameter(){ParameterName="@idesp",Value=reg.idespecialidad},
                new SqlParameter(){ParameterName="@img",Value=ruta},
                new SqlParameter(){ParameterName="@nom",Value=reg.nombreesp},
                new SqlParameter(){ParameterName="@funcion",Value=reg.descripcionesp},
            };
            ViewBag.mensaje = especialidades.CRUDESPECIALIDAD("usp_editar_especialidad", pars, 2);
            ViewBag.especialidades = especialidades.listarEspecialidades();
            return View(reg);
        }
        /*******************************TERMINA EL CRUD DE ESPECIALIDADES*********************************/
        /*******************************COMIENZA EL CRUD DE USUARIOS*********************************/
        public ActionResult MantenimientoUsuarios(int idusu = 0)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Usuario reg = usuarios.listado().Where(u => u.idusuario == idusu).FirstOrDefault();
            if (reg == null) reg = new Usuario();
            ViewBag.sexos = sexos.listadoSexo();
            ViewBag.tipos = tipos.listadoTipoCuenta();
            ViewBag.distritos = distritos.listadoDistrito();
            ViewBag.estados = estados.listadoEstadoCuenta();
            return View(reg);
        }
    }
}