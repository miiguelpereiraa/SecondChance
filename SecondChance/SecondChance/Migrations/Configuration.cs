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

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //################### Criação de Roles ###################
            //Role Gestores
            if (!roleManager.RoleExists("Gestores"))
            {
                var role = new IdentityRole();
                role.Name = "Gestores";
                roleManager.Create(role);
            }

            //Role Utilizador
            if (!roleManager.RoleExists("Utilizador"))
            {
                var role = new IdentityRole();
                role.Name = "Utilizador";
                roleManager.Create(role);
            }

            ////################### Criação de utilizadores ###################
            var user1 = new ApplicationUser();
            user1.UserName = "miguelpe@example.com";
            user1.Email = "miguelpe@example.com";
            string user1PWD = "Teste123!#";
            var chkUser1 = userManager.Create(user1, user1PWD);

            var user2 = new ApplicationUser();
            user2.UserName = "tiagoro@example.com";
            user2.Email = "tiagoro@example.com";
            string user2PWD = "Teste123!#";
            var chkUser2 = userManager.Create(user2, user2PWD);

            var user3 = new ApplicationUser();
            user3.UserName = "jarmando@example.com";
            user3.Email = "jarmando@example.com";
            string user3PWD = "Teste123!#";
            var chkUser3 = userManager.Create(user3, user3PWD);

            var user4 = new ApplicationUser();
            user4.UserName = "jsantos@example.com";
            user4.Email = "jsantos@example.com";
            string user4PWD = "Teste123!#";
            var chkUser4 = userManager.Create(user4, user4PWD);

            //################### Adição de utilizadores às respectivas Roles ###################
            if (chkUser1.Succeeded)
            {
                userManager.AddToRole(user1.Id, "Gestores");
                userManager.AddToRole(user1.Id, "Utilizador");
            }

            if (chkUser2.Succeeded)
            {
                userManager.AddToRole(user2.Id, "Gestores");
                userManager.AddToRole(user2.Id, "Utilizador");
            }

            if (chkUser3.Succeeded)
            {
                userManager.AddToRole(user3.Id, "Utilizador");
            }

            if (chkUser4.Succeeded)
            {
                userManager.AddToRole(user4.Id, "Utilizador");
            }


            var utilizadores = new List<Utilizador>
            {
                new Utilizador {IdUtilizador=1, Nome="Miguel Pereira", UsernameID="miguelpe@example.com", Localidade="Vila Nova da Barquinha", Sexo="Masculino", DataNasc=new DateTime(1995, 7, 10)},
                new Utilizador {IdUtilizador=2, Nome="Tiago Rodrigues", UsernameID="tiagoro@example.com", Localidade="Tomar", Sexo="Masculino", DataNasc=new DateTime(1995, 7, 10)},
                new Utilizador {IdUtilizador=3, Nome="José Armando", UsernameID="jarmando@example.com", Localidade="Torres Novas", Sexo="Masculino", DataNasc=new DateTime(1990, 12, 18)},
                new Utilizador {IdUtilizador=4, Nome="Joana Santos", UsernameID="jsantos@example.com", Localidade="Entroncamento", Sexo="Feminino", DataNasc=new DateTime(1993, 2, 15)}
            };
            utilizadores.ForEach(uu => context.Utilizador.AddOrUpdate(u => u.IdUtilizador, uu));
            context.SaveChanges();

            var categorias = new List<Categoria>
            {
                new Categoria {IdCategoria=1, Designacao="Informática"},
                new Categoria{IdCategoria=2, Designacao="Brinquedos"},
                new Categoria{IdCategoria=3, Designacao="Móveis, Decoração e Jardim"},
                new Categoria{IdCategoria=4, Designacao="Imóveis"},
                new Categoria{IdCategoria=5, Designacao="Moda"},
                new Categoria{IdCategoria=6, Designacao="Veículos"},
                new Categoria{IdCategoria=7, Designacao="Instrumentos Musicais"},
                new Categoria{IdCategoria=8, Designacao="Equipamentos e Ferramentas"},
                new Categoria{IdCategoria=9, Designacao="Outras Vendas"},
            };
            categorias.ForEach(cc => context.Categoria.AddOrUpdate(c => c.IdCategoria, cc));
            context.SaveChanges();

            var artigos = new List<Artigo>
            {
                new Artigo {IdArtigo=1, Titulo="Macbook Air 13", Preco=350, Descricao="Computador em óptimo estado.", IdGestor=1, IdDono=1, IdCategoria=1},
                new Artigo {IdArtigo=2, Titulo="Guitarra Elétrica Fender", Preco=150, Descricao="Guitarra em bom estado, com cordas novas.", IdGestor=1, IdDono=3, IdCategoria=7},
                new Artigo {IdArtigo=3, Titulo="BMW 318", Preco=10000, Descricao="Carro como novo, apenas com pneus novos.", IdGestor=1, IdDono=2, IdCategoria=6},
                new Artigo {IdArtigo=4, Titulo="Cozinha de brincar", Preco=25, Descricao="Cozinha para crianças até 3 anos.", IdGestor=2, IdDono=4, IdCategoria=2},
                new Artigo {IdArtigo=5, Titulo="Berbequim", Preco=100, Descricao="Berbequim em bom estado e inclui 5 brocas.", IdGestor=1, IdDono=2, IdCategoria=8},
                new Artigo {IdArtigo=6, Titulo="Vestido Azul", Preco=15, Descricao="Vestido formal usado apenas duas vezes.", IdGestor=1, IdDono=3, IdCategoria=5},
            };
            artigos.ForEach(aa => context.Artigo.AddOrUpdate(a => a.IdArtigo, aa));
            context.SaveChanges();

            var recMultimedia = new List<Multimedia>
            {
                new Multimedia {IdMultimedia=1, Designacao="comp1.jpg", Tipo="imagem", IdArtigo=1},
                new Multimedia {IdMultimedia=2, Designacao="guit1.jpg", Tipo="imagem", IdArtigo=2},
                new Multimedia {IdMultimedia=3, Designacao="bmw1.jpg", Tipo="imagem", IdArtigo=3},
                new Multimedia {IdMultimedia=4, Designacao="bmw2.jpg", Tipo="imagem", IdArtigo=3},
                new Multimedia {IdMultimedia=5, Designacao="coz1.jpg", Tipo="imagem", IdArtigo=4},
                new Multimedia {IdMultimedia=6, Designacao="berb1.jpg", Tipo="imagem", IdArtigo=5},
                new Multimedia {IdMultimedia=7, Designacao="vestid1.jpg", Tipo="imagem", IdArtigo=6},
            };
            recMultimedia.ForEach(mm => context.RecMultimedia.AddOrUpdate(m => m.IdMultimedia, mm));
            context.SaveChanges();

            var mensagens = new List<Mensagem>
            {
                new Mensagem {IdMensagem=1, Conteudo="O artigo ainda está disponivel?", DataHora=DateTime.Now, IdUtilOrigem=1, IdUtilDestino=2},
                new Mensagem {IdMensagem=2, Conteudo="Sim, ainda está disponível", DataHora=DateTime.Now, IdUtilOrigem=2, IdUtilDestino=1},
                new Mensagem {IdMensagem=3, Conteudo="Pode informar os Km que o carro tem?", DataHora=DateTime.Now, IdUtilOrigem=4, IdUtilDestino=2},
                new Mensagem {IdMensagem=4, Conteudo="Que altura tem a cozinha?", DataHora=DateTime.Now, IdUtilOrigem=3, IdUtilDestino=4},
                new Mensagem {IdMensagem=5, Conteudo="Qual é o tamanho do vestido?", DataHora=DateTime.Now, IdUtilOrigem=1, IdUtilDestino=3},
                new Mensagem {IdMensagem=6, Conteudo="O carro tem 50 000 Km", DataHora=DateTime.Now, IdUtilOrigem=2, IdUtilDestino=4},
            };
            mensagens.ForEach(mm => context.Mensagem.AddOrUpdate(m => m.IdMensagem, mm));
            context.SaveChanges();
        }
    }
}
