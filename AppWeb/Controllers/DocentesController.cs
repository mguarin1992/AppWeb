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
    public class DocentesController : Controller
    {
        // GET: Docentes
        public ActionResult Index()
        {
            List<TablaDocentes> lst = null;
            using (ColegioEntities db = new ColegioEntities())
            {
                lst = (from d in db.Docentes
                       select new TablaDocentes
                       {
                           Id = d.Id_Docente,
                           Nombre1 = d.Primer_Nombre,
                           Nombre2 = d.Segundo_Nombre,
                           Apellido1 = d.Primer_Apellido,
                           Apellido2 = d.Segundo_Apellido,
                           Identificacion = (decimal)d.Identificacion,
                           Usuario = d.Usuarios.Nombre_Usuario

                       }).ToList();
            }
            return View(lst);
        }
    }
}