using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
  public class Cinema
  {
    [Key]
    [Required]
    public int Id { get; set; }

    [Required (ErrorMessage = "Nome é um campo obrigatório")]
    public string Nome { get; set; }
    public virtual Endereco Endereco { get; set; }
    public int EnderecoId { get; set; }
    public virtual Gerente Gerente { get; set; }
    public int GerenteId { get; set; }
    public virtual List<Filme> Filmes { get; set; }
  }
}