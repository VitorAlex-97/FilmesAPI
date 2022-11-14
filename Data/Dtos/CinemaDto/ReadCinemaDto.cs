using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace FilmesAPI.Data.Dtos
{
  public class ReadCinemaDto
  {
    public int Id { get; set; }

    [Required (ErrorMessage = "Nome é um campo obrigatório")]
    public string Nome { get; set; }

    public virtual Endereco Endereco { get; set; }
    public virtual Gerente Gerente { get; set; }
  }
}