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
            //Caso o user não pertenca a role Gestores nem Utilizadores, redireccionar para a página inicial
            if (!User.IsInRole("Gestores") && !User.IsInRole("Utilizador"))
            {
                return RedirectToAction("../Artigo");
            }
            var recMultimedia = db.RecMultimedia.Include(m => m.Artigo);
            return View(recMultimedia.ToList());
        }

        // GET: Multimedia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            //Encontrar a imagem desejada
            Multimedia multimedia = db.RecMultimedia.Find(id);

            //Encontrar o artigo a que a imagem pertence
            Artigo artigo = db.Artigo.Where(a => a.IdArtigo == multimedia.IdArtigo).FirstOrDefault();

            //Não permite eliminar a imagem, caso o artigo apenas possua uma
            if (artigo.ListaRecMultimedia.Count() == 1)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }

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
            //Encontrar a imagem pertendida
            Multimedia multimedia = db.RecMultimedia.Find(id);
            //Eliminar a imagem
            db.RecMultimedia.Remove(multimedia);
            //Guardar as alterações
            db.SaveChanges();
            return RedirectToAction("../Artigo");
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
