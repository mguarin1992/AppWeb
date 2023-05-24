using AppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AppWeb.filtro
{

    public class Autorizacion_rol : AuthorizeAttribute
    {
        public string modulo { get; set; }
        public enum Roles
        {
            admin = 1,
            profesor = 2,
            estudiante = 3
        }
        private List<Roles> _roles;

        /*public Autorizacion_rol(string modulo, params Roles[] roles)
        {
            this.modulo = modulo;
            _roles = roles.ToList();
        }*/

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Verificar si el usuario está autenticado
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            // Verificar si el rol del usuario coincide con alguno de los roles permitidos
            var usuario = (Usuarios)httpContext.Session["User"];
            if (usuario != null && _roles.Contains((Roles)usuario.Id_rol))
            {
                return true;
            }
            return false;
        }

        public static bool IsAuthorized(string modulo, Usuarios usuario)
        {
            // permisos usuario admin
            if (usuario != null && modulo == "Notas" && usuario.Id_rol == 1)
                return false;
            if (usuario != null && modulo == "RegisMate" && usuario.Id_rol == 1)
                return false;
            if (usuario != null && modulo == "MisNotas" && usuario.Id_rol == 1)
                return false;
            if (usuario != null && usuario.Id_rol == 1)
                return true;
           
            // Permisos usuario Profesor
            if (usuario != null && modulo == "Cerrar Sesión" && usuario.Id_rol == 2)
                return true;
            if (usuario != null && modulo == "Cursos" && usuario.Id_rol == 2)
                return true;
            if (usuario != null && modulo == "Notas" && usuario.Id_rol == 2)
                return true;
            if (usuario != null && modulo == "NuevoNota" && usuario.Id_rol == 2)
                return true;
            if (usuario != null && modulo == "EditarNota" && usuario.Id_rol == 2)
                return true;
            if (usuario != null && modulo == "Contraseña" && usuario.Id_rol == 2)
                return true;

            //permisos usuario Alumno
            if (usuario != null && modulo == "MisNotas" && usuario.Id_rol == 3)
                return true;
            if (usuario != null && modulo == "Notas" && usuario.Id_rol == 3)
                return true;
            if (usuario != null && modulo == "RegisMate" && usuario.Id_rol == 3)
                return true;
            if (usuario != null && modulo == "Contraseña" && usuario.Id_rol == 3)
                return true;
            if (usuario != null && modulo == "Cerrar Sesión" && usuario.Id_rol == 3)
                return true;


            return false;
        }
    }
}