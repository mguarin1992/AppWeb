using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AppWeb.Models.Modelos
{
    public class RegistroCurso
    {
        [Required]
        public Cursos Cursos { get; set; }

    }
    public class EditarCurso
    {
        public Cursos Cursos { get; set; }
    }   
}