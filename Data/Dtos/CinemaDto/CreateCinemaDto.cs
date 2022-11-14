using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Data.Dtos
{
    public class CreateCinemaDto
    {
        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Campo EnderecoId é obrigatório")]
        public int EnderecoId { get; set; }
        public int GerenteId { get; set; }
    }
}