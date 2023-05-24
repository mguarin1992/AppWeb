using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AppWeb.Models.Modelos
{
    public class RegistroMaterias
    {
        [Required]
        public Materias Materias { get; set; }

    }
    public class EditarMaterias
    {
        
        public Materias Materias { get; set; }

    }   
}