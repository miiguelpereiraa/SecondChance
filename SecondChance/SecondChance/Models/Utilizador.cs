using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecondChance.Models
{
    public class Utilizador
    {
        //Onde terá de ser feita a instanciação das listas de mensagens recebidas e enviadas para 
        //implementação da funcionalidade de conversação???

        [Key]
        public int Id_Utilizador { get; set; }

        public string Nome_Utilizador { get; set; }

        //Indica se é "Gestor" ou "Cliente" ???
        public string Tipo { get; set; }

        public string Nome { get; set; }

        public string Localidade { get; set; }

        public string Sexo { get; set; }

        public DateTime Data_Nasc { get; set; }

        public string Email { get; set; }

        //Lista de Mensagens Enviadas
        public ICollection<Mensagem> ListaMensagens { get; set; }

        //Lista de Mensagens Recebidas
        public ICollection<MU_Recebe> ListaMesgReceb { get; set; }

        //Lista que representa a wishlist (relacionamento wishlist entre utilizador e artigo)
        public ICollection<Artigo> Wishlist { get; set; }
    }
}