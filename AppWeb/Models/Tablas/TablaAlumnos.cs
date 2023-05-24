using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb.Models.Tablas
{
    public class TablaAlumnos
    {
        public int IdCurso { get; set; }
        public int Id { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public decimal Identificacion { get; set; }
        public decimal Telefono { get; set; }
        public string Usuario { get; set; }
        public string Curso { get; internal set; }
        public string Materia { get; internal set; }
        
    }
}