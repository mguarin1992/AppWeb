using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web;

namespace AppWeb.Models.Datos
{
    public class usuarios
    {
        public void Guardar(Usuarios modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                db.Usuarios.Add(modelo);
                db.SaveChanges();
            }
        }
        public List<Usuarios> Consultar()
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                return db.Usuarios.AsNoTracking().ToList();
            }
        }
    }

}