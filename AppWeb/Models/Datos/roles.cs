using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb.Models.Modelos
{
    public class roles
    {
        public List<Rol>Consultar()
        {
            using(ColegioEntities db=new  ColegioEntities())
            {
                return db.Rol.AsNoTracking().ToList();
            }
        }
    }
}