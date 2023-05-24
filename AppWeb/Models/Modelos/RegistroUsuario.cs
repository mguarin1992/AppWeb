using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AppWeb.Models.Modelos
{
    public class RegistroUsuario
    {
        [Required]
        public Usuarios Usuarios { get; set; }
        [Required]
        public IEnumerable<SelectListItem> ListadoRoles { get; set; }


    }
    public class EditarUsuario
    {
        
        public Usuarios Usuarios { get; set; }
        [Required]
        public List<SelectListItem> ListadoRoles { get; set; }


    }
    public class CambiarContraseña
    {
        public int Id { get; set; }
        public string ContraseñaActual { get; set; }
        public string NuevaContraseña { get; set; }
        public string ConfirmarNuevaContraseña { get; set; }
    }
}