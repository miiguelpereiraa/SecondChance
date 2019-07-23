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
    [Authorize]
    public class UtilizadorController : Controller
    {
        private SecondChanceDB db = new SecondChanceDB();

        // GET: Utilizador
        public ActionResult Index()
        {
            //Se o utilizador que está a tentar aceder não pertence á role Gestores nem Utilizadores, redireccionar para a página inicial
            if (!User.IsInRole("Gestores") && !User.IsInRole("Utilizador"))
            {
                return RedirectToAction("../Artigo");
            }

            return View(db.Utilizador.ToList());
        }

        // GET: Utilizador/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizador.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            return View(utilizador);
        }

        // GET: Utilizador/Edit/5
        public ActionResult Edit(int? id)
        {
            //Obtém o username do utilizador a editar
            var user = db.Utilizador.Find(id);

            if(user == null)
            {
                return RedirectToAction("../Artigo");
            }

            //Se o utilizador não é gestor mas está a tentar editar a sua própria informação
            if(User.Identity.Name == user.UsernameID)
            {
                return View(user);
            }

            //Se o utilizador é gestor
            if (User.IsInRole("Gestores"))
            {
                return View(user);
            }

            //Se o username do utilizador que está a solicitar a edição for diferente do username do utilizador a editar ou se não for gestor, retornar a página inicial
            if (User.Identity.Name != user.UsernameID && !User.IsInRole("Gestores"))
            {
                return RedirectToAction("../Artigo");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("../Artigo");
        }

        // POST: Utilizador/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtilizador,Nome,UsernameID,Localidade,Sexo,DataNasc")] Utilizador utilizador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilizador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utilizador);
        }

        // GET: Utilizador/Delete/5
        public ActionResult Delete(int? id)
        {
            //Se o utilizador que está a tentar aceder não pertence á role Gestores, redireccionar para a página inicial
            if (!User.IsInRole("Gestores"))
            {
                return RedirectToAction("../Artigo");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilizador utilizador = db.Utilizador.Find(id);
            if (utilizador == null)
            {
                return HttpNotFound();
            }
            return View(utilizador);
        }

        // POST: Utilizador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilizador utilizador = db.Utilizador.Find(id);
            db.Utilizador.Remove(utilizador);
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
