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
    
    public class UsuariosController : Controller
    {
        
        // GET: Usuarios
        public ActionResult Index()
        {
             List<TablaUsuarios> lst = null;
            using(ColegioEntities db = new ColegioEntities())
            {
                lst = (from d in db.Usuarios
                       select new TablaUsuarios
                       {
                           Nombre_Usuario = d.Nombre_Usuario,
                           Id = d.Id_Usuario,
                           Rol=d.Rol.nombre_rol
                       }).ToList();
            }
            return View(lst);
        }
    }
}