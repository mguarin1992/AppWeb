using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb.Models.Datos
{
    public class alumnos
    {
        public void Guardar(Alumnos modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                db.Alumnos.Add(modelo);
                db.SaveChanges();
            }
        }
        public List<Alumnos> Consultar()
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                return db.Alumnos.AsNoTracking().ToList();
            }
        }
    }
}