using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aula_03.Models
{
    public class ClassificacaoLivro
    {
        public int ID { get; set; }
        [DisplayName("Classificação")]
        [Required(ErrorMessage = "Por favor, informe a classificação")]
        [MinLength(3, ErrorMessage = "A classificação deve possuir, no mínimo três caracteres")]
        [MaxLength(250, ErrorMessage = "A classificação deve possuir, no máximo 250 caracteres")]
        public string Classificacao { get; set; }
    }
}
