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
    public class ListadoNotasController : Controller
    {
        private IEnumerable<SelectListItem> materias;
        private IEnumerable<SelectListItem> alumnos;
        // GET: ListadoUsuarios
        public ActionResult Index()
        {
            agregar();
            RegistroAlumnoNota modelo = new RegistroAlumnoNota()
            {
                ListadoMaterias = materias,
                ListadoAlumnos = alumnos
            };
            return View(modelo);
        }

        public ActionResult agregar()
        {
            alumnos = new alumnos().Consultar().ToList().Select(
             p => new SelectListItem { Value = p.Id_Alumno.ToString(), Text = $"{p.Primer_Nombre} {p.Primer_Apellido} {p.Identificacion}" });
            materias = new materias().Consultar().ToList().Select(
             p => new SelectListItem { Value = p.Id_Materia.ToString(), Text = p.Nombre_Materia });
            return View();
        }
        public ActionResult Nuevo(RegistroAlumnoNota modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {

                var registroExistente = db.Notas.FirstOrDefault(an => an.Id_Alumno == modelo.Notas.Id_Alumno && an.Id_Materia == modelo.Notas.Id_Materia);
                if (registroExistente != null)
                {
                    TempData["Message"] = "El alumno ya tiene registrada la materia seleccionada.";
                    return RedirectToAction("Index", "ListadoNotas");
                }

                // Si no existe el registro, guardarlo en la tabla "AlumnosNotas"
                Notas modeloV = new Notas()
                {
                    Id_Materia = modelo.Notas.Id_Materia,
                    Id_Alumno = modelo.Notas.Id_Alumno
                };
                new alumnosnotas().Guardar(modeloV);
                agregar();
                modelo = new RegistroAlumnoNota()
                {
                    ListadoMaterias = materias,
                    ListadoAlumnos = alumnos
                };
                return RedirectToAction("Index", "MisNotas");

            }
            // Si la validación falla, volver a cargar la vista con los errores
            modelo.ListadoMaterias = materias;
            return RedirectToAction("Index", modelo);
        }
        public ActionResult Editar(int id)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                var notas = db.Notas.Find(id);

                if (notas == null)
                {
                    return HttpNotFound();
                }

                var modelo = new EditarNotas
                {
                    Notas = notas,
                };

                return View(modelo);
            }
        }
        [HttpPost]
        public ActionResult Editar(EditarNotas modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                // Verificar si ya existe un curso con el mismo nombre
               
                    Notas notas = db.Notas.Find(modelo.Notas.Id_Nota);
                    notas.Nota = modelo.Notas.Nota;
                    db.SaveChanges();

                int idAlumno =(int) notas.Id_Alumno;

                return RedirectToAction("Index", "Notas", new { IdAlumno = idAlumno });

            }
        }


        public ActionResult Eliminar(int id)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                var notas = db.Notas.Find(id);

                if (notas == null)
                {
                    return HttpNotFound();
                }

                db.Notas.Remove(notas);
                db.SaveChanges();

                return RedirectToAction("Index", "Notas");
            }
        }

    }


}