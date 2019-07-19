using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecondChance.Models {
    public class Utilizador {

        public Utilizador() {
            ListaDeArtigos = new HashSet<Artigo>();
            }

        [Key] //Chave Primária
        public int ID { get; set; }

        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [StringLength(50, ErrorMessage = "O {0} deverá conter {1} caracteres no máximo.")]
           [RegularExpression("[A-ZÁÉÍÓÚ][a-záéíóúàèÌòùãõîôûâç]+(( | e | de | do | das | da | dos |-|')[A-ZÁÉÍÓÚ][a-zzáéíóúàèÌòùãõîôûâç]+)*",
             ErrorMessage ="O {0} só pode conter letras. Cada palavra deve começar com Maiúscula.")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [StringLength(30, ErrorMessage = "A {0} deverá conter {1} caracteres no máximo.")]
        [RegularExpression("[A-ZÁÉÍÓÚ][a-záéíóúàèÌòùãõîôûâç]+(( | e | de | do | das | da | dos |-|')[A-ZÁÉÍÓÚ][a-zzáéíóúàèÌòùãõîôûâç]+)*",
             ErrorMessage = "A {0} só pode conter letras. Cada palavra deve começar com Maiúscula.")]
        public String Localidade { get; set; }

        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [StringLength(30, ErrorMessage = "A {0} deverá conter {1} caracteres no máximo.")]
        [RegularExpression("Feminino|feminino|Masculino|masculino", ErrorMessage ="Deverá escrever Feminino ou Masculino.")]
        public String Sexo { get; set; }

        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [RegularExpression("[1-2][0-9][0-9][0-9]+-+[0-1][0-9]+-+[0-3][0-9]", ErrorMessage ="A {0} deverá esta no formato AAAA-MM-DD.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Este campo é de preenchimento obrigatório.")]
        [StringLength(9, ErrorMessage = "O campo {0} deverá conter {1} caracteres no máximo.")]
        [RegularExpression("[0-9]*", ErrorMessage ="O campo {0} só poderá conter números.")]
        public String Telemovel { get; set; }
        /// <summary>
        /// Lista de Artigos associados ao Utilizador 
        /// </summary>
        public virtual ICollection<Artigo> ListaDeArtigos { get; set; }
        }
    }