//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Notas
    {
        public int Id_Nota { get; set; }
        public Nullable<int> Id_Alumno { get; set; }
        public Nullable<int> Id_Materia { get; set; }
        public Nullable<decimal> Nota { get; set; }
    
        public virtual Alumnos Alumnos { get; set; }
        public virtual Materias Materias { get; set; }
    }
}
