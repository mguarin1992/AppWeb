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
    public class MisNotasController : Controller
    {
        // GET: Alumnos
        public ActionResult Index()
        {
            List<TablaAlumnos> lst = new List<TablaAlumnos>();
            using (ColegioEntities db = new ColegioEntities())
            {
                Usuarios usuarios = (Usuarios)Session["User"];
                if (usuarios != null)
                {
                    // Buscar el alumno asociado al usuario actual
                    Alumnos alumno = db.Alumnos.FirstOrDefault(a => a.Id_Usuario == usuarios.Id_Usuario);
                    if (alumno != null)
                    {

                        TablaAlumnos tablaAlumno = new TablaAlumnos
                        {
                            Id = alumno.Id_Alumno,
                            Nombre1 = alumno.Primer_Nombre,
                            Nombre2 = alumno.Segundo_Nombre,
                            Apellido1 = alumno.Primer_Apellido,
                            Apellido2 = alumno.Segundo_Apellido,
                            Identificacion = (decimal)alumno.Identificacion,
                            Telefono = (decimal)alumno.Telefono,
                            Usuario = alumno.Usuarios.Nombre_Usuario
                        };
                        lst.Add(tablaAlumno);
                    }
                }
            }
            return View(lst);
        }
    }
}