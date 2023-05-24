using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb.Models.Datos
{
    public class cursos
    {
        public void Guardar(Cursos modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                db.Cursos.Add(modelo);
                db.SaveChanges();
            }
        }
        public List<Cursos> Consultar()
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                return db.Cursos.AsNoTracking().ToList();
            }
        }
    }
}