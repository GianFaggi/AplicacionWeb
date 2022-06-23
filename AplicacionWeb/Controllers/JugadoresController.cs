using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionWeb.Models;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;

namespace AplicacionWeb.Controllers
{
    public class JugadoresController : Controller
    {
        // GET: Jugadores
        public ActionResult Index()
        {
            return View();
        }

        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/


        /*Jugador */
        public ActionResult obj_Jugadores()
        {
            JugadoresCLS j = new JugadoresCLS { id = 2, name = "Leonel", lastName = "Messi", team = "PSG", position = "Delantero" };
            return View(j);
        }

        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/


        /*Lista de Jugadores*/
        public ActionResult Lista_jugadores()
        {

            List<JugadoresCLS> jugadores = new List<JugadoresCLS>();


            JugadoresCLS l1 = new JugadoresCLS { id = 2, name = "Leonel", lastName = "Messi", team = "PSG", position = "Delantero" };
            JugadoresCLS l2 = new JugadoresCLS { id = 3, name = "Martin", lastName = "Palermo", team = "Aldosivi", position = "D.T" };
            JugadoresCLS l3 = new JugadoresCLS { id = 4, name = "Robert", lastName = "Rojas", team = "River Plate", position = "Delantero" };
            JugadoresCLS l4 = new JugadoresCLS { id = 5, name = "Robert", lastName = "Lewandoski", team = "Bayern Munich", position = "Delantero" };

            jugadores.Add(l1);
            jugadores.Add(l2);
            jugadores.Add(l3);
            jugadores.Add(l4);

            return View(jugadores);

        }

        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /*Lista de jugadores en Linq*/
       /* public ActionResult Lista_jugadores_linq2(string searchString)
        {
            List<JugadoresCLS> lista = null;
            using (var db = new PWBDEntities())
            {
                lista = (from Jug in db.Jugadores
                         select new JugadoresCLS
                         {
                             id = Jug.id,
                             name = Jug.name,
                             lastName = Jug.lastName,
                             team = Jug.team,
                             position = Jug.position
                         }).ToList();
            }
            return View(lista);

        }
       */

        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*Agregar jugador por clase*/

        public ActionResult Agregar_Jugador_Por_Clase()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Agregar_Jugador_Por_Clase(JugadoresCLS ojugadoresCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(ojugadoresCLS);
            }
            else
            {
                using (var db = new PWBDEntities())
                {
                    Jugadores ojugador = new Jugadores();
                    ojugador.name = ojugadoresCLS.name;
                    ojugador.lastName = ojugadoresCLS.lastName;
                    ojugador.team = ojugadoresCLS.team;
                    ojugador.position = ojugadoresCLS.position;
                    db.Jugadores.Add(ojugador);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Lista_jugadores_linq");
        }



        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/


        /* Lista de jugadores por BD - ¿? */
        /*
         public ActionResult Lista_jugadores_DB()
         {
             using (var db = new PWBDEntities())
             {



                 return View(db.Jugadores.ToList());
             }
        }
      */

        

        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*Agregar Jugador BD - Priorizamos linq */
        /*
        public ActionResult Agregar_jugador()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar_Jugador(Jugadores j)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new PWBDEntities())
                {
                    db.Jugadores.Add(j);
                    db.SaveChanges();
                    return RedirectToAction("Lista_jugadores_linq");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        */


        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*Detalle Jugador */

        public ActionResult DetalleJugador(int id)
        {
            try
            {
                using (var db = new PWBDEntities())
                {
                    Jugadores Jugador = db.Jugadores.Find(id);
                    return View(Jugador);

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ERROR AL MOSTRAR DETALLES DEL JUGADOR", ex);
                return RedirectToAction("Lista_jugadores_linq");
            }
        }


        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*delete */

        public ActionResult delete(int id)
        {
            try
            {
                using (var db = new PWBDEntities())
                {
                    Jugadores alu = db.Jugadores.Find(id);
                    db.Jugadores.Remove(alu);
                    db.SaveChanges();
                    return RedirectToAction("lista_jugadores_linq");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al Borrar Jugador", ex);
                return RedirectToAction("lista_jugadores_linq");
            }
        }

        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /* Editar Jugador*/

        public ActionResult Editar(int id)
        {
            try
            {
                using (var db = new PWBDEntities())
                {
                    Jugadores jug = db.Jugadores.Find(id);

                    return View(jug);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Editar(Jugadores jug)
        {
            if (!ModelState.IsValid)
            {
                return View(jug);
            }
            try
            {
                using (var db = new PWBDEntities())
                {
                    Jugadores ju = db.Jugadores.Find(jug.id);
                    ju.name = jug.name;
                    ju.lastName = jug.lastName;
                    ju.team = jug.team;
                    ju.position = jug.position;


                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al Editar Jugador- ", ex);
            }
            return View();


        }
        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*datos directos base de datos*/

        public ActionResult Lista_jugadores_linq(JugadoresCLS ojugadoresCLS)
        {
            {
                string apellido_Jugador = ojugadoresCLS.lastName;
                List<JugadoresCLS> lista2 = null;
                using (var db = new PWBDEntities())
                {
                    if (ojugadoresCLS.lastName == null)
                    {
                        lista2 = (from jugadores in db.Jugadores
                            select new JugadoresCLS
                            {
                                id = jugadores.id,
                                lastName = jugadores.lastName,
                                name = jugadores.name,
                                team = jugadores.team,
                                position = jugadores.position,
                                

                            }).ToList();
                        Session["Lista_Jugadores"] = lista2;
                    }
                    else
                    {
                        lista2 = (from jugadores in db.Jugadores
                                 where jugadores.lastName.Contains(apellido_Jugador)
                                 select new JugadoresCLS
                                 {
                                     id = jugadores.id,
                                     lastName = jugadores.lastName,
                                     name = jugadores.name,
                                     team = jugadores.team,
                                     position = jugadores.position,
                                 }).ToList();
                        Session["Lista_Jugadores"] = lista2;
                    }
                }
                return View(lista2);
            }
            
          /*  if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                try
                {
                    using (var db = new PWBDEntities())
                    {
                        List<Jugadores> listaJugadores = null;
                        listaJugadores = db.Jugadores.ToList();
                        Session["listaJugadores"] = listaJugadores;
                        //return View(listaJugadores);
                         return View(db.Jugadores.ToList());
                    }
                }
                catch(Exception)
                {
                    throw;
                }
            }
          */
        }
        
        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*Generacion Pdf*/

        public FileResult generarPDF()
        {
            Document doc = new Document();
            byte[] buffer;
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                Paragraph titulo = new Paragraph("Listado de Jugadores");
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);

                Paragraph espacio = new Paragraph(" ");
                doc.Add(espacio);

                //Columnas(tabla)
                PdfPTable tabla = new PdfPTable(5);
                //Ancho Columnas
                float[] Valores = new float[5] {20,40,40,60,40};
                //Asignamos esos anchos a la tabla
                tabla.SetWidths(Valores);
                //Creamos las celdas(ponemos contenido)- Color y alineado de centro
                PdfPCell celda1 = new PdfPCell(new Phrase("Id Jugador"));
                celda1.BackgroundColor = new BaseColor(130, 130, 130);
                celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda1);

                PdfPCell celda2 = new PdfPCell(new Phrase("Nombre"));
                celda1.BackgroundColor = new BaseColor(130, 130, 130);
                celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda2);

                PdfPCell celda3 = new PdfPCell(new Phrase("Apellido"));
                celda1.BackgroundColor = new BaseColor(130, 130, 130);
                celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda3);

                PdfPCell celda4 = new PdfPCell(new Phrase("Equipo"));
                celda1.BackgroundColor = new BaseColor(130, 130, 130);
                celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda4);

                PdfPCell celda5 = new PdfPCell(new Phrase("Posicion"));
                celda1.BackgroundColor = new BaseColor(130, 130, 130);
                celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda5);

                List<JugadoresCLS> lista2 = (List<JugadoresCLS>)Session["Lista_Jugadores"];
                int nregistros = lista2.Count; 
                for(int i =0; i < nregistros; i++)
                    {
                    tabla.AddCell(lista2[i].id.ToString());
                    tabla.AddCell(lista2[i].name);
                    tabla.AddCell(lista2[i].lastName);
                    tabla.AddCell(lista2[i].team);
                    tabla.AddCell(lista2[i].position);

                }
                doc.Add(tabla);
                doc.Close();
                buffer = ms.ToArray();
            }
            return File(buffer, "application/pdf");
        }
    }
}