using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SecondChance.Models
{
    public class Mensagem
    {

        [Key]
        public int IdMensagem { get; set; }

        [Required]
        public string Conteudo { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        //Chave Forasteira para identificar a origem da mensagem
        public int IdUtilOrigem { get; set; }
        public virtual ApplicationUser UtilOrigem { get; set; }

        //Chave Forasteira para identificar o destinatário da mensagem
        public int IdUtilDestino { get; set; }
        public virtual ApplicationUser UtilDestino { get; set; }

    }

    public class MensagemContext : DbContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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