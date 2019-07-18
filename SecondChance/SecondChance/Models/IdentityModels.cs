using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SecondChance.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            //instanciação do ICollection de Artigos Avaliados
            ListaArtigosAvaliados = new HashSet<Artigo>();

            //instanciação do ICollection de Artigos
            ListaArtigos = new HashSet<Artigo>();

            //instanciação do ICollection de Artigos favoritos
            ListaFavoritos = new HashSet<Artigo>();
        }

        //Atributos do utilizador

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Localidade { get; set; }

        [Required]
        [StringLength(1)]
        [RegularExpression("[MmFf]")]
        public string Sexo { get; set; }

        [Required]
        public DateTime Data_Nasc { get; set; }

        //***************************************************

        /// <summary>
        /// Lista de Artigos avaliados por um Gestor
        /// </summary>
        public virtual ICollection<Artigo> ListaArtigosAvaliados { get; set; }

        /// <summary>
        /// Lista de Artigos publicados por um utilizador
        /// </summary>
        public virtual ICollection<Artigo> ListaArtigos { get; set; }

        /// <summary>
        /// Lista de Artigos favoritos de um utilizador
        /// </summary>
        public virtual ICollection<Artigo> ListaFavoritos { get; set; }

        /// <summary>
        /// Lista de Mensagens enviadas
        /// </summary>
        public virtual ICollection<Mensagem> ListaMesgOrigem { get; set; }

        /// <summary>
        /// Lista de Mensagens recebidas
        /// </summary>
        public virtual ICollection<Mensagem> ListaMesgDestino { get; set; }

        //***************************************************


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //Tabelas da BD
        //public DbSet<ApplicationUser> Utilizadores { get; set; }
        public DbSet<Artigo> Artigos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Multimedia> RecMultimedia { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //eliminar a convenção de atribuir automaticamente o 'on Delete Cascade' nas FKs
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Artigo>()
                        .HasRequired(m => m.Gestor)
                        .WithMany(t => t.ListaArtigosAvaliados)
                        .HasForeignKey(m => m.IdGestor)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Artigo>()
                        .HasRequired(m => m.Dono)
                        .WithMany(t => t.ListaArtigos)
                        .HasForeignKey(m => m.IdDono)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mensagem>()
                        .HasRequired(m => m.UtilOrigem)
                        .WithMany(t => t.ListaMesgOrigem)
                        .HasForeignKey(m => m.IdUtilOrigem)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mensagem>()
                        .HasRequired(m => m.UtilDestino)
                        .WithMany(t => t.ListaMesgDestino)
                        .HasForeignKey(m => m.IdUtilDestino)
                        .WillCascadeOnDelete(false);
        }
    }
}