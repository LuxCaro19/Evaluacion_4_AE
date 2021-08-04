using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ev_4_AE.Models;
using System.Data.Entity;
using Ev_4_AE.Filters;

namespace Ev_4_AE.Controllers
{
    public class AvancesController : Controller
    {
        private readonly eva4_db db = new eva4_db();
        [HttpGet]
        public ActionResult Listar(int? id)
        {
            Session["OrdenTrabajoId"] = id;
            var avanceEnOt = from av in db.AvanceOrdenTrabajo where av.ordenTrabajoId == id select av;
            return View(avanceEnOt.ToList());
        }
 
        public ActionResult Eliminar(int? id) {
            var avance = db.AvanceOrdenTrabajo.Find(id);
            return View(avance);
        }
        [HttpPost]
        public ActionResult Eliminar(AvanceOrdenTrabajo ava)
        {
           

                var ava_delete = db.AvanceOrdenTrabajo.Find(ava.id);
                var otid = ava.ordenTrabajoId; 
                db.AvanceOrdenTrabajo.Remove(ava_delete);

                int num = db.SaveChanges();

                if (num > 0)
                {
                    TempData["mensaje"] = "Avance eliminado";
                    return RedirectToAction("Listar", "Avances", new { id = otid });
                }
                else
                {
                TempData["mensaje"] = "No se ha podido eliminar el avance";
                    return RedirectToAction("Listar", "Avances", new { id = otid });
                }
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(AvanceOrdenTrabajo av)
        {
            
            if (ModelState.IsValid)
            {
                av.ordenTrabajoId = Convert.ToInt32(Session["OrdenTrabajoId"]);
                db.AvanceOrdenTrabajo.Add(av);
                db.SaveChanges();
                TempData["mensaje"] = "se ha creado el avance";
                return RedirectToAction("Listar", "Avances", new { id = av.ordenTrabajoId });
            }

            return View(av);
        }
        
        [HttpGet]
        [AuthorizeUser(idOperacion: 7)]
        public ActionResult Editar(int? id)
        {
            var av = db.AvanceOrdenTrabajo.Find(id);
            if (av != null)
            {
                return View(av);
            }
            return RedirectToAction("Listar", "Avances", new { id = Convert.ToInt32(Session["OrdenTrabajoId"]) });
        }
        [HttpPost]
        public ActionResult Editar(AvanceOrdenTrabajo avan)
        {
            if (ModelState.IsValid)
            {
                avan.ordenTrabajoId = Convert.ToInt32(Session["OrdenTrabajoId"]);
                db.Entry(avan).State = EntityState.Modified;
                db.SaveChanges();

                TempData["mensaje"] = "avance de trabajo modificada";
                return RedirectToAction("Listar", "Avances", new { id = avan.ordenTrabajoId});
            }
            return View(avan);
        }
    }
}