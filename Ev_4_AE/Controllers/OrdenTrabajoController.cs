﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ev_4_AE.Models;
using System.Data.Entity;

namespace Ev_4_AE.Controllers
{
    public class OrdenTrabajoController : Controller
    {
        private readonly eva4_db db = new eva4_db();

        [HttpGet]
        public ActionResult Listar()
        {
            var equipos = db.OrdenTrabajo.ToList();
            return View(equipos);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            var equipos = db.OrdenTrabajo.ToList();
            ViewBag.EquipoId = new SelectList(equipos,"id","descripcion");
            return View();
        }

        
        [HttpGet]
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
    }
}