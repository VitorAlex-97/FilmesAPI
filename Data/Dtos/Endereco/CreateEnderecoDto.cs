using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Data.Dtos.Endereco
{
    public class CreateEnderecoDto
    {
        [Required(ErrorMessage = "Campo Logradouro é obrigatório")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Campo Logradouro é Bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Campo Logradouro é Número")]
        public int Numero { get; set; }
    }
}