using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AppWeb.Models.Modelos
{
    public class RegistroDocente
    {
        [Required]
        public Docentes Docentes { get; set; }
        [Required]
        public IEnumerable<SelectListItem> ListadoUsuarios { get; set; }


    }
    public class EditarDocente
    {
        
        public Docentes Docentes { get; set; }
        [Required]
        public List<SelectListItem> ListadoUsuarios { get; set; }


    }   
}