using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ev_4_AE.Models;
using System.Data.Entity;

namespace Ev_4_AE.Controllers
{
    public class EquipoController : Controller
    {
        // GET: Equipo
        private readonly eva4_db db = new eva4_db();

        [HttpGet]
        public ActionResult Listar()
        {
            var equipos = db.Equipo.ToList();
            return View(equipos);
        }

        [HttpGet]
        [ActionName("Detalle")]
        public PartialViewResult Detalle(int? id)
        {

            var modelo = db.Equipo.Include(eq=>eq.Cliente).Where(eq=>eq.id==id).FirstOrDefault();

            return PartialView("_Detalles",modelo);
        }

        [HttpGet]
        [ActionName("Eliminar")]
        public PartialViewResult Eliminar(int? id)
        {

            var modelo = db.Equipo.Include(eq => eq.Cliente).Where(eq => eq.id == id).FirstOrDefault();
            return PartialView("_Eliminar",modelo);


        }


        [HttpPost]
        [ActionName("DeleteEquipo")]
        public JsonResult DeleteEquipo(int? id)
        {

           
            var equipo = db.Equipo.Find(id);

            db.Equipo.Remove(equipo);

            db.SaveChanges();

            return Json(equipo,JsonRequestBehavior.AllowGet);






        }

        [HttpGet]
        [ActionName("AgregarForms")]
        public PartialViewResult AgregarForms()
        {
            var clientes = db.Cliente.ToList();
            ViewBag.clienteId = new SelectList(clientes, "id", "nombre");
            return PartialView("_formularios");
        }

        [HttpPost]
        [ActionName("GuardarEquipo")]
        public JsonResult GuardarEquipo(Equipo eq)
        {
            int res = 0;
            eq.rutaImagen = "./../images/1.jpg";
            eq.fechaRecepcion = DateTime.Now;
            db.Equipo.Add(eq);
            res = db.SaveChanges();
            return Json(res, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        [ActionName("EditarForms")]
        public PartialViewResult EditarForms(int? id)
        {
            var equipo = db.Equipo.Include(eq => eq.Cliente).Where(eq => eq.id == id).FirstOrDefault();
            ViewBag.clienteId = new SelectList(db.Cliente.ToList(), "id", "nombre",equipo.clienteId);
            return PartialView("_formularios",equipo);
        }

        [HttpPost]
        [ActionName("EditarEquipo")]
        public JsonResult EditarEquipo(Equipo eq)
        {
            int res = 0;
            db.Entry(eq).State = EntityState.Modified;
            res = db.SaveChanges();
            return Json(res, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        [ActionName("uploadArchivo")]
        public PartialViewResult uploadArchivo(int? id)
        {
            Session["idEquipo"] = id;
            return PartialView("_uploadArchivo");

        }

        [HttpPost]
        [ActionName("subirArchivo")]
        public JsonResult subirArchivo(HttpPostedFileBase archivo)
        {
            try
            {

                if (archivo != null)
                {
                    string path = Server.MapPath("~/images/");
                    if (System.IO.Directory.Exists(path) == false)
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }

                    Random r = new Random();

                    string file = (r.Next()) + archivo.FileName;
                    
                    archivo.SaveAs(path + System.IO.Path.GetFileName(file));

                    
                    int idEquipo = (int)Session["idEquipo"];
                    var equipo = db.Equipo.Find(idEquipo);
                    equipo.rutaImagen = "./../images/" + file;
                    db.Entry(equipo).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {

                    return Json(new { mensaje = "Error Imagen no guardada" }, JsonRequestBehavior.AllowGet);

                }

            }
            catch
            {



            }

            return Json(new { mensaje = "Error Imagen no guardada" }, JsonRequestBehavior.AllowGet);
        }




    }
}