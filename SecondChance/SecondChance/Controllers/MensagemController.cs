using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SecondChance.Models
{
    [Authorize]
    public class MensagemsController : Controller
    {
        private SecondChanceDB db = new SecondChanceDB();

        // GET: Mensagem
        public ActionResult Index()
        {
            var mensagens = db.Mensagem.Include(m => m.UtilDestino).Include(m => m.UtilOrigem);
            return View(mensagens.ToList());
        }

        // GET: Mensagem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensagem mensagem = db.Mensagem.Find(id);
            if (mensagem == null)
            {
                return HttpNotFound();
            }
            return View(mensagem);
        }

        // GET: Mensagem/Create
        public ActionResult Create()
        {
            ViewBag.IdUtilDestino = new SelectList(db.Utilizador, "IdUtilizador", "Nome");
            ViewBag.IdUtilOrigem = new SelectList(db.Utilizador, "IdUtilizador", "Nome");
            return View();
        }

        // POST: Mensagem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMensagem,Conteudo,DataHora,IdUtilOrigem,IdUtilDestino")] Mensagem mensagem)
        {
            if (ModelState.IsValid)
            {
                db.Mensagem.Add(mensagem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUtilDestino = new SelectList(db.Utilizador, "IdUtilizador", "Nome", mensagem.IdUtilDestino);
            ViewBag.IdUtilOrigem = new SelectList(db.Utilizador, "IdUtilizador", "Nome", mensagem.IdUtilOrigem);
            return View(mensagem);
        }

        // GET: Mensagem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensagem mensagem = db.Mensagem.Find(id);
            if (mensagem == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUtilDestino = new SelectList(db.Utilizador, "IdUtilizador", "Nome", mensagem.IdUtilDestino);
            ViewBag.IdUtilOrigem = new SelectList(db.Utilizador, "IdUtilizador", "Nome", mensagem.IdUtilOrigem);
            return View(mensagem);
        }

        // POST: Mensagem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMensagem,Conteudo,DataHora,IdUtilOrigem,IdUtilDestino")] Mensagem mensagem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mensagem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUtilDestino = new SelectList(db.Utilizador, "IdUtilizador", "Nome", mensagem.IdUtilDestino);
            ViewBag.IdUtilOrigem = new SelectList(db.Utilizador, "IdUtilizador", "Nome", mensagem.IdUtilOrigem);
            return View(mensagem);
        }

        // GET: Mensagem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensagem mensagem = db.Mensagem.Find(id);
            if (mensagem == null)
            {
                return HttpNotFound();
            }
            return View(mensagem);
        }

        // POST: Mensagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mensagem mensagem = db.Mensagem.Find(id);
            db.Mensagem.Remove(mensagem);
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
