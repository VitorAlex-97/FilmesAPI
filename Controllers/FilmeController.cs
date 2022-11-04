using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class FilmeController : ControllerBase
  {
    private AppDbContext _context;
    private IMapper _mapper;

    public FilmeController(AppDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateFilmeDto filmeDto)
    {
      Filme filme = _mapper.Map<Filme>(filmeDto);

      _context.Filmes.Add(filme);
      _context.SaveChanges();
      return CreatedAtAction(nameof(Create), new {Id = filme.Id }, filme);
    }

    [HttpGet]
    public IEnumerable<Filme> GetAll()
    {
      var filmesDto = _mapper.Map<List<Filme>>(_context.Filmes.ToList());
      return filmesDto;
    }

    [HttpGet("{id}")]
    public IActionResult GetOneById(int id)
    {
      Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
      if (filme != null)
      {
        ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return Ok(filmeDto);
      }

      return BadRequest();
    }

    [HttpPut]
    public IActionResult UpdateOne(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
      var filmeDb = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
      if (filmeDb == null)
      {
        return BadRequest("Filme não encontrado");
      } 
      _mapper.Map(filmeDto, filmeDb);
      _context.SaveChanges();
      return NoContent();
    }

    [HttpDelete]
    public IActionResult DeleteOne(int id)
    {
      var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
      if (filme == null)
      {
        return NoContent();
      }
      _context.Filmes.Remove(filme);
      _context.SaveChanges();

    return NoContent();
    }
  }
}