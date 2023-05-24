using AppWeb.filtro;
using AppWeb.Models;
using AppWeb.Models.Datos;
using AppWeb.Models.Modelos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AppWeb.Controllers
{
    //[Autorizacion_rol(modulo: "Usuarios", Autorizacion_rol.Roles.admin)]
    public class ListadoUsuariosController : Controller
    {
        private IEnumerable<SelectListItem> Roles;
        // GET: ListadoUsuarios
        public ActionResult Index()
        {
            agregar();
            RegistroUsuario modelo = new RegistroUsuario()
            {
                ListadoRoles = Roles
            };
            return View(modelo);
        }
        public ActionResult agregar()
        {
            Roles = new roles().Consultar().ToList().Select(
             p => new SelectListItem { Value = p.Id_rol.ToString(), Text = p.nombre_rol });
            return View();
        }
        public ActionResult Nuevo(RegistroUsuario modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                if (string.IsNullOrEmpty(modelo.Usuarios.Nombre_Usuario))
                {
                    TempData["Message"] = "Debe ingresar Nombre de Usuario";
                    return RedirectToAction("Index");
                }
                if (string.IsNullOrEmpty(modelo.Usuarios.Contraseña_Usuario))
                {
                    TempData["Message"] = "Debe ingresar una contraseña";
                    return RedirectToAction("Index");
                }
                // Verificar si ya existe un usuario con el mismo nombre
                if (db.Usuarios.Any(u => u.Nombre_Usuario == modelo.Usuarios.Nombre_Usuario))
                {
                    TempData["Message"] = "El nombre de usuario ya existe.";
                 return RedirectToAction("Index");
                }
                if (!IsPasswordValid(modelo.Usuarios.Contraseña_Usuario))
                {
                    TempData["Message"] = "La contraseña debe ser alfanumérica y tener al menos 8 caracteres.";
                    return RedirectToAction("Index");
                }
                else
                {

                    Usuarios modeloV = new Usuarios()
                    {
                        Nombre_Usuario = modelo.Usuarios.Nombre_Usuario,
                        Contraseña_Usuario = Hash(modelo.Usuarios.Contraseña_Usuario),
                        Id_rol = modelo.Usuarios.Id_rol
                    };
                    new usuarios().Guardar(modeloV);
                    agregar();
                    modelo = new RegistroUsuario()
                    {
                        ListadoRoles = Roles
                    };
                    return RedirectToAction("Index", "Usuarios");
                }
            }
            // Si la validación falla, volver a cargar la vista con los errores
            modelo.ListadoRoles = Roles;
            return RedirectToAction("Index", modelo);
        }

        public static bool IsPasswordValid(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            if (password.Length < 8)
                return false;

            bool hasLetter = false;
            bool hasDigit = false;

            foreach (char c in password)
            {
                if (char.IsLetter(c))
                    hasLetter = true;
                else if (char.IsDigit(c))
                    hasDigit = true;

                if (hasLetter && hasDigit)
                    return true;
            }

            return false;
        }
        public static string Hash(string input)
        {

            using (SHA256 sha256 = SHA256Managed.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        public ActionResult Editar(int id)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                var usuario = db.Usuarios.Find(id);

                if (usuario == null)
                {
                    return HttpNotFound();
                }

                var modelo = new EditarUsuario
                {
                    Usuarios = usuario,
                    ListadoRoles = db.Rol.Select(r => new SelectListItem
                    {
                        Value = r.Id_rol.ToString(),
                        Text = r.nombre_rol
                    }).ToList()
                };

                return View(modelo);
            }
        }
       

        [HttpPost]
        public ActionResult Editar(EditarUsuario modelo)
        {
            using (ColegioEntities db = new ColegioEntities())
            {
                if (string.IsNullOrEmpty(modelo.Usuarios.Nombre_Usuario))
                {
                    TempData["Message"] = "Debe ingresar Nombre de Usuario";
                    return RedirectToAction("Index");
                }
                // Verificar si ya existe un usuario con el mismo nombre
                if (db.Usuarios.Any(u => u.Nombre_Usuario == modelo.Usuarios.Nombre_Usuario && u.Id_Usuario != modelo.Usuarios.Id_Usuario))
                {
                    TempData["Message"] = "El nombre de usuario ya existe.";
                    return RedirectToAction("Index");
                }
                else
                {
                    Usuarios usuario = db.Usuarios.Find(modelo.Usuarios.Id_Usuario);
                    usuario.Nombre_Usuario = modelo.Usuarios.Nombre_Usuario;
                    usuario.Id_rol = modelo.Usuarios.Id_rol;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Usuarios");
                }
            }
        }

        public ActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                // Manejar el caso cuando el valor del parámetro 'id' es null
                return RedirectToAction("Index", "Usuarios");
            }
            using (ColegioEntities db = new ColegioEntities())
            {
                var usuario = db.Usuarios.Find(id);

                if (usuario == null)
                {
                    return HttpNotFound();
                }

                db.Usuarios.Remove(usuario);
                db.SaveChanges();

                return RedirectToAction("Index", "Usuarios");
            }
        }

        public ActionResult CambiarContraseña()
        {
            Usuarios usuario = (Usuarios)Session["User"];

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var modelo = new CambiarContraseña
            {
                Id = usuario.Id_Usuario

            };

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarContraseña(CambiarContraseña model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (ColegioEntities db = new ColegioEntities())
            {
                var usuario = db.Usuarios.Find(model.Id);

                if (usuario == null)
                {
                    return HttpNotFound();
                }
                if (string.IsNullOrEmpty(model.ContraseñaActual))
                {
                    ModelState.AddModelError("ContraseñaActual", "Debe ingresar la contraseña actual.");
                    return View(model);
                }

                if (usuario.Contraseña_Usuario != Hash( model.ContraseñaActual))
                {
                    ModelState.AddModelError("ContraseñaActual", "La contraseña actual es incorrecta.");
                    return View(model);
                }
                if (string.IsNullOrEmpty(model.NuevaContraseña))
                {
                    ModelState.AddModelError("NuevaContraseña", "Debe ingresar una nueva contraseña.");
                    return View(model);
                }
                if (!IsPasswordValid(model.NuevaContraseña))
                {
                    ModelState.AddModelError("NuevaContraseña", "La nueva contraseña debe ser alfanumérica y tener al menos 8 caracteres.");
                    return View(model);
                }

                if (model.NuevaContraseña != model.ConfirmarNuevaContraseña)
                {
                    ModelState.AddModelError("ConfirmarNuevaContraseña", "La confirmación de la nueva contraseña no coincide.");
                    return View(model);
                }

                if (model.NuevaContraseña == model.ContraseñaActual)
                {
                    ModelState.AddModelError("NuevaContraseña", "La nueva contraseña debe ser diferente de la contraseña actual.");
                    return View(model);
                }

                usuario.Contraseña_Usuario = Hash(model.NuevaContraseña);
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Mensaje"] = "La contraseña se ha cambiado correctamente.";

                return RedirectToAction("Index", "Home");
            }
        }

        
    }


}