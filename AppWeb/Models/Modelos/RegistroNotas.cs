using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AppWeb.Models.Modelos
{
    public class RegistroNotas
    {
        [Required]
        public Notas Notas { get; set; }
        [Required]
        public IEnumerable<SelectListItem> ListadoAlumnos { get; set; }
        [Required]
        public IEnumerable<SelectListItem> ListadoMaterias { get; set; }


    }
    public class EditarNotas
    {
        
        public Notas Notas { get; set; }
        [Required]
        public List<SelectListItem> ListadoAlumnos { get; set; }
        [Required]
        public List<SelectListItem> ListadoMaterias { get; set; }


    }   
}