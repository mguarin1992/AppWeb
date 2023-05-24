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
    public class AlumnosController : Controller
    {
        // GET: Alumnos
        public ActionResult Index(int? IdCurso)
        {
            List<TablaAlumnos> lst = null;
            using (ColegioEntities db = new ColegioEntities())
            {
                if (IdCurso == null)
                {
                    lst = (from d in db.AlumCur
                           select new TablaAlumnos
                           {
                               Id = d.Alumnos.Id_Alumno,
                               Nombre1 = d.Alumnos.Primer_Nombre,
                               Nombre2 = d.Alumnos.Segundo_Nombre,
                               Apellido1 = d.Alumnos.Primer_Apellido,
                               Apellido2 = d.Alumnos.Segundo_Apellido,
                               Identificacion = (decimal)d.Alumnos.Identificacion,
                               Telefono = (decimal)d.Alumnos.Telefono,
                               Usuario = d.Alumnos.Usuarios.Nombre_Usuario
                           }).ToList();
                }
                else
                {
                    lst = (from d in db.AlumCur
                           where d.Id_Curso == IdCurso
                           select new TablaAlumnos
                           {
                               Id = d.Alumnos.Id_Alumno,
                               Nombre1 = d.Alumnos.Primer_Nombre,
                               Nombre2 = d.Alumnos.Segundo_Nombre,
                               Apellido1 = d.Alumnos.Primer_Apellido,
                               Apellido2 = d.Alumnos.Segundo_Apellido,
                               Identificacion = (decimal)d.Alumnos.Identificacion,
                               Telefono = (decimal)d.Alumnos.Telefono,
                               Usuario = d.Alumnos.Usuarios.Nombre_Usuario
                           }).ToList();
                }
            }
            return View(lst);
        }
    }
}