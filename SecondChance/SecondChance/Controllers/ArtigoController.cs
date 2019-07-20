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
    public class ArtigoController : Controller
    {
        private SecondChanceDB db = new SecondChanceDB();

        // GET: Artigo
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.ordenarTitulo = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.ordenarPreco = sortOrder == "preco" ? "preco_desc" : "preco";
            ViewBag.ordenarCategoria = sortOrder == "cat" ? "cat_desc" : "cat";
            ViewBag.ordenarNome = sortOrder == "nome" ? "nome_desc" : "nome";
            var artigos = db.Artigo.Include(a => a.Categoria).Include(a => a.Dono).Include(a => a.Gestor);

            if (!String.IsNullOrEmpty(searchString))
            {
                artigos = artigos.Where(a => a.Titulo.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    artigos = artigos.OrderByDescending(a => a.Titulo);
                    break;
                case "preco":
                    artigos = artigos.OrderBy(a => a.Preco);
                    break;
                case "preco_desc":
                    artigos = artigos.OrderByDescending(a => a.Preco);
                    break;
                case "cat":
                    artigos = artigos.OrderBy(a => a.Categoria.Designacao);
                    break;
                case "cat_desc":
                    artigos = artigos.OrderByDescending(a => a.Categoria.Designacao);
                    break;
                case "nome":
                    artigos = artigos.OrderBy(a => a.Dono.Nome);
                    break;
                case "nome_desc":
                    artigos = artigos.OrderByDescending(a => a.Dono.Nome);
                    break;
                default:
                    artigos = artigos.OrderBy(a => a.Titulo);
                    break;
            }
            return View(artigos.ToList());
        }

        // GET: Artigo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artigo artigo = db.Artigo.Find(id);
            if (artigo == null)
            {
                return HttpNotFound();
            }
            return View(artigo);
        }

        // GET: Artigo/Create
        public ActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "Designacao");
            ViewBag.IdDono = new SelectList(db.Utilizador, "IdUtilizador", "Nome");
            ViewBag.IdGestor = new SelectList(db.Utilizador, "IdUtilizador", "Nome");
            return View();
        }

        // POST: Artigo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArtigo,Titulo,Preco,Descricao,IdGestor,IdDono,IdCategoria")] Artigo artigo)
        {
            if (ModelState.IsValid)
            {
                db.Artigo.Add(artigo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "Designacao", artigo.IdCategoria);
            ViewBag.IdDono = new SelectList(db.Utilizador, "IdUtilizador", "Nome", artigo.IdDono);
            ViewBag.IdGestor = new SelectList(db.Utilizador, "IdUtilizador", "Nome", artigo.IdGestor);
            return View(artigo);
        }

        // GET: Artigo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artigo artigo = db.Artigo.Find(id);
            if (artigo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "Designacao", artigo.IdCategoria);
            ViewBag.IdDono = new SelectList(db.Utilizador, "IdUtilizador", "Nome", artigo.IdDono);
            ViewBag.IdGestor = new SelectList(db.Utilizador, "IdUtilizador", "Nome", artigo.IdGestor);
            return View(artigo);
        }

        // POST: Artigo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArtigo,Titulo,Preco,Descricao,IdGestor,IdDono,IdCategoria")] Artigo artigo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artigo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "Designacao", artigo.IdCategoria);
            ViewBag.IdDono = new SelectList(db.Utilizador, "IdUtilizador", "Nome", artigo.IdDono);
            ViewBag.IdGestor = new SelectList(db.Utilizador, "IdUtilizador", "Nome", artigo.IdGestor);
            return View(artigo);
        }

        // GET: Artigo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artigo artigo = db.Artigo.Find(id);
            if (artigo == null)
            {
                return HttpNotFound();
            }
            return View(artigo);
        }

        // POST: Artigo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artigo artigo = db.Artigo.Find(id);
            db.Artigo.Remove(artigo);
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
