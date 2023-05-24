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
    
    public class MateriasController : Controller
    {
        
        // GET: Usuarios
        public ActionResult Index()
        {
             List<TablaMaterias> lst = null;
            using(ColegioEntities db = new ColegioEntities())
            {
                lst = (from d in db.Materias
                       select new TablaMaterias
                       {
                           Id = d.Id_Materia,
                           Nombre_Materia = d.Nombre_Materia
                       }).ToList();
            }
            return View(lst);
        }
    }
}