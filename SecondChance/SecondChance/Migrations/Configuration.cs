namespace SecondChance.Migrations
{
    using SecondChance.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SecondChance.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SecondChance.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var utilizadores = new List<ApplicationUser>
            {
                new ApplicationUser {Id="914e83b0-8f66-47af-b3a8-517c021697e5", UserName="miguepe", PasswordHash="?", Nome="Miguel Pereira", Localidade="Vila Nova da Barquinha", Sexo="M", Data_Nasc=new DateTime(1995,7,10) },
                new ApplicationUser {Id="3d48733f-02c4-4e4a-8456-37968437846e", UserName="tiagoro", PasswordHash="?", Nome="Tiago Rodrigues", Localidade="Tomar", Sexo="M", Data_Nasc=new DateTime(1995,1,1) }
            };
            utilizadores.ForEach(uu => context.Users.AddOrUpdate(u => u.Id, uu));
            context.SaveChanges();

            var artigos = new List<Artigo>
            {
                new Artigo {IdArtigo=1, Titulo="Macbook Air 13", Preco=350, Descricao="Computador em óptimo estado.", IdGestor="914e83b0-8f66-47af-b3a8-517c021697e5", IdDono="3d48733f-02c4-4e4a-8456-37968437846e", IdCategoria=?},
            };
            artigos.ForEach(aa => context.Artigos.AddOrUpdate(a => a.IdArtigo, aa));
            context.SaveChanges();

            var categorias = new List<Categoria>
            {
                new Categoria {IdCategoria=1, Designacao="Informática"},
            };
            categorias.ForEach(cc => context.Categorias.AddOrUpdate(c => c.IdCategoria, cc));
            context.SaveChanges();

            var recMultimedia = new List<Multimedia>
            {
                new Multimedia {IdMultimedia=1, Designacao="comp1.jpg", Tipo="imagem", IdArtigo=1},
            };
            recMultimedia.ForEach(mm => context.RecMultimedia.AddOrUpdate(m => m.IdMultimedia, mm));
            context.SaveChanges();

            var mensagens = new List<Mensagem>
            {
                new Mensagem {IdMensagem=1, Conteudo="O artigo ainda está disponivel?", DataHora=DateTime.Now, IdUtilOrigem="914e83b0-8f66-47af-b3a8-517c021697e5", IdUtilDestino="3d48733f-02c4-4e4a-8456-37968437846e"},
                new Mensagem {IdMensagem=2, Conteudo="Sim, ainda está disponível", DataHora=DateTime.Now, IdUtilOrigem="3d48733f-02c4-4e4a-8456-37968437846e", IdUtilDestino="914e83b0-8f66-47af-b3a8-517c021697e5"},
            };
            mensagens.ForEach(mm => context.Mensagens.AddOrUpdate(m => m.IdMensagem, mm));
            context.SaveChanges();
        }
    }
}
