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
    public class CursoAlumnosController : Controller
    {
        private IEnumerable<SelectListItem> alumnos;
        private IEnumerable<SelectListItem> cursos;
        // GET: ListadoUsuarios
        public ActionResult Index()
        {
            agregar();
            RegistroAlumnoCurso modelo = new RegistroAlumnoCurso()
            {
                ListadoAlumnos = alumnos,
                ListadoCurso = cursos
            };
            return View(modelo);
        }
        public ActionResult agregar()
        {
            alumnos = new alumnos().Consultar().ToList().Select(
             p => new SelectListItem { Value = p.Id_Alumno.ToString(), Text = $"{p.Primer_Nombre} {p.Primer_Apellido} {p.Identificacion}" });
            cursos = new cursos().Consultar().ToList().Select(
             p => new SelectListItem { Value = p.Id_Curso.ToString(), Text = p.Nombre_Curso });
            return View();
        }
        public ActionResult Nuevo(RegistroAlumnoCurso modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {

                // Verificar si ya existe un usuario con el mismo nombre
                
                   AlumCur modeloV = new AlumCur()
                    {
                       Id_Alumno = modelo.AlumCur.Id_Alumno,
                       Id_Curso = modelo.AlumCur.Id_Curso
                   };
                    new alumnoscursos().Guardar(modeloV);
                    agregar();
                    modelo = new RegistroAlumnoCurso()
                    {
                        ListadoAlumnos = alumnos,
                        ListadoCurso = cursos
                    };
                    return RedirectToAction("Index", "Cursos");
                
            }
            // Si la validación falla, volver a cargar la vista con los errores
            modelo.ListadoAlumnos = alumnos;
            modelo.ListadoCurso = cursos;
            return RedirectToAction("Index", modelo);
        }
        
        public ActionResult Editar(int id)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                var usuario = db.Usuarios.Find(id);

                if (usuario == null)
                {
                    return HttpNotFound();
                }

                var modelo = new EditarUsuario
                {
                    Usuarios = usuario,
                    ListadoRoles = db.Rol.Select(r => new SelectListItem
                    {
                        Value = r.Id_rol.ToString(),
                        Text = r.nombre_rol
                    }).ToList()
                };

                return View(modelo);
            }
        }
        [HttpPost]
        public ActionResult Editar(EditarUsuario modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                if (ModelState.IsValid)
                {
                        Usuarios usuario = db.Usuarios.Find(modelo.Usuarios.Id_Usuario);

                        if (usuario != null)
                        {
                            usuario.Nombre_Usuario = modelo.Usuarios.Nombre_Usuario;
                            
                            usuario.Id_rol = modelo.Usuarios.Id_rol;

                            db.SaveChanges();
                        }
                    

                    return RedirectToAction("Index", "AlumnosAdmin");
                }

                // Si la validación falla, volver a cargar la vista con los errores
                modelo.ListadoRoles = db.Rol.Select(r => new SelectListItem
                {
                    Value = r.Id_rol.ToString(),
                    Text = r.nombre_rol
                }).ToList();
                return View("Editar", modelo);
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