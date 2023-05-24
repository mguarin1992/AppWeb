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
    public class ListadoCursosController : Controller
    {
       
        public ActionResult Index()
        {
            
            return View();
        }
        
        public ActionResult Nuevo(RegistroCurso modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                if (string.IsNullOrEmpty(modelo.Cursos.Nombre_Curso))
                {
                    TempData["Message"] = "Debe ingresar un nombre para el curso";
                    return RedirectToAction("Index");
                }
                // Verificar si ya existe un usuario con el mismo nombre
                if (db.Cursos.Any(u => u.Nombre_Curso == modelo.Cursos.Nombre_Curso))
                {
                    TempData["Message"] = "El nombre de curso ya existe.";
                    return RedirectToAction("Index");
                }
                else
                {
                    Cursos modeloV = new Cursos()
                    {
                        Nombre_Curso = modelo.Cursos.Nombre_Curso,

                    };
                    new cursos().Guardar(modeloV);


                    return RedirectToAction("Index", "Cursos");
                }
            }
            // Si la validación falla, volver a cargar la vista con los errores
            
            return RedirectToAction("Index", modelo);
        }
        
        public ActionResult Editar(int id)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                var cursos = db.Cursos.Find(id);

                if (cursos == null)
                {
                    return HttpNotFound();
                }

                var modelo = new EditarCurso
                {
                    Cursos = cursos,
                };

                return View(modelo);
            }
        }
        [HttpPost]
        public ActionResult Editar(EditarCurso modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                if (string.IsNullOrEmpty(modelo.Cursos.Nombre_Curso))
                {
                    TempData["Message"] = "Debe ingresar un nombre para el curso";
                    return RedirectToAction("Index");
                }
                // Verificar si ya existe un curso con el mismo nombre
                if (db.Cursos.Any(u => u.Nombre_Curso == modelo.Cursos.Nombre_Curso && u.Id_Curso != modelo.Cursos.Id_Curso))
                {
                    TempData["Message"] = "El nombre del curso ya existe.";
                    return RedirectToAction("Index");
                }
                else
                {
                    Cursos cursos = db.Cursos.Find(modelo.Cursos.Id_Curso);
                    cursos.Nombre_Curso = modelo.Cursos.Nombre_Curso;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Cursos");
                }
            }
        }

        public ActionResult Eliminar(int id)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                var cursos = db.Cursos.Find(id);

                if (cursos == null)
                {
                    return HttpNotFound();
                }

                db.Cursos.Remove(cursos);
                db.SaveChanges();

                return RedirectToAction("Index", "cursos");
            }
        }

    }


}