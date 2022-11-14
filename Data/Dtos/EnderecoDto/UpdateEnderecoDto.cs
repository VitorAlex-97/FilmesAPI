using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Data.Dtos
{
    public class UpdateEnderecoDto
    {
        [Required(ErrorMessage = "Campo Logradouro é obrigatório")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Campo Bairro é obrigatório")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Campo Numero é obrigatório")]
        public int Numero { get; set; }

        // public Cinema Cinema { get; set; }
    }
}