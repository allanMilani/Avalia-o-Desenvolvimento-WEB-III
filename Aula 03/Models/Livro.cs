using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aula_03.Models
{
    public class Livro
    {
        [DisplayName("Identificador de Livro")]
        //[Required(ErrorMessage = "Este campo é obrigatório")]
        //[Range(1, 1000, ErrorMessage = "O valor deve ficar entre 1 e  1000")]
        public int ID { get; set; }

        [DisplayName("Título do Livro")]
        [Required(ErrorMessage = "Por favor, informe o título do livro")]
        [MinLength(3, ErrorMessage = "O título do livro deve possuir, no mínimo três caracteres")]
        [MaxLength(250, ErrorMessage = "O Título do livro deve possuir, no máximo 250 caracteres")]
        public string Titulo { get; set; }
        
        [DisplayName("Data de Publicação")]
        [Required(ErrorMessage = "Por favor, informe a data de publicação")]
        [DataType(DataType.Date)]
        public DateTime DataPublicacao { get; set; }

        [DisplayName("Quantidade de Páginas")]
        [Required(ErrorMessage = "Por favor, informe o número de páginas")]
        //[MinLength(1, ErrorMessage = "O livro deve possuir no mínimo uma página")]
        public int NumeroPagina { get; set; }

        [DisplayName("Acesso Online")]
        [Required(ErrorMessage = "Por favor, informe se o livro possui acesso online")]
        public bool AcessoOnline { get; set; }

        [DisplayName("Editora")]
        [Required(ErrorMessage = "Por favor, informe a editora")]
        public Editora Editora { get; set; }
        public int? EditoraID { get; set; }
        [DisplayName("Classicição do Livro")]
        [Required(ErrorMessage = "Por favor, informe a classificação do livro")]
        public ClassificacaoLivro ClassificacaoLivro { get; set; }
        public int? ClassificacaoLivroID { get; set; }


    }
}
