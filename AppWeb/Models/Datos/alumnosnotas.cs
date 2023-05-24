using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb.Models.Datos
{
    public class alumnosnotas
    {
        public void Guardar(Notas modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                db.Notas.Add(modelo);
                db.SaveChanges();
            }
        }
    }
}