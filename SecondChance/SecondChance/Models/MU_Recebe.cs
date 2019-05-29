using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SecondChance.Models
{
    public class MU_Recebe
    {
        //Chave primária por exigência da Entity Framework
        [Key]
        public int ID { get; set; }

        //Atributo do Relacionamento
        public DateTime Data_Hora_Recepcao { get; set; }

        //Chave estrangeira para Mensagem
        [ForeignKey(nameof(Mensagem))]
        public int MensagemFK { get; set; }
        public Mensagem Mensagem { get; set; }

        //Chave estrangeira para Utilizador
        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }
        public Utilizador Utilizador { get; set; }
    }
}