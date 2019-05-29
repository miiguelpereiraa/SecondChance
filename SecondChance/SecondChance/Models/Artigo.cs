using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondChance.Models
{
    public class Artigo
    {
        public int Id_Artigo { get; set; }

        public string Titulo { get; set; }

        public string Categoria { get; set; }

        public decimal Preco { get; set; }

        public string Descricao { get; set; }

        public string Tipo_Anunciante { get; set; }

        //Lista que representa o relacionamento wishlist entre utilizador e artigo
        public ICollection<Utilizador> ListaUtilizadores { get; set; }
    }
}