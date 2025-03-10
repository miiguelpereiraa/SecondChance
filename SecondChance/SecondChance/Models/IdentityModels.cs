﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace SecondChance.Models{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class SecondChanceDB : IdentityDbContext<ApplicationUser>
    {
        public SecondChanceDB()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static SecondChanceDB Create()
        {
            return new SecondChanceDB();
        }

        static SecondChanceDB() {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<SecondChanceDB>(new ApplicationDbInitializer());
            }

        //Tabelas da BD
        public DbSet<Utilizador> Utilizador { get; set; }
        public DbSet<Artigo> Artigo { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Multimedia> RecMultimedia { get; set; }
        public DbSet<Mensagem> Mensagem { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

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

            //eliminar a convenção de atribuir automaticamente o 'on Delete Cascade' nas FKs
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

    }

}