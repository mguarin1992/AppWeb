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
    public class ListadoMateriasController : Controller
    {
       
        public ActionResult Index()
        {
            
            return View();
        }
        
        public ActionResult Nuevo(RegistroMaterias modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                if (string.IsNullOrEmpty(modelo.Materias.Nombre_Materia))
                {
                    TempData["Message"] = "Debe ingresar un nombre para la materia";
                    return RedirectToAction("Index");
                }
                // Verificar si ya existe un usuario con el mismo nombre
                if (db.Materias.Any(u => u.Nombre_Materia == modelo.Materias.Nombre_Materia))
                {
                    TempData["Message"] = "El nombre de la materia ya existe.";
                    return RedirectToAction("Index");
                }
                else
                {
                    Materias modeloV = new Materias()
                    {
                        Nombre_Materia = modelo.Materias.Nombre_Materia,

                    };
                    new materias().Guardar(modeloV);


                    return RedirectToAction("Index", "Materias");
                }
            }
            // Si la validación falla, volver a cargar la vista con los errores
            
            return RedirectToAction("Index", modelo);
        }
        
        public ActionResult Editar(int id)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                var materias = db.Materias.Find(id);

                if (materias == null)
                {
                    return HttpNotFound();
                }

                var modelo = new EditarMaterias
                {
                    Materias = materias,
                };

                return View(modelo);
            }
        }
        [HttpPost]
        public ActionResult Editar(EditarMaterias modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                if (string.IsNullOrEmpty(modelo.Materias.Nombre_Materia))
                {
                    TempData["Message"] = "Debe ingresar un nombre para la materia";
                    return RedirectToAction("Index");
                }
                // Verificar si ya existe un curso con el mismo nombre
                if (db.Materias.Any(u => u.Nombre_Materia == modelo.Materias.Nombre_Materia && u.Id_Materia != modelo.Materias.Id_Materia))
                {
                    TempData["Message"] = "El nombre de la materia ya existe.";
                    return RedirectToAction("Index");
                }
                else
                {
                    Materias materias = db.Materias.Find(modelo.Materias.Id_Materia);
                    materias.Nombre_Materia = modelo.Materias.Nombre_Materia;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Materias");
                }
            }
        }

        public ActionResult Eliminar(int id)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                var materias = db.Materias.Find(id);

                if (materias == null)
                {
                    return HttpNotFound();
                }

                db.Materias.Remove(materias);
                db.SaveChanges();

                return RedirectToAction("Index", "Materias");
            }
        }

    }


}