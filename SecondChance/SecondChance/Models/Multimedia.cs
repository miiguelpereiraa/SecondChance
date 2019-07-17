using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SecondChance.Models
{
    public class Multimedia
    {

        [Key]
        public int IdMultimedia { get; set; }

        public string Designacao { get; set; }

        [Required]
        public string Tipo { get; set; }

        //Chave Forasteira para Artigo
        [ForeignKey("Artigo")]
        public int IdArtigo { get; set; }
        public virtual Artigo Artigo { get; set; }

    }
}