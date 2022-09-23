using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tiende_De_Video_Juegos.Models;

namespace Tiende_De_Video_Juegos.Controllers
{
    public class prestamoesController : Controller
    {
        private TiendaDeVideoJuegosEntities db = new TiendaDeVideoJuegosEntities();

        // GET: prestamoes
        public ActionResult Index()
        {
            var prestamo = db.prestamo.Include(p => p.usuario).Include(p => p.videoJuego);
            return View(prestamo.ToList());
        }

        // GET: prestamoes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prestamo prestamo = db.prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // GET: prestamoes/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioCedula = new SelectList(db.usuario.ToList(), "UsuarioCedula", "UsuarioNombre");
            ViewBag.VideoJuegoCodigo = new SelectList(db.videoJuego.ToList(), "VideoJuegoCodigo", "VideoJuegoNombre");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPrestamo,UsuarioCedula,VideoJuegoCodigo,VideoJuegoCodigoEjemplar,fecha")] prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                videoJuego _videojuego = db.videoJuego.ToList().Find(x => x.VideoJuegoCodigo == prestamo.VideoJuegoCodigo);
                if (_videojuego != null)
                {
                    prestamo.VideoJuegoCodigoEjemplar = _videojuego.VideoJuegoCodigoEjemplar;
                    db.prestamo.Add(prestamo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.UsuarioCedula = new SelectList(db.usuario.ToList(), "UsuarioCedula", "UsuarioNombre", prestamo.UsuarioCedula);
            ViewBag.VideoJuegoCodigo = new SelectList(db.videoJuego.ToList(), "VideoJuegoCodigo", "VideoJuegoNombre", prestamo.VideoJuegoCodigo);
            return View(prestamo);
        }

        // GET: prestamoes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prestamo prestamo = db.prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioCedula = new SelectList(db.usuario.ToList(), "UsuarioCedula", "UsuarioNombre", prestamo.UsuarioCedula);
            ViewBag.VideoJuegoCodigo = new SelectList(db.videoJuego.ToList(), "VideoJuegoCodigo", "VideoJuegoNombre", prestamo.VideoJuegoCodigo);
            return View(prestamo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPrestamo,UsuarioCedula,VideoJuegoCodigo,fecha")] prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                videoJuego _videojuego = db.videoJuego.ToList().Find(x => x.VideoJuegoCodigo == prestamo.VideoJuegoCodigo);
                if (_videojuego != null)
                {
                    prestamo.VideoJuegoCodigoEjemplar = _videojuego.VideoJuegoCodigoEjemplar;
                    db.Entry(prestamo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.UsuarioCedula = new SelectList(db.usuario.ToList(), "UsuarioCedula", "UsuarioNombre", prestamo.UsuarioCedula);
            ViewBag.VideoJuegoCodigo = new SelectList(db.videoJuego.ToList(), "VideoJuegoCodigo", "VideoJuegoNombre", prestamo.VideoJuegoCodigo);
            return View(prestamo);
        }

        // GET: prestamoes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prestamo prestamo = db.prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // POST: prestamoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            prestamo prestamo = db.prestamo.Find(id);
            db.prestamo.Remove(prestamo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
