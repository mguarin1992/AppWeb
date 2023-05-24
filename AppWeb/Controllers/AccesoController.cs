using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AppWeb.Models;

namespace AppWeb.Controllers
{
    public class AccesoController : Controller
    {
        
        // GET: Acceso
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult EnviarContraseña(string user, string email)
        {
            try
            {
                using (ColegioEntities db = new ColegioEntities())
                {
                    // Verificar si el usuario existe en la base de datos
                    var usuario = db.Usuarios.FirstOrDefault(u => u.Nombre_Usuario == user);

                    if (usuario != null)
                    {
                        // Generar una nueva contraseña
                        string newPassword = Membership.GeneratePassword(12, 2);

                        // Actualizar la contraseña del usuario en la base de datos
                        usuario.Contraseña_Usuario = Hash(newPassword);
                        db.SaveChanges();

                        // Enviar la nueva contraseña al correo electrónico proporcionado por el usuario
                        string subject = "Nueva contraseña de acceso";
                        string body = "Hola " + user + ",\nSu nueva contraseña es: " + newPassword;
                        EnviarCorreo(email, subject, body);
                        TempData["SuccessMessage"] = "Correo enviado con éxito";
                        return RedirectToAction("Index", "Acceso");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "El nombre de usuario no existe.";
                        return RedirectToAction("RecuperarClave", "Acceso");
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = "El campo correo es obligatorio.";
                return RedirectToAction("RecuperarClave", "Acceso");
            }
            
        }

        private void EnviarCorreo(string destinatario, string asunto, string cuerpo)
        {
            // Configurar la conexión SMTP
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.office365.com"; // Especificar el servidor SMTP que utilizará
            smtp.Port = 587; // Especificar el puerto SMTP que utilizará
            smtp.EnableSsl = true; // Habilitar SSL/TLS para cifrado de la conexión
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("miguel.legion@hotmail.com", "jgmsufzejhbbphsz"); // Especificar las credenciales de autenticación del remitente
           

            // Configurar el mensaje de correo electrónico
            MailMessage mensaje = new MailMessage();
            mensaje.From = new MailAddress("miguel.legion@hotmail.com"); // Especificar la dirección de correo electrónico del remitente
            mensaje.To.Add(new MailAddress(destinatario)); // Especificar la dirección de correo electrónico del destinatario
            mensaje.Subject = asunto; // Especificar el asunto del mensaje
            mensaje.Body = cuerpo; // Especificar el cuerpo del mensaje

            // Enviar el mensaje de correo electrónico
            smtp.Send(mensaje);
        }
        public ActionResult RecuperarClave()
        {

            return View();
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

        public ActionResult Enter(string user, string password)
        {
            try
            {
                using(ColegioEntities db= new ColegioEntities())
                {
                    string hashedPassword = Hash(password);
                    var lst = from d in db.Usuarios
                              where d.Nombre_Usuario == user && d.Contraseña_Usuario == hashedPassword
                              select d;
                       if(lst.Count()>0)
                        {
                            Usuarios oUser = lst.First();
                            Session["User"] = oUser;
                            return Content("1");
                        
                        }
                        else
                        {
                            return Content("Error de usuario o contraseña");
                        }

                }
                
            }
            
            catch (Exception ex)
            {
                return Content("Error de usuario o contraseña" + ex.Message);
            }
        }
        public ActionResult Logout()
        {
            // Elimina la sesión actual del usuario
            Session.Clear();
            Session.Abandon();

            // Redirige al usuario a la página de inicio de sesión
            return RedirectToAction("Index", "Acceso");
        }
    }

}