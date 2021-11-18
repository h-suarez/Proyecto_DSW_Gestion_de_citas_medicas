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
        proveedorDAO proveedores = new proveedorDAO();
        farmaceuticoDAO farmaceuticos = new farmaceuticoDAO();

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
                case "Registrar": return AgregarEsp(reg,archivo);
                case "Editar": return ActualizarEsp(reg,archivo);
                default: return RedirectToAction("MantenimientoEspecialidades", new { idesp = "" });
            }
        }
        public ActionResult AgregarEsp(Especialidad reg, HttpPostedFileBase archivo)
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
        public ActionResult ActualizarEsp(Especialidad reg, HttpPostedFileBase archivo)
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
            ViewBag.sexos = new SelectList(sexos.listadoSexo(), "idsexo", "descripcion", reg.idsexo);
            ViewBag.tipos = new SelectList(tipos.listadoTipoCuenta(), "idtipo", "descripcion", reg.idtipo);
            ViewBag.distritos = new SelectList(distritos.listadoDistrito(), "iddistrito", "descripcion", reg.iddistrito);
            ViewBag.estados = new SelectList(estados.listadoEstadoCuenta(), "idestado", "descripcion", reg.idestado);
            ViewBag.usuarios = usuarios.listadoManUsuarios();
            return View(reg);
        }
        [HttpPost]public ActionResult MantenimientoUsuarios(string btncrud, Usuario reg)
        {
            switch (btncrud)
            {
                case "Registrar": return AgregarUsu(reg);
                case "Actualizar": return ActualizarUsu(reg);
                default: return RedirectToAction("MantenimientoUsuarios", new { id = "" });
            }
        }
        public ActionResult AgregarUsu(Usuario reg){
            SqlParameter[] pars =
            {
                new SqlParameter(){ParameterName="@dni",Value=reg.dniusu},
                new SqlParameter(){ParameterName="@nom",Value=reg.nombreusu},
                new SqlParameter(){ParameterName="@sexo",Value=reg.idsexo},
                new SqlParameter(){ParameterName="@distrito",Value=reg.iddistrito},
                new SqlParameter(){ParameterName="@cel",Value=reg.celularusu},
                new SqlParameter(){ParameterName="@email",Value=reg.email},
                new SqlParameter(){ParameterName="@clave",Value=reg.clave},
                new SqlParameter(){ParameterName="@tipo",Value=reg.idtipo},
                new SqlParameter(){ParameterName="@estado",Value=reg.idestado},
            };
            ViewBag.mensaje = usuarios.CRUDUSUCUENTAS("usp_agregar_empleado", pars, 1);
            ViewBag.sexos = new SelectList(sexos.listadoSexo(), "idsexo", "descripcion", reg.idsexo);
            ViewBag.tipos = new SelectList(tipos.listadoTipoCuenta(), "idtipo", "descripcion", reg.idtipo);
            ViewBag.distritos = new SelectList(distritos.listadoDistrito(), "iddistrito", "descripcion", reg.iddistrito);
            ViewBag.estados = new SelectList(estados.listadoEstadoCuenta(), "idestado", "descripcion", reg.idestado);
            ViewBag.usuarios = usuarios.listadoManUsuarios();
            return View(reg);
        }
        public ActionResult ActualizarUsu(Usuario reg){
            SqlParameter[] pars =
            {
                new SqlParameter(){ParameterName="@idcli",Value=reg.idusuario},
                new SqlParameter(){ParameterName="@dni",Value=reg.dniusu},
                new SqlParameter(){ParameterName="@nom",Value=reg.nombreusu},
                new SqlParameter(){ParameterName="@sexo",Value=reg.idsexo},
                new SqlParameter(){ParameterName="@distrito",Value=reg.iddistrito},
                new SqlParameter(){ParameterName="@cel",Value=reg.celularusu},
                new SqlParameter(){ParameterName="@email",Value=reg.email},
                new SqlParameter(){ParameterName="@clave",Value=reg.clave},
                new SqlParameter(){ParameterName="@tipo",Value=reg.idtipo},
                new SqlParameter(){ParameterName="@estado",Value=reg.idestado},
            };
            ViewBag.mensaje = usuarios.CRUDUSUCUENTAS("usp_editar_empleado", pars, 2);
            ViewBag.sexos = new SelectList(sexos.listadoSexo(), "idsexo", "descripcion", reg.idsexo);
            ViewBag.tipos = new SelectList(tipos.listadoTipoCuenta(), "idtipo", "descripcion", reg.idtipo);
            ViewBag.distritos = new SelectList(distritos.listadoDistrito(), "iddistrito", "descripcion", reg.iddistrito);
            ViewBag.estados = new SelectList(estados.listadoEstadoCuenta(), "idestado", "descripcion", reg.idestado);
            ViewBag.usuarios = usuarios.listadoManUsuarios();
            return View(reg);
        }
        /*******************************TERMINA EL CRUD DE USUARIOS*********************************/
        /*******************************COMIENZA EL CRUD DE PROVEEDORES*********************************/
        public ActionResult MantenimientoProveedores(int idprov = 0)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Proveedor reg = proveedores.listarProveedores().Where(p => p.idproveedor == idprov).FirstOrDefault();
            if (reg == null) reg = new Proveedor();
            ViewBag.distritos = new SelectList(distritos.listadoDistrito(), "iddistrito", "descripcion", reg.iddistrito);
            ViewBag.estados = new SelectList(estados.listadoEstadoProveedor(), "idestado", "descripcion", reg.idestado);
            ViewBag.proveedores = proveedores.listadoManProveedor();
            return View(reg);
        }
        [HttpPost]public ActionResult MantenimientoProveedores(string btncrud, Proveedor reg)
        {
            switch (btncrud)
            {
                case "Registrar": return AgregarProv(reg);
                case "Actualizar": return ActualizarProv(reg);
                default: return RedirectToAction("MantenimientoProveedores", new { id = "" });
            }
        }
        public ActionResult AgregarProv(Proveedor reg)
        {
            SqlParameter[] pars =
            {
                new SqlParameter(){ParameterName="@ruc",Value=reg.rucprov},
                new SqlParameter(){ParameterName="@nom",Value=reg.nombreprov},
                new SqlParameter(){ParameterName="@tel",Value=reg.telefonoprov},
                new SqlParameter(){ParameterName="@rz",Value=reg.razonprov},
                new SqlParameter(){ParameterName="@distrito",Value=reg.iddistrito},
                new SqlParameter(){ParameterName="@direccion",Value=reg.direccionprov},
            };
            ViewBag.mensaje = proveedores.CRUDPROVEEDOR("usp_agregar_proveedor", pars, 1);
            ViewBag.distritos = new SelectList(distritos.listadoDistrito(), "iddistrito", "descripcion", reg.iddistrito);
            ViewBag.estados = new SelectList(estados.listadoEstadoProveedor(), "idestado", "descripcion", reg.idestado);
            ViewBag.proveedores = proveedores.listadoManProveedor();
            return View(reg);
        }
        public ActionResult ActualizarProv(Proveedor reg)
        {
            SqlParameter[] pars =
            {
                new SqlParameter(){ParameterName="@idprov",Value=reg.idproveedor},
                new SqlParameter(){ParameterName="@ruc",Value=reg.rucprov},
                new SqlParameter(){ParameterName="@nom",Value=reg.nombreprov},
                new SqlParameter(){ParameterName="@tel",Value=reg.telefonoprov},
                new SqlParameter(){ParameterName="@rz",Value=reg.razonprov},
                new SqlParameter(){ParameterName="@distrito",Value=reg.iddistrito},
                new SqlParameter(){ParameterName="@direccion",Value=reg.direccionprov},
                new SqlParameter(){ParameterName="@estado",Value=reg.idestado},
            };
            ViewBag.mensaje = proveedores.CRUDPROVEEDOR("usp_editar_proveedor", pars, 2);
            ViewBag.distritos = new SelectList(distritos.listadoDistrito(), "iddistrito", "descripcion", reg.iddistrito);
            ViewBag.estados = new SelectList(estados.listadoEstadoProveedor(), "idestado", "descripcion", reg.idestado);
            ViewBag.proveedores = proveedores.listadoManProveedor();
            return View(reg);
        }
        /*******************************TERMINA EL CRUD DE PROVEEDORES*********************************/
        /*******************************COMIENZA EL CRUD DE FARMACEUTICOS*********************************/
        public ActionResult MantenimientoFarmaceuticos(int idfarm = 0)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Farmaceutico reg = farmaceuticos.listarFarmaceuticos().Where(p => p.idfarmaceutico == idfarm).FirstOrDefault();
            if (reg == null) reg = new Farmaceutico();
            ViewBag.proveedores = new SelectList(proveedores.listarProveedores(), "idproveedor", "nombreprov", reg.idproveedor);
            ViewBag.estados = new SelectList(estados.listadoEstadoFarmaceutico(), "idestado", "descripcion", reg.idestado);
            ViewBag.farmaceuticos = farmaceuticos.listarFarmaceuticos();
            return View(reg);
        }
        [HttpPost]public ActionResult MantenimientoFarmaceuticos(string btncrud, Farmaceutico reg, HttpPostedFileBase archivo)
        {
            switch (btncrud)
            {
                case "Registrar": return AgregarFarm(reg, archivo);
                case "Editar": return ActualizarFarm(reg, archivo);
                default: return RedirectToAction("MantenimientoFarmaceuticos", new { idfarm = "" });
            }
        }
        public ActionResult AgregarFarm(Farmaceutico reg, HttpPostedFileBase archivo)
        {
            if (archivo == null || archivo.ContentLength <= 0)
            {
                ViewBag.mensaje = "Seleccione una imagen";
                ViewBag.proveedores = new SelectList(proveedores.listarProveedores(), "idproveedor", "nombreprov", reg.idproveedor);
                ViewBag.estados = new SelectList(estados.listadoEstadoFarmaceutico(), "idestado", "descripcion", reg.idestado);
                ViewBag.farmaceuticos = farmaceuticos.listarFarmaceuticos();
                return View(reg);
            }
            string ruta = "~/IMAGENES/FARMACEUTICOS/" + System.IO.Path.GetFileName(archivo.FileName);
            archivo.SaveAs(Server.MapPath(ruta));
            SqlParameter[] pars =
            {
                new SqlParameter(){ParameterName="@foto",Value=ruta},
                new SqlParameter(){ParameterName="@nom",Value=reg.nombrefarm},
                new SqlParameter(){ParameterName="@stock",Value=reg.stockfarm},
                new SqlParameter(){ParameterName="@precio",Value=reg.preciofarm},
                new SqlParameter(){ParameterName="@desc",Value=reg.descripcionfarm},
                new SqlParameter(){ParameterName="@proveedor",Value=reg.idproveedor},
            };
            ViewBag.mensaje = farmaceuticos.CRUDFARMACEUTICOS("usp_agregar_farmaceutico", pars, 1);
            ViewBag.proveedores = new SelectList(proveedores.listarProveedores(), "idproveedor", "nombreprov", reg.idproveedor);
            ViewBag.estados = new SelectList(estados.listadoEstadoFarmaceutico(), "idestado", "descripcion", reg.idestado);
            ViewBag.farmaceuticos = farmaceuticos.listarFarmaceuticos();
            return View(reg);
        }
        public ActionResult ActualizarFarm(Farmaceutico reg, HttpPostedFileBase archivo)
        {
            if (archivo == null || archivo.ContentLength <= 0)
            {
                ViewBag.mensaje = "Seleccione una imagen";
                ViewBag.proveedores = new SelectList(proveedores.listarProveedores(), "idproveedor", "nombreprov", reg.idproveedor);
                ViewBag.estados = new SelectList(estados.listadoEstadoFarmaceutico(), "idestado", "descripcion", reg.idestado);
                ViewBag.farmaceuticos = farmaceuticos.listarFarmaceuticos();
                return View(reg);
            }
            string ruta = "~/IMAGENES/FARMACEUTICOS/" + System.IO.Path.GetFileName(archivo.FileName);
            archivo.SaveAs(Server.MapPath(ruta));
            SqlParameter[] pars =
            {
                new SqlParameter(){ParameterName="@idfarm",Value=reg.idfarmaceutico},
                new SqlParameter(){ParameterName="@foto",Value=ruta},
                new SqlParameter(){ParameterName="@nom",Value=reg.nombrefarm},
                new SqlParameter(){ParameterName="@stock",Value=reg.stockfarm},
                new SqlParameter(){ParameterName="@precio",Value=reg.preciofarm},
                new SqlParameter(){ParameterName="@desc",Value=reg.descripcionfarm},
                new SqlParameter(){ParameterName="@proveedor",Value=reg.idproveedor},
                new SqlParameter(){ParameterName="@estado",Value=reg.idestado},
            };
            ViewBag.mensaje = farmaceuticos.CRUDFARMACEUTICOS("usp_editar_farmaceutico", pars, 2);
            ViewBag.proveedores = new SelectList(proveedores.listarProveedores(), "idproveedor", "nombreprov", reg.idproveedor);
            ViewBag.estados = new SelectList(estados.listadoEstadoFarmaceutico(), "idestado", "descripcion", reg.idestado);
            ViewBag.farmaceuticos = farmaceuticos.listarFarmaceuticos();
            return View(reg);
        }
        /*******************************TERMINA EL CRUD DE FARMACEUTICOS*********************************/
        /*******************************COMIENZA EL CRUD DE MEDICOS*********************************/
        public ActionResult MantenimientoMedicos(int idmed = 0)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Medico reg = medicos.listadoMedicos().Where(m => m.idmedico == idmed).FirstOrDefault();
            if (reg == null) reg = new Medico();
            ViewBag.especialidades = new SelectList(especialidades.listarEspecialidades(), "idespecialidad", "nombreesp", reg.idespecialidad);
            ViewBag.cuentas = new SelectList(usuarios.listadoCombo().Where(u => u.tipo == "Paciente" || u.tipo == "Medico"), "idusuario", "email", reg.idcuenta);
            ViewBag.medicos = medicos.listadoManteniminetoMedicos();
            return View(reg);
        }
        [HttpPost]public ActionResult MantenimientoMedicos(string btncrud, Medico reg, HttpPostedFileBase archivo)
        {
            switch (btncrud)
            {
                case "Registrar": return AgregarMed(reg, archivo);
                case "Editar": return ActualizarMed(reg, archivo);
                default: return RedirectToAction("MantenimientoMedicos", new { idmed = "" });
            }
        }
        public ActionResult AgregarMed(Medico reg, HttpPostedFileBase archivo)
        {
            if (archivo == null || archivo.ContentLength <= 0)
            {
                ViewBag.mensaje = "Seleccione una imagen";
                ViewBag.especialidades = new SelectList(especialidades.listarEspecialidades(), "idespecialidad", "nombreesp", reg.idespecialidad);
                ViewBag.cuentas = new SelectList(usuarios.listadoCombo().Where(u => u.tipo == "Paciente" || u.tipo == "Medico"), "idusuario", "email", reg.idcuenta);
                ViewBag.medicos = medicos.listadoManteniminetoMedicos();
                return View(reg);
            }
            string ruta = "~/IMAGENES/MEDICOS/" + System.IO.Path.GetFileName(archivo.FileName);
            archivo.SaveAs(Server.MapPath(ruta));
            SqlParameter[] pars =
            {
                new SqlParameter(){ParameterName="@img",Value=ruta},
                new SqlParameter(){ParameterName="@especialidad",Value=reg.idespecialidad},
                new SqlParameter(){ParameterName="@horaini",Value=reg.horaini},
                new SqlParameter(){ParameterName="@horafin",Value=reg.horafin},
                new SqlParameter(){ParameterName="@cuenta",Value=reg.idcuenta},
            };
            ViewBag.mensaje = medicos.CRUDMEDICO("usp_agregar_medico", pars, 1);
            ViewBag.especialidades = new SelectList(especialidades.listarEspecialidades(), "idespecialidad", "nombreesp", reg.idespecialidad);
            ViewBag.cuentas = new SelectList(usuarios.listadoCombo().Where(u => u.tipo == "Paciente" || u.tipo == "Medico"), "idusuario", "email", reg.idcuenta);
            ViewBag.medicos = medicos.listadoManteniminetoMedicos();
            return View(reg);
        }
        public ActionResult ActualizarMed(Medico reg, HttpPostedFileBase archivo)
        {
            if (archivo == null || archivo.ContentLength <= 0)
            {
                ViewBag.mensaje = "Seleccione una imagen";
                ViewBag.especialidades = new SelectList(especialidades.listarEspecialidades(), "idespecialidad", "nombreesp", reg.idespecialidad);
                ViewBag.cuentas = new SelectList(usuarios.listadoCombo().Where(u => u.tipo == "Paciente" || u.tipo == "Medico"), "idusuario", "email", reg.idcuenta);
                ViewBag.medicos = medicos.listadoManteniminetoMedicos();
                return View(reg);
            }
            string ruta = "~/IMAGENES/MEDICOS/" + System.IO.Path.GetFileName(archivo.FileName);
            archivo.SaveAs(Server.MapPath(ruta));
            SqlParameter[] pars =
            {
                new SqlParameter(){ParameterName="@idmed",Value=reg.idmedico},
                new SqlParameter(){ParameterName="@img",Value=ruta},
                new SqlParameter(){ParameterName="@especialidad",Value=reg.idespecialidad},
                new SqlParameter(){ParameterName="@horaini",Value=reg.horaini},
                new SqlParameter(){ParameterName="@horafin",Value=reg.horafin},
                new SqlParameter(){ParameterName="@cuenta",Value=reg.idcuenta},
            };
            ViewBag.mensaje = medicos.CRUDMEDICO("usp_editar_medico", pars, 2);
            ViewBag.especialidades = new SelectList(especialidades.listarEspecialidades(), "idespecialidad", "nombreesp", reg.idespecialidad);
            ViewBag.cuentas = new SelectList(usuarios.listadoCombo().Where(u => u.tipo == "Paciente" || u.tipo == "Medico"), "idusuario", "email", reg.idcuenta);
            ViewBag.medicos = medicos.listadoManteniminetoMedicos();
            return View(reg);
        }
        /*******************************TERMINA EL CRUD DE MEDICOS*********************************/
        /*******************************COMIENZA LA ATENCIÓN DE CITAS MEDICAS*********************************/
        public ActionResult ConsultaCitasMedicas(int idcit = 0)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Cita reg = citas.listadoCitas().Where(c => c.idcita == idcit).FirstOrDefault();
            if (reg == null) reg = new Cita();
            ViewBag.citasdemedico = citas.listadoConsultaCitaMedico(usuario.idusuario);
            ViewBag.estados = new SelectList(estados.listadoEstadoCita(), "idestado", "descripcion", reg.idestado);
            ViewBag.paciente = new SelectList(usuarios.listado().Where(p => p.idtipo == 2), "idusuario", "nombreusu", reg.idusuario);
            return View(reg);
        }
        [HttpPost]public ActionResult ConsultaCitasMedicas(string btnactualizar, Cita reg)
        {
            switch (btnactualizar)
            {
                case "Pagar": return AtenderCita(reg);
                default: return RedirectToAction("ConsultaCitasMedicas", new { idcit = "" });
            }
        }
        public ActionResult AtenderCita(Cita reg)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            if (usuario == null)
            {
                return RedirectToAction("Login", "Home");
            }
            SqlParameter[] pars =
            {
                new SqlParameter(){ParameterName="@idcita",Value=reg.idcita},
                new SqlParameter(){ParameterName="@observaciones",Value=reg.observaciones},
                new SqlParameter(){ParameterName="@prescripcion",Value=reg.prescripcion},
                new SqlParameter(){ParameterName="@pago",Value=reg.pagocita},
                new SqlParameter(){ParameterName="@estado",Value=reg.idestado},
            };
            ViewBag.mensaje = citas.METODOSCITA("usp_editar_cita", pars, 2);
            ViewBag.citasdemedico = citas.listadoConsultaCitaMedico(usuario.idusuario);
            ViewBag.estados = new SelectList(estados.listadoEstadoCita(), "idestado", "descripcion", reg.idestado);
            ViewBag.paciente = new SelectList(usuarios.listado().Where(p => p.idtipo == 2), "idusuario", "nombreusu", reg.idusuario);
            return View(reg);
        }
        /*******************************TERMINA LA ATENCIÓN DE CITAS MEDICAS*********************************/
        /*******************************COMENZAR REGISTRO DE VENTAS*********************************/

    }
}