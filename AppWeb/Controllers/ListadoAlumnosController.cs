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
    public class ListadoAlumnosController : Controller
    {
        private IEnumerable<SelectListItem> usuarios;
        // GET: ListadoUsuarios
        public ActionResult Index()
        {
            agregar();
            RegistroAlumno modelo = new RegistroAlumno()
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
        public ActionResult Nuevo(RegistroAlumno modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                if (string.IsNullOrEmpty(modelo.Alumnos.Primer_Apellido) ||
                   string.IsNullOrEmpty(modelo.Alumnos.Primer_Nombre))
                {
                    TempData["Message"] = "Los campos Primer Nombre, Primer Apellido e Identificación son requeridos.";
                    return RedirectToAction("Index");
                }
                // Verificar si ya existe un usuario con el mismo nombre

                Alumnos modeloV = new Alumnos()
                    {
                        Primer_Nombre = modelo.Alumnos.Primer_Nombre,
                        Segundo_Nombre = modelo.Alumnos.Segundo_Nombre,
                        Primer_Apellido = modelo.Alumnos.Primer_Apellido,
                        Segundo_Apellido = modelo.Alumnos.Segundo_Apellido,
                        Identificacion = modelo.Alumnos.Identificacion,
                        Telefono = modelo.Alumnos.Telefono,
                        Id_Usuario = modelo.Alumnos.Id_Usuario
                    };
                    new alumnos().Guardar(modeloV);
                    agregar();
                    modelo = new RegistroAlumno()
                    {
                        ListadoUsuarios = usuarios
                    };
                    return RedirectToAction("Index", "AlumnosAdmin");
                
            }
            // Si la validación falla, volver a cargar la vista con los errores
            modelo.ListadoUsuarios = usuarios;
            return RedirectToAction("Index", modelo);
        }
        
        public ActionResult Editar(int id)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                var alumnos = db.Alumnos.Find(id);

                if (alumnos == null)
                {
                    return HttpNotFound();
                }

                var modelo = new EditarAlumno
                {
                    Alumnos = alumnos,
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
        public ActionResult Editar(EditarAlumno modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                // Verificar si ya existe un usuario con el mismo nombre
               
                    Alumnos alumnos = db.Alumnos.Find(modelo.Alumnos.Id_Alumno);
                    alumnos.Primer_Nombre = modelo.Alumnos.Primer_Nombre;
                    alumnos.Segundo_Nombre = modelo.Alumnos.Segundo_Nombre;
                    alumnos.Primer_Apellido = modelo.Alumnos.Primer_Apellido;
                    alumnos.Segundo_Apellido = modelo.Alumnos.Segundo_Apellido;
                    alumnos.Identificacion = modelo.Alumnos.Identificacion;
                    alumnos.Telefono = modelo.Alumnos.Telefono;
                    alumnos.Id_Usuario = modelo.Alumnos.Id_Usuario;
                    db.SaveChanges();
                    return RedirectToAction("Index", "AlumnosAdmin");
                
            }
        }

        public ActionResult Eliminar(int id)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                var alumnos = db.Alumnos.Find(id);

                if (alumnos == null)
                {
                    return HttpNotFound();
                }

                db.Alumnos.Remove(alumnos);
                db.SaveChanges();

                return RedirectToAction("Index", "AlumnosAdmin");
            }
        }

    }


}