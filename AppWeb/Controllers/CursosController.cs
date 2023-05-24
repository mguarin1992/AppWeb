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
    public class CursosController : Controller
    {
        // GET: Alumnos
        public ActionResult Index()
        {
            List<TablaCursos> lst = null;
            using (ColegioEntities db = new ColegioEntities())
            {
                lst = (from d in db.Cursos
                       select new TablaCursos
                       {
                           Id = d.Id_Curso,
                           NombreCurso=d.Nombre_Curso,
                           
                       }).ToList();
            }
            return View(lst);
        }
    }
}