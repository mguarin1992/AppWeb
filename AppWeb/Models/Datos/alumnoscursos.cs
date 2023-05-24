using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb.Models.Datos
{
    public class alumnoscursos
    {
        public void Guardar(AlumCur modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                db.AlumCur.Add(modelo);
                db.SaveChanges();
            }
        }
    }
}