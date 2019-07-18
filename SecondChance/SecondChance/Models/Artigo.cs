using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SecondChance.Models
{
    public class Artigo
    {

        public Artigo()
        {
            //instanciação do ICollection de utilizadores
            ListaUtilizadoresFav = new HashSet<ApplicationUser>();
        }

        [Key]
        public int IdArtigo { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public decimal Preco { get; set; }

        [Required]
        public string Descricao { get; set; }

        //Chave Forasteira para identificar o Gestor do Artigo
        public string IdGestor { get; set; }
        public virtual ApplicationUser Gestor { get; set; }

        //Chave Forasteira para identificar o Dono do Artigo
        public string IdDono { get; set; }
        public virtual ApplicationUser Dono { get; set; }

        //Chave Forasteira para categoria
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }

        /// <summary>
        /// Lista de utilizadores para exprimir o relacionamento para a lista de favoritos (M - N) entre Utilizadore e Artigo
        /// </summary>
        public virtual ICollection<ApplicationUser> ListaUtilizadoresFav { get; set; }

        /// <summary>
        /// Lista de recursos multimédia associados a um artigo
        /// </summary>
        public virtual ICollection<Multimedia> ListaRecMultimedia { get; set; }

    }
}