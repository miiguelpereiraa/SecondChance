using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecondChance.Models {
    public class Utilizador {

        public Utilizador()
        {
            ////instanciação do ICollection de Artigo Avaliados
            //ListaArtigosAvaliados = new HashSet<Artigo>();

            //instanciação do ICollection de Artigo
            ListaArtigos = new HashSet<Artigo>();

            //instanciação do ICollection de Artigo favoritos
            ListaFavoritos = new HashSet<Artigo>();
        }

        //Atributos do utilizador

        [Key]
        public int IdUtilizador { get; set; }

        [Display(Name ="Nome do Utilizador")]
        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [StringLength(50, ErrorMessage = "O {0} deverá conter {1} caracteres no máximo.")]
        [RegularExpression("[A-ZÁÉÍÓÚ][a-záéíóúàèÌòùãõîôûâç]+(( | e | de | do | das | da | dos |-|')[A-ZÁÉÍÓÚ][a-zzáéíóúàèÌòùãõîôûâç]+)*",
             ErrorMessage = "O {0} só pode conter letras. Cada palavra deve começar com Maiúscula.")]
        public string Nome { get; set; }

        //Chave forasteira que liga um utilizador ao seu respectivo user
        [Display(Name = "Username")]
        public string UsernameID { get; set; }

        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [StringLength(30, ErrorMessage = "A {0} deverá conter {1} caracteres no máximo.")]
        [RegularExpression("[A-ZÁÉÍÓÚ][a-záéíóúàèÌòùãõîôûâç]+(( | e | de | do | das | da | dos |-|')[A-ZÁÉÍÓÚ][a-zzáéíóúàèÌòùãõîôûâç]+)*",
             ErrorMessage = "A {0} só pode conter letras. Cada palavra deve começar com Maiúscula.")]
        public string Localidade { get; set; }

        [Required]
        [StringLength(9)]
        [RegularExpression("Masculino|Feminino", ErrorMessage = "P.f. introduza Masculino ou Feminino")]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        //Está a dar erro de validação
        //[RegularExpression("^(?:0[1-9]|[12]\\d|3[01])([\\/.-])(?:0[1-9]|1[12])\\1(?:19|20)\\d\\d$", ErrorMessage = "A {0} deverá esta no formato AAAA-MM-DD.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNasc { get; set; }

        //***************************************************

        ///// <summary>
        ///// Lista de Artigo avaliados por um Gestor
        ///// </summary>
        //public virtual ICollection<Artigo> ListaArtigosAvaliados { get; set; }

        /// <summary>
        /// Lista de Artigo publicados por um utilizador
        /// </summary>
        public virtual ICollection<Artigo> ListaArtigos { get; set; }

        /// <summary>
        /// Lista de Artigo favoritos de um utilizador
        /// </summary>
        public virtual ICollection<Artigo> ListaFavoritos { get; set; }

        /// <summary>
        /// Lista de Mensagem enviadas
        /// </summary>
        public virtual ICollection<Mensagem> ListaMesgOrigem { get; set; }

        /// <summary>
        /// Lista de Mensagem recebidas
        /// </summary>
        public virtual ICollection<Mensagem> ListaMesgDestino { get; set; }

        //***************************************************
    }
}