using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Aula_03.Models
{
    public class Editora
    {
        public int ID { get; set; }
        [DisplayName("Editora")]
        [Required(ErrorMessage = "Por favor, informe o nome da editora")]
        [MinLength(3, ErrorMessage = "O nome da editora deve possuir, no mínimo três caracteres")]
        [MaxLength(250, ErrorMessage = "O Título do livro deve possuir, no máximo 250 caracteras")]
        public string NomeEditora { get; set; }
    }
}
