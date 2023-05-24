using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb.Models.Datos
{
    public class materias
    {
        public void Guardar(Materias modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                db.Materias.Add(modelo);
                db.SaveChanges();
            }
        }
        public List<Materias> Consultar()
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                return db.Materias.AsNoTracking().ToList();
            }
        }
    }
}