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

        [Required(ErrorMessage ="O preenchimento deste campo é obrigatório.")]
        public string Conteudo { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        //Chave Forasteira para identificar a origem da mensagem
        public int IdUtilOrigem { get; set; }
        public virtual Utilizador UtilOrigem { get; set; }

        //Chave Forasteira para identificar o destinatário da mensagem
        public int IdUtilDestino { get; set; }
        public virtual Utilizador UtilDestino { get; set; }

    }
}