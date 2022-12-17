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
}