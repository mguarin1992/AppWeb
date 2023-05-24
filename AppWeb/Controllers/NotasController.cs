using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppWeb.Models;
using AppWeb.Models.Tablas;
using AppWeb.Models.Modelos;

namespace AppWeb.Controllers
{
    public class NotasController : Controller
    {
        // GET: Alumnos
        public ActionResult Index(int? IdAlumno)
        {
            

            List<TablaNotas> lst = null;
            using (ColegioEntities db = new ColegioEntities())
            {
                lst = (from d in db.Notas
                       where d.Id_Alumno == IdAlumno
                       select new TablaNotas
                       {
                           Id=d.Id_Nota,
                           Materia=d.Materias.Nombre_Materia,
                           Nota = d.Nota != null ? (int)d.Nota : 0

                       }).ToList();
            }
            return View(lst);
        }
        public ActionResult ReMaterias(int? IdAlumno)
        {
            List<TablaNotas> lst = null;
            using (ColegioEntities db = new ColegioEntities())
            {
                lst = (from d in db.Notas
                       where d.Id_Alumno == IdAlumno
                       select new TablaNotas
                       {
                           Id = d.Id_Nota,
                           Materia = d.Materias.Nombre_Materia,
                           Nota = d.Nota != null ? (int)d.Nota : 0

                       }).ToList();
            }
            return View(lst);
        }
    }
}