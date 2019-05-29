using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SecondChance.Models
{
    public class Mensagem
    {
        public Mensagem()
        {
            //Instanciação da lista de mensagens recebidas
            //(será com certeza necessário para listagem das mensagens na janela de conversação, a verificar!!!!)
            ListaMesgReceb = new HashSet<MU_Recebe>();
        }
        [Key]
        public int Id_Mensagem { get; set; }

        public string Conteudo { get; set; }

        public DateTime Data_Hora_Envio { get; set; }

        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }
        public Utilizador Utilizador { get; set; }

        public virtual ICollection<MU_Recebe> ListaMesgReceb { get; set; }
    }
}