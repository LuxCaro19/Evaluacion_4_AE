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
    public class OrdenTrabajoController : Controller
    {
        private readonly eva4_db db = new eva4_db();

        [HttpGet]
        public ActionResult Listar()
        {
            var ot = db.OrdenTrabajo.ToList();
            return View(ot);
        }

        [HttpGet]
        [AuthorizeUser(idOperacion: 6)]

        public ActionResult Agregar()
        {
            var equipos = db.Equipo.ToList();
            ViewBag.EquipoId = new SelectList(equipos,"id", "descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(OrdenTrabajo ot)
        {
            if (ModelState.IsValid)
            {
                var usuario = (Usuario)Session["User"];
                ot.usuarioId = usuario.id;
                db.OrdenTrabajo.Add(ot);
                db.SaveChanges();

                TempData["mensaje"] = "se ha creado la orden de trabajo";
                return RedirectToAction("Listar", "OrdenTrabajo");
            }
            var equipos = db.Equipo.ToList();
            ViewBag.EquipoId = new SelectList(equipos, "id", "descripcion", ot.equipoId);
            return View(ot);
        }

        public ActionResult Eliminar(int? id)
        {
            var ord = db.OrdenTrabajo.Find(id);
            return View(ord);

        }


        [HttpPost]
        public ActionResult Eliminar(OrdenTrabajo or)
        {
            string msg = "";
                try
                {

                var ord_delete = db.OrdenTrabajo.Find(or.id);
                msg = ord_delete.tipo + " " + ord_delete.descripcion + " " + ord_delete.fecha.ToShortDateString();
                db.OrdenTrabajo.Remove(ord_delete);

                int num = db.SaveChanges();

                if (num>0)
                {
                    TempData["mensaje"] = "Orden de trabajo eliminada";
                    return RedirectToAction("Listar","OrdenTrabajo");
                }
                else
                {
                    TempData["mensaje"] = "Orden de trabajo No eliminada, Orden: "+msg;
                    return RedirectToAction("Listar", "OrdenTrabajo");
                }

                }
                    catch (Exception)
                    {

                TempData["mensaje"] = "Orden de trabajo No eliminada (Tiene detalle asociado), Orden: " + msg;
                return RedirectToAction("Listar", "OrdenTrabajo");

                }



        }
        [HttpGet]
        [AuthorizeUser(idOperacion: 7)]
        public ActionResult Editar(int? id)
        {
            var ot = db.OrdenTrabajo.Find(id);
            if (ot != null)
            {
                var equipos = db.Equipo.ToList();
                ViewBag.EquipoId = new SelectList(equipos, "id", "descripcion", ot.equipoId);
                return View(ot);
            }
            return RedirectToAction("Listar", "OrdenTrabajo");
        }
        [HttpPost]
        public ActionResult Editar(OrdenTrabajo orden) {
            if (ModelState.IsValid) {
                var usuario = (Usuario)Session["User"];
                orden.usuarioId = usuario.id;
                db.Entry(orden).State = EntityState.Modified;
                db.SaveChanges();

                TempData["mensaje"] = "orden de trabajo modificada";
                return RedirectToAction("Listar", "OrdenTrabajo");
            }
            var equipos = db.Equipo.ToList();
            ViewBag.EquipoId = new SelectList(equipos, "id", "descripcion", orden.equipoId);
            return View(orden);
        }
    }
}