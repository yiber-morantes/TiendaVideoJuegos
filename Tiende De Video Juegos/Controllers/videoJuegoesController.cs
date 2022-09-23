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
    public class videoJuegoesController : Controller
    {
        private TiendaDeVideoJuegosEntities db = new TiendaDeVideoJuegosEntities();

        // GET: videoJuegoes
        public ActionResult Index()
        {
            return View(db.videoJuego.ToList());
        }

        // GET: videoJuegoes/Details/5
        public ActionResult Details(long? VideoJuegoCodigo, long? videoJuegoCodigoEjemplar)
        {
            if (VideoJuegoCodigo == null && videoJuegoCodigoEjemplar == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            videoJuego videoJuego = db.videoJuego.Find(VideoJuegoCodigo, videoJuegoCodigoEjemplar);
            if (videoJuego == null)
            {
                return HttpNotFound();
            }
            return View(videoJuego);
        }

        // GET: videoJuegoes/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VideoJuegoNombre,VideoJuegoCodigo,VideoJuegoCodigoEjemplar")] videoJuego videoJuego)
        {
            if (ModelState.IsValid)
            {
                db.videoJuego.Add(videoJuego);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(videoJuego);
        }

        // GET: videoJuegoes/Edit/5
        public ActionResult Edit(long? VideoJuegoCodigo,long? videoJuegoCodigoEjemplar )
        {
            if (VideoJuegoCodigo == null && videoJuegoCodigoEjemplar==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            videoJuego videoJuego = db.videoJuego.Find(VideoJuegoCodigo, videoJuegoCodigoEjemplar);
            if (videoJuego == null)
            {
                return HttpNotFound();
            }
            return View(videoJuego);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VideoJuegoNombre,VideoJuegoCodigo,VideoJuegoCodigoEjemplar")] videoJuego videoJuego)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videoJuego).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(videoJuego);
        }

        // GET: videoJuegoes/Delete/5
        public ActionResult Delete(long? VideoJuegoCodigo, long? videoJuegoCodigoEjemplar)
        {
            if (VideoJuegoCodigo == null && videoJuegoCodigoEjemplar == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            videoJuego videoJuego = db.videoJuego.Find(VideoJuegoCodigo, videoJuegoCodigoEjemplar);
            if (videoJuego == null)
            {
                return HttpNotFound();
            }
            return View(videoJuego);
        }

        // POST: videoJuegoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long? VideoJuegoCodigo, long? videoJuegoCodigoEjemplar)
        {
            videoJuego videoJuego = db.videoJuego.Find(VideoJuegoCodigo,videoJuegoCodigoEjemplar);
            db.videoJuego.Remove(videoJuego);
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
