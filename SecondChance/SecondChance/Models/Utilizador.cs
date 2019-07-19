using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecondChance.Models {
    public class Utilizador {

        public Utilizador()
        {
            //instanciação do ICollection de Artigos Avaliados
            ListaArtigosAvaliados = new HashSet<Artigo>();

            //instanciação do ICollection de Artigos
            ListaArtigos = new HashSet<Artigo>();

            //instanciação do ICollection de Artigos favoritos
            ListaFavoritos = new HashSet<Artigo>();
        }

        //Atributos do utilizador

        [Key]
        public int IdUtilizador { get; set; }

        [Required]
        public string Nome { get; set; }

        //Chave forasteira que liga um utilizador ao seu respectivo user
        public string UsernameID { get; set; }

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
    }
}