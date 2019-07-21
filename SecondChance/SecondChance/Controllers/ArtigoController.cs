﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SecondChance.Models;
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;

namespace SecondChance.Controllers
{
    public class ArtigoController : Controller
    {
        private SecondChanceDB db = new SecondChanceDB();

        // GET: Artigo
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;

            ViewBag.ordenarTitulo = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.ordenarPreco = sortOrder == "preco" ? "preco_desc" : "preco";
            ViewBag.ordenarCategoria = sortOrder == "cat" ? "cat_desc" : "cat";
            ViewBag.ordenarNome = sortOrder == "nome" ? "nome_desc" : "nome";
            var artigos = db.Artigo.Include(a => a.Categoria).Include(a => a.Dono);


            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


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

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(artigos.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Gestores, Utilizador")]
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

        [Authorize(Roles = "Gestores, Utilizador")]
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
        [Authorize(Roles = "Gestores, Utilizador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Titulo,Preco,Descricao,IdCategoria")] Artigo artigo/*, HttpPostedFileBase ficheiro*/)
        {

            if (ModelState.IsValid)
            {
                var caminho = "";

                List<Multimedia> ficheiros = new List<Multimedia>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var ficheiro = Request.Files[i];
                    string mimeType = ficheiro.ContentType;

                    if (ficheiro != null && (mimeType == "image/jpeg" || mimeType == "image/png"))
                    {

                        Guid g;
                        g = Guid.NewGuid();
                        string extensao = Path.GetExtension(ficheiro.FileName).ToLower();
                        string nomeFicheiro = g.ToString() + extensao;

                        caminho = Path.Combine(Server.MapPath("~/Imagens/"), nomeFicheiro);

                        Multimedia fotografia = new Multimedia()
                        {
                            IdArtigo = artigo.IdArtigo,
                            Designacao = nomeFicheiro,
                            Tipo = "fotografia"
                        };

                        db.RecMultimedia.Add(fotografia);

                        ficheiro.SaveAs(caminho);

                    }
                    else
                    {
                        Multimedia porDefeito = new Multimedia()
                        {
                            IdArtigo = artigo.IdArtigo,
                            Designacao = "defaultThumbnail.jpg",
                            Tipo = "fotografia"
                        };

                        db.RecMultimedia.Add(porDefeito);
                    }
                }


                var curUser = db.Utilizador.Where(u => u.UsernameID == User.Identity.Name).FirstOrDefault();
                //if (curUser == null)
                //{
                //    return BadRequest(new { error: "Utilizador não encontrado" });
                //}

                artigo.IdDono = curUser.IdUtilizador;

                //valor por defeito
                artigo.Validado = false;

                db.Artigo.Add(artigo);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "Designacao", artigo.IdCategoria);
            ViewBag.IdDono = new SelectList(db.Utilizador, "IdUtilizador", "Nome", artigo.IdDono);
            return View(artigo);
        }

        [Authorize(Roles = "Gestores, Utilizador")]
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
            //ViewBag.IdGestor = new SelectList(db.Utilizador, "IdUtilizador", "Nome", artigo.IdGestor);
            return View(artigo);
        }

        // POST: Artigo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Gestores, Utilizador")]
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
            //ViewBag.IdGestor = new SelectList(db.Utilizador, "IdUtilizador", "Nome", artigo.IdGestor);
            return View(artigo);
        }

        [Authorize(Roles = "Gestores, Utilizador")]
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

        [Authorize(Roles = "Gestores, Utilizador")]
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
