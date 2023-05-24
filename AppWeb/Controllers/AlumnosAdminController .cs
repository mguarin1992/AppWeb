using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AppWeb.Models;
using AppWeb.Models.Tablas;
using System.IO;
using ClosedXML.Excel;

namespace AppWeb.Controllers
{
    public class AlumnosAdminController : Controller
    {
        public object ExcelFillStyle { get; private set; }

        // GET: Alumnos
        public ActionResult Index()
        {
            List<TablaAlumnos> lst = null;
            using (ColegioEntities db = new ColegioEntities())
            {
                           lst = (from d in db.Alumnos
                           select new TablaAlumnos
                           {
                               Id = d.Id_Alumno,
                               Nombre1 = d.Primer_Nombre,
                               Nombre2 = d.Segundo_Nombre,
                               Apellido1 = d.Primer_Apellido,
                               Apellido2 = d.Segundo_Apellido,
                               Identificacion = (decimal)d.Identificacion,
                               Telefono = (decimal)d.Telefono,
                               Usuario = d.Usuarios.Nombre_Usuario
                           }).ToList();
                
            }
            return View(lst);
        }


        public ActionResult Reporte(string busqueda, string descargar)
        {
            List<TablaAlumnos> lst = null;
            using (ColegioEntities db = new ColegioEntities())
            {
                lst = (from ac in db.AlumCur
                       join a in db.Alumnos on ac.Id_Alumno equals a.Id_Alumno
                       join c in db.Cursos on ac.Id_Curso equals c.Id_Curso
                       join mc in db.MatCur on ac.Id_Curso equals mc.Id_Curso
                       join m in db.Materias on mc.Id_Materia equals m.Id_Materia
                       where a.Primer_Apellido.ToLower().Contains(busqueda.ToLower().Trim()) ||
                         a.Identificacion.ToString().Contains(busqueda.Trim())
                       select new TablaAlumnos
                       {
                           Id = a.Id_Alumno,
                           Nombre1 = a.Primer_Nombre,
                           Nombre2 = a.Segundo_Nombre,
                           Apellido1 = a.Primer_Apellido,
                           Apellido2 = a.Segundo_Apellido,
                           Identificacion = (decimal)a.Identificacion,
                           Telefono = (decimal)a.Telefono,
                           Usuario = a.Usuarios.Nombre_Usuario,
                           Curso = c.Nombre_Curso,
                           Materia = m.Nombre_Materia,

                       }).ToList();
            }
            if (descargar =="true")
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Reporte");

                    // Encabezados de columna
                    worksheet.Cell(1, 1).Value = "Id";
                    worksheet.Cell(1, 2).Value = "Primer Nombre";
                    worksheet.Cell(1, 3).Value = "Segundo Nombre";
                    worksheet.Cell(1, 4).Value = "Primer Apellido";
                    worksheet.Cell(1, 5).Value = "Segundo Apellido";
                    worksheet.Cell(1, 6).Value = "Identificación";
                    worksheet.Cell(1, 7).Value = "Teléfono";
                    worksheet.Cell(1, 8).Value = "Usuario";
                    worksheet.Cell(1, 9).Value = "Curso";
                    worksheet.Cell(1, 10).Value = "Materia";

                    // Datos de los alumnos
                    for (int i = 0; i < lst.Count; i++)
                    {
                        var alumno = lst[i];
                        worksheet.Cell(i + 2, 1).Value = alumno.Id;
                        worksheet.Cell(i + 2, 2).Value = alumno.Nombre1;
                        worksheet.Cell(i + 2, 3).Value = alumno.Nombre2;
                        worksheet.Cell(i + 2, 4).Value = alumno.Apellido1;
                        worksheet.Cell(i + 2, 5).Value = alumno.Apellido2;
                        worksheet.Cell(i + 2, 6).Value = alumno.Identificacion;
                        worksheet.Cell(i + 2, 7).Value = alumno.Telefono;
                        worksheet.Cell(i + 2, 8).Value = alumno.Usuario;
                        worksheet.Cell(i + 2, 9).Value = alumno.Curso;
                        worksheet.Cell(i + 2, 10).Value = alumno.Materia;
                    }

                    // Ajustar el ancho de las columnas automáticamente
                    worksheet.Columns().AdjustToContents();

                    // Convertir el libro de trabajo a un arreglo de bytes
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();

                        // Devolver el archivo Excel como una descarga
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte.xlsx");
                    }
                }
            }
            else
            {
                // Resto del código para mostrar la vista con los resultados de la búsqueda
                return View(lst);
            }
        }
    }
}