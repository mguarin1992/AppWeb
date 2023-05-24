using AppWeb.filtro;
using AppWeb.Models;
using AppWeb.Models.Datos;
using AppWeb.Models.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AppWeb.Controllers
{
    //[Autorizacion_rol(modulo: "Usuarios", Autorizacion_rol.Roles.admin)]
    public class ListadoDocentesController : Controller
    {
        private IEnumerable<SelectListItem> usuarios;
        // GET: ListadoUsuarios
        public ActionResult Index()
        {
            agregar();
            RegistroDocente modelo = new RegistroDocente()
            {
                ListadoUsuarios = usuarios
            };
            return View(modelo);
        }
        public ActionResult agregar()
        {
            usuarios = new usuarios().Consultar().ToList().Select(
             p => new SelectListItem { Value = p.Id_Usuario.ToString(), Text = p.Nombre_Usuario });
            return View();
        }
        public ActionResult Nuevo(RegistroDocente modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                if (string.IsNullOrEmpty(modelo.Docentes.Primer_Nombre) ||
                   string.IsNullOrEmpty(modelo.Docentes.Primer_Apellido))
                {
                    TempData["Message"] = "Los campos Primer Nombre, Primer Apellido e Identificación son requeridos.";
                    return RedirectToAction("Index");
                }
                // Verificar si ya existe un usuario con el mismo nombre

                Docentes modeloV = new Docentes()
                    {
                        Primer_Nombre = modelo.Docentes.Primer_Nombre,
                        Segundo_Nombre = modelo.Docentes.Segundo_Nombre,
                        Primer_Apellido = modelo.Docentes.Primer_Apellido,
                        Segundo_Apellido = modelo.Docentes.Segundo_Apellido,
                        Identificacion = modelo.Docentes.Identificacion,
                        Id_Usuario = modelo.Docentes.Id_Usuario
                    };
                    new docentes().Guardar(modeloV);
                    agregar();
                    modelo = new RegistroDocente()
                    {
                        ListadoUsuarios = usuarios
                    };
                    return RedirectToAction("Index", "Docentes");
                
            }
            // Si la validación falla, volver a cargar la vista con los errores
            modelo.ListadoUsuarios = usuarios;
            return RedirectToAction("Index", modelo);
        }
        
        public ActionResult Editar(int id)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                var docente = db.Docentes.Find(id);

                if (docente == null)
                {
                    return HttpNotFound();
                }

                var modelo = new EditarDocente
                {
                    Docentes = docente,
                    ListadoUsuarios = db.Usuarios.Select(r => new SelectListItem
                    {
                        Value = r.Id_Usuario.ToString(),
                        Text = r.Nombre_Usuario
                    }).ToList()
                };

                return View(modelo);
            }
        }
        [HttpPost]
        public ActionResult Editar(EditarDocente modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                // Verificar si ya existe un usuario con el mismo nombre

                Docentes docentes = db.Docentes.Find(modelo.Docentes.Id_Docente);
                docentes.Primer_Nombre = modelo.Docentes.Primer_Nombre;
                docentes.Segundo_Nombre = modelo.Docentes.Segundo_Nombre;
                docentes.Primer_Apellido = modelo.Docentes.Primer_Apellido;
                docentes.Segundo_Apellido = modelo.Docentes.Segundo_Apellido;
                docentes.Identificacion = modelo.Docentes.Identificacion;
                docentes.Id_Usuario = modelo.Docentes.Id_Usuario;
                db.SaveChanges();
                return RedirectToAction("Index", "Docentes");

            }
        }

        public ActionResult Eliminar(int id)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                var docentes = db.Docentes.Find(id);

                if (docentes == null)
                {
                    return HttpNotFound();
                }

                db.Docentes.Remove(docentes);
                db.SaveChanges();

                return RedirectToAction("Index", "Docentes");
            }
        }

    }


}