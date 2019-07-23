using System;
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

            //Obter lista de artigos
            var artigos = db.Artigo.Include(a => a.Categoria).Include(a => a.Dono);

            //Definir a ordenação seleccionada, para quando se alterar a página, a ordenação se manter a mesma
            ViewBag.CurrentSort = sortOrder;

            //Ordenações usadas - Ascendente/Descendente por Titulo, Preço, Categoria, Nome e Validação
            ViewBag.ordenarTitulo = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.ordenarPreco = sortOrder == "preco" ? "preco_desc" : "preco";
            ViewBag.ordenarCategoria = sortOrder == "cat" ? "cat_desc" : "cat";
            ViewBag.ordenarNome = sortOrder == "nome" ? "nome_desc" : "nome";
            ViewBag.ordenarValidado = sortOrder == "valid" ? "valid_desc" : "valid";

            //Se o user não pertence à role dos gestores, obter apenas os artigos que estão validados
            if (!User.IsInRole("Gestores"))
            {
                artigos = db.Artigo.Include(a => a.Categoria).Include(a => a.Dono).Where(a => a.Validado);
            }

            //Se não houver nenhuma string que pesquisa, obter a primeira página de artigos, se não, obter os artigos correspondentes ao filtro a ser usado
            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            //Obter os artigos em que o título contém palavras da string de pesquisa
            if (!String.IsNullOrEmpty(searchString))
            {
                artigos = artigos.Where(a => a.Titulo.Contains(searchString));
            }

            //Ordenar os artigos de acordo com a ordenação escolhida
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
                case "valid":
                    artigos = artigos.OrderBy(a => a.Validado);
                    break;
                case "valid_desc":
                    artigos = artigos.OrderByDescending(a => a.Validado);
                    break;
                default:
                    artigos = artigos.OrderBy(a => a.Titulo);
                    break;
            }
            
            //número de artigos por página
            int pageSize = 5;
            // ?? - null-coalescing operator - define um valor por defeito para um tipo de dados que pode ser null
            //Se houver um valor para page, ele é retornado, se não retornar 1 se o valor de page for null
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
        public ActionResult Create([Bind(Include = "Titulo,Preco,Descricao,Contacto,IdCategoria")] Artigo artigo)
        {

            if (ModelState.IsValid)
            {
                //variável auxiliar para guardar o caminho da imagem
                var caminho = "";

                //Criar uma lista de "Multimedia"
                List<Multimedia> ficheiros = new List<Multimedia>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //Obtém cada ficheiro enviado pelo cliente
                    var ficheiro = Request.Files[i];
                    //Obtém o tipo do ficheiro enviado
                    string mimeType = ficheiro.ContentType;

                    //Verifica se foir introduzido um ficheiro
                    //Garante que apenas sao aceites imagens com extensão JPEG ou PNG
                    if (ficheiro != null && (mimeType == "image/jpeg" || mimeType == "image/png"))
                    {
                        //criar um guid para atribuir ao nome do ficheiro enviado, garantindo que não existem ficheiros com nomes iguais
                        Guid g;
                        g = Guid.NewGuid();
                        //obter a extensão do ficheiro
                        string extensao = Path.GetExtension(ficheiro.FileName).ToLower();
                        //Concatenar o nome do ficheiro com a extensão
                        string nomeFicheiro = g.ToString() + extensao;

                        //Concatenar num "Path" o caminho onde irá ser guardado o ficheiro no servidor com o seu nome 
                        caminho = Path.Combine(Server.MapPath("~/Imagens/"), nomeFicheiro);

                        //Criar um novo objecto multimedia
                        Multimedia fotografia = new Multimedia()
                        {
                            IdArtigo = artigo.IdArtigo,
                            Designacao = nomeFicheiro,
                            Tipo = "fotografia"
                        };

                        //Adicionar o objecto multimedia a base de dados
                        db.RecMultimedia.Add(fotografia);

                        //Gravar os ficheiros no servidor
                        ficheiro.SaveAs(caminho);

                    }
                    //Se não foi enviado nenhum ficheiro, atribuir a imagem por defeito ao artigo
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

                //Obter o user que está a criar o novo artigo e atribuir o seu id ao artigo que está a ser criado (IdDono)
                var curUser = db.Utilizador.Where(u => u.UsernameID == User.Identity.Name).FirstOrDefault();
                artigo.IdDono = curUser.IdUtilizador;

                //Colocar o artigo como não válido, será necessário o mesmo ser avaliado por um gestor antes de se tornar público
                artigo.Validado = false;

                //Adicionar artigo à base de dados e guardar as alterações
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
            //Obtém o id do dono do artigo
            var userArtigo = db.Artigo.Where(a => a.IdArtigo == id).FirstOrDefault().IdDono;
            //Obtém o username do dono do artigo
            var username = db.Utilizador.Where(u => u.IdUtilizador == userArtigo).FirstOrDefault().UsernameID;

            //Se o username do utilizador que está a solicitar a edição for diferente do username do dono do artigo ou se não for gestor, retornar a página inicial
            if(User.Identity.Name != username && !User.IsInRole("Gestores"))
            {
                return RedirectToAction("../Artigo");
            }

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
            return View(artigo);
        }

        // POST: Artigo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Gestores, Utilizador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArtigo,Titulo,Preco,Descricao,Contacto,IdDono,IdGestor,IdCategoria,Validado")] Artigo artigo)
        {
            if (ModelState.IsValid)
            {
                //inserção de novas imagens
                var caminho = "";

                //Criar uma lista de "Multimedia"
                List<Multimedia> ficheiros = new List<Multimedia>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    //Obtém cada ficheiro enviado pelo cliente
                    var ficheiro = Request.Files[i];
                    //Obtém o tipo do ficheiro enviado
                    string mimeType = ficheiro.ContentType;

                    if (ficheiro != null && (mimeType == "image/jpeg" || mimeType == "image/png"))
                    {
                        //criar um guid para atribuir ao nome do ficheiro enviado, garantindo que não existem ficheiros com nomes iguais
                        Guid g;
                        g = Guid.NewGuid();
                        //obter a extensão do ficheiro
                        string extensao = Path.GetExtension(ficheiro.FileName).ToLower();
                        //Concatenar o nome do ficheiro com a extensão
                        string nomeFicheiro = g.ToString() + extensao;

                        //Concatenar num "Path" o caminho onde irá ser guardado o ficheiro no servidor com o seu nome 
                        caminho = Path.Combine(Server.MapPath("~/Imagens/"), nomeFicheiro);

                        //Criar um novo objecto multimedia
                        Multimedia fotografia = new Multimedia()
                        {
                            IdArtigo = artigo.IdArtigo,
                            Designacao = nomeFicheiro,
                            Tipo = "fotografia"
                        };

                        //Adicionar o objecto multimedia a base de dados
                        db.RecMultimedia.Add(fotografia);

                        //Gravar o ficheiro no servidor
                        ficheiro.SaveAs(caminho);

                    }
                }

                //Guardar as alterações efectuadas
                db.Entry(artigo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(db.Categoria, "IdCategoria", "Designacao", artigo.IdCategoria);
            ViewBag.IdDono = new SelectList(db.Utilizador, "IdUtilizador", "Nome", artigo.IdDono);
            return View(artigo);
        }

        [Authorize(Roles = "Gestores, Utilizador")]
        // GET: Artigo/Delete/5
        public ActionResult Delete(int? id)
        {
            //Obtém o id do dono do artigo
            var userArtigo = db.Artigo.Where(a => a.IdArtigo == id).FirstOrDefault().IdDono;
            //Obtém o username do dono do artigo
            var username = db.Utilizador.Where(u => u.IdUtilizador == userArtigo).FirstOrDefault().UsernameID;

            //Se o username do utilizador que está a solicitar a edição for diferente do username do dono do artigo ou se não for gestor, retornar a página inicial
            if (User.Identity.Name != username && !User.IsInRole("Gestores"))
            {
                return RedirectToAction("../Artigo");
            }


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

            List<Multimedia> ficheiros = db.RecMultimedia.Where(m => m.IdArtigo == id).ToList();

            foreach(var ficheiro in ficheiros)
            {
                db.RecMultimedia.Remove(ficheiro);
            }
            
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
