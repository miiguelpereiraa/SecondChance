using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecondChance.Models
{
    public class Categoria
    {

        public Categoria()
        {
            ListaArtigos = new HashSet<Artigo>();
        }

        [Key]
        public int IdCategoria { get; set; }

        [Required]
        public string Designacao { get; set; }

        /// <summary>
        /// Lista de artigos associados a uma categoria
        /// </summary>
        public virtual ICollection<Artigo> ListaArtigos { get; set; }

    }
}