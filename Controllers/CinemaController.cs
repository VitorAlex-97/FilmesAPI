using AutoMapper;
using Domain.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private readonly CinemaService _cinemaService;
        public CinemaController(CinemaService cinemaService)
        {
            _cinemaService= cinemaService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cinema>> GetAll()
        {
            return _cinemaService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<ReadCinemaDto> GetOneById(int id)
        {
            var cinema = _cinemaService.GetOneById(id);
        
            if (cinema == null) return BadRequest("Cinema não encontrado");
            return cinema;
        }

        [HttpPost]
        public ActionResult<Cinema> Create([FromBody] CreateCinemaDto dto)
        {
            var result = _cinemaService.Create(dto);
            if (result.IsSuccess) 
            {
                var readCinemaDto = result.Value;
                return CreatedAtAction(nameof(Create), new {Id = readCinemaDto.Id }, readCinemaDto);    
            }

            return BadRequest(result.Reasons[0].Message);
        }

        [HttpPut("{id}")]
        public  IActionResult UpdateById(int id, [FromBody] UpdateCinemaDto dto)
        {
            var result = _cinemaService.UpdateById(id, dto);
            if (result.IsFailed) return BadRequest(result);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOneById(int id)
        {
            var results =_cinemaService.DeleteOneById(id);
            if (results.IsFailed) return BadRequest(results);
            return Ok();
        }

    }
}