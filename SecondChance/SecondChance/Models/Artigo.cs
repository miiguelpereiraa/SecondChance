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
            ListaUtilizadoresFav = new HashSet<Utilizador>();
        }

        [Key] //Identifica a chave primária
        public int IdArtigo { get; set; }

        [Display(Name ="Título")]
        [Required(ErrorMessage="O preenchimento do {0} é obrigatório.")]
        [StringLength(75, ErrorMessage = "O {0} deverá ter, no máximo, {1} caracteres.")]
        public string Titulo { get; set; }

        [Display(Name = "Preço")]
        [Required(ErrorMessage = "O preenchimento do {0} é obrigatório.")]
        [RegularExpression("^([0-9]{0,5}((,)[0-9]{0,3}))$", ErrorMessage ="Este campo apenas poderá conter números.")]
        public string Preco { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O preenchimento da {0} é obrigatório.")]
        [StringLength(500, ErrorMessage = "O {0} deverá ter, no máximo, {1} caracteres.")]
        public string Descricao { get; set; }

        public bool Validado { get; set; }

        ////Chave Forasteira para identificar o Gestor do Artigo
        //public int IdGestor { get; set; }
        //public virtual Utilizador Gestor { get; set; }

        //Chave Forasteira para identificar o Dono do Artigo
        public int IdDono { get; set; }
        public virtual Utilizador Dono { get; set; }

        //Chave Forasteira para categoria
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }

        /// <summary>
        /// Lista de utilizadores para exprimir o relacionamento para a lista de favoritos (M - N) entre Utilizadore e Artigo
        /// </summary>
        public virtual ICollection<Utilizador> ListaUtilizadoresFav { get; set; }

        /// <summary>
        /// Lista de recursos multimédia associados a um artigo
        /// </summary>
        public virtual ICollection<Multimedia> ListaRecMultimedia { get; set; }

    }
}