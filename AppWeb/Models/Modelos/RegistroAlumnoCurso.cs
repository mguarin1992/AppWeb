using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AppWeb.Models.Modelos
{
    public class RegistroAlumnoCurso
    {
        [Required]
        public AlumCur AlumCur { get; set; }
        [Required]
        public IEnumerable<SelectListItem> ListadoAlumnos { get; set; }
        [Required]
        public IEnumerable<SelectListItem> ListadoCurso { get; set; }


    }
    public class EditarAlumnoCurso
    {
        
        public AlumCur AlumCur { get; set; }
        [Required]
        public List<SelectListItem> ListadoAlumnos { get; set; }
        [Required]
        public List<SelectListItem> ListadoMaterias { get; set; }


    }   
}