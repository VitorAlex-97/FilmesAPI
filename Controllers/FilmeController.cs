using AutoMapper;
using Domain.Models;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeService _filmeService;

        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateFilmeDto filmeDto)
        {
            var readFilmeDto = _filmeService.Create(filmeDto);
            
            return CreatedAtAction(nameof(Create), new {Id = readFilmeDto.Id }, readFilmeDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Filme>> GetAll([FromQuery] int? classificacaoEtaria = null)
        {
            var readFilmesDto = _filmeService.GetAll(classificacaoEtaria);
          
            return Ok(readFilmesDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetOneById(int id)
        {
            ReadFilmeDto readFilmeDto = _filmeService.GetOneById(id);

            if (readFilmeDto != null) return Ok(readFilmeDto);
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateOne(int id, [FromBody] UpdateFilmeDto updateFilmeDto)
        {
            var result = _filmeService.UpdateOne(updateFilmeDto, id);
            
            if (result.IsFailed) return BadRequest(result);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteOne(int id)
        {
            var results = _filmeService.DeleteOne(id);

            if (results.IsFailed) return BadRequest(results);
            return Ok();
        }
    }
<<<<<<< HEAD

    [HttpPost]
    public IActionResult Create([FromBody] CreateFilmeDto filmeDto)
    {
      Filme filme = _mapper.Map<Filme>(filmeDto);

      _context.Filmes.Add(filme);
      _context.SaveChanges();
      return CreatedAtAction(nameof(Create), new {Id = filme.Id }, filme);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Filme>> GetAll([FromQuery] int? classificacaoEtaria = null)
    {
      IEnumerable<Filme> filmes = new List<Filme>();
      
      if (classificacaoEtaria == null)
      {
        filmes = _context.Filmes.ToList();
      } 
      else {
        filmes  = _context.Filmes
                    .Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria)
                    .ToList();
      }

      
      if (filmes == null)
      {
        filmes = Enumerable.Empty<Filme>();
      }

      var filmesDto = _mapper.Map<List<Filme>>(filmes);
      return Ok(filmesDto);
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
=======
>>>>>>> main
}