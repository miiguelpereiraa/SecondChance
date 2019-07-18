namespace SecondChance.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SecondChance.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SecondChance.Models.SecondChanceDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SecondChance.Models.SecondChanceDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            SecondChanceDB db = new SecondChanceDB();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criação de utilizadores
            var user1 = new ApplicationUser();
            user1.UserName = "miguelpe";
            user1.Email = "miguelpe@example.com";
            string user1PWD = "Teste123!#";
            user1.Nome = "Miguel Pereira";
            user1.Localidade = "Vila Nova da Barquinha";
            user1.Sexo = "M";
            user1.Data_Nasc = new DateTime(1995, 7, 10);
            userManager.Create(user1, user1PWD);

            var user2 = new ApplicationUser();
            user2.UserName = "tiagoro";
            user2.Email = "tiagoro@example.com";
            string user2PWD = "Teste123!#";
            user2.Nome = "Tiago Rodrigues";
            user2.Localidade = "Tomar";
            user2.Sexo = "M";
            user2.Data_Nasc = new DateTime(1995, 7, 10);
            userManager.Create(user2, user2PWD);

            var categorias = new List<Categoria>
            {
                new Categoria {IdCategoria=1, Designacao="Informática"},
            };
            categorias.ForEach(cc => context.Categorias.AddOrUpdate(c => c.IdCategoria, cc));
            context.SaveChanges();

            var artigos = new List<Artigo>
            {
                new Artigo {IdArtigo=1, Titulo="Macbook Air 13", Preco=350, Descricao="Computador em óptimo estado.", IdGestor=user1.Id, IdDono=user2.Id, IdCategoria=1},
            };
            artigos.ForEach(aa => context.Artigos.AddOrUpdate(a => a.IdArtigo, aa));
            context.SaveChanges();

            var recMultimedia = new List<Multimedia>
            {
                new Multimedia {IdMultimedia=1, Designacao="comp1.jpg", Tipo="imagem", IdArtigo=1},
            };
            recMultimedia.ForEach(mm => context.RecMultimedia.AddOrUpdate(m => m.IdMultimedia, mm));
            context.SaveChanges();

            var mensagens = new List<Mensagem>
            {
                new Mensagem {IdMensagem=1, Conteudo="O artigo ainda está disponivel?", DataHora=DateTime.Now, IdUtilOrigem=user1.Id, IdUtilDestino=user2.Id},
                new Mensagem {IdMensagem=2, Conteudo="Sim, ainda está disponível", DataHora=DateTime.Now, IdUtilOrigem=user2.Id, IdUtilDestino=user1.Id},
            };
            mensagens.ForEach(mm => context.Mensagens.AddOrUpdate(m => m.IdMensagem, mm));
            context.SaveChanges();
        }
    }
}
