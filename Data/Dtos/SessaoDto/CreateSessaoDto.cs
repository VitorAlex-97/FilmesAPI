using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace FilmesAPI.Data.Dtos
{
    public class CreateSessaoDto
    {
    
        [Required(ErrorMessage = "Campo CinemaId é obrigatório")]
        public int CinemaId { get; set; }

        [Required(ErrorMessage = "Campo FilmeId é obrigatório")]
        public int FilmeId { get; set; }
        public DateTime HorarioEnceramento { get; set; }
    }
}