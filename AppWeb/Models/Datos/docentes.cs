using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb.Models.Datos
{
    public class docentes
    {
        public void Guardar(Docentes modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                db.Docentes.Add(modelo);
                db.SaveChanges();
            }
        }
    }
}