using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SecondChance.Models;

namespace SecondChance.Controllers
{
    public class MultimediaController : Controller
    {
        private SecondChanceDB db = new SecondChanceDB();

        // GET: Multimedia
        public ActionResult Index()
        {
            var recMultimedia = db.RecMultimedia.Include(m => m.Artigo);
            return View(recMultimedia.ToList());
        }

        // GET: Multimedia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Multimedia multimedia = db.RecMultimedia.Find(id);
            if (multimedia == null)
            {
                return HttpNotFound();
            }
            return View(multimedia);
        }

        // GET: Multimedia/Create
        public ActionResult Create()
        {
            ViewBag.IdArtigo = new SelectList(db.Artigo, "IdArtigo", "Titulo");
            return View();
        }

        // POST: Multimedia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMultimedia,Designacao,Tipo,IdArtigo")] Multimedia multimedia)
        {
            if (ModelState.IsValid)
            {
                db.RecMultimedia.Add(multimedia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdArtigo = new SelectList(db.Artigo, "IdArtigo", "Titulo", multimedia.IdArtigo);
            return View(multimedia);
        }

        // GET: Multimedia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Multimedia multimedia = db.RecMultimedia.Find(id);
            if (multimedia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdArtigo = new SelectList(db.Artigo, "IdArtigo", "Titulo", multimedia.IdArtigo);
            return View(multimedia);
        }

        // POST: Multimedia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMultimedia,Designacao,Tipo,IdArtigo")] Multimedia multimedia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(multimedia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdArtigo = new SelectList(db.Artigo, "IdArtigo", "Titulo", multimedia.IdArtigo);
            return View(multimedia);
        }

        // GET: Multimedia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Multimedia multimedia = db.RecMultimedia.Find(id);
            if (multimedia == null)
            {
                return HttpNotFound();
            }
            return View(multimedia);
        }

        // POST: Multimedia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Multimedia multimedia = db.RecMultimedia.Find(id);
            db.RecMultimedia.Remove(multimedia);
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
