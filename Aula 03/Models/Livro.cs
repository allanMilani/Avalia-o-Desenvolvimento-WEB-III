using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Aula_03.Models
{
    public class Livro
    {
        [DisplayName("Identificador de Livro")]
        //[Required(ErrorMessage = "Este campo é obrigatório")]
        //[Range(1, 1000, ErrorMessage = "O valor deve ficar entre 1 e  1000")]
        public int Id { get; set; }

        [DisplayName("Título do Livro")]
        [Required(ErrorMessage = "Por favor, informe o título do livro")]
        [MinLength(3, ErrorMessage = "O título do livro deve possuir, no mínimo três caracteres")]
        [MaxLength(250, ErrorMessage = "O Título do livro deve possuir, no máximo 250 caracteres")]
        public string Titulo { get; set; }
        
        [DisplayName("Data de Publicação")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Date)]
        public DateTime DataPublicacao { get; set; }

    }
}
