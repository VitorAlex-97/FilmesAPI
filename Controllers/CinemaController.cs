using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CinemaController : ControllerBase
  {
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public CinemaController(AppDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Cinema>> GetAll()
    {
      var cinemas =  _context.Cinemas.ToList() ;
      return cinemas;
    }

    [HttpGet("{id}")]
    public ActionResult<Cinema> GetOneById(int id)
    {
      var cinema = _context.Cinemas.FirstOrDefault(c => c.Id == id);
      if (cinema == null) {
        return BadRequest("Cinema não encontrado");
      }

      return cinema;
    }

    [HttpPost]
    public async Task<ActionResult<Cinema>> Create([FromBody] CreateCinemaDto dto)
    {
      var cinema = _mapper.Map<Cinema>(dto);
      await _context.Cinemas.AddAsync(cinema);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(Create), new {Id = cinema.Id}, cinema);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateById(int id, [FromBody] UpdateCinemaDto dto)
    {
      var cinema = _mapper.Map<Cinema>(dto);
      var cinemaDb = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

      if (cinemaDb == null) {
        return BadRequest("Cinema não encontrado");
      }
      
      _mapper.Map(dto, cinemaDb);
      await _context.SaveChangesAsync();
      var dtoResponse = _mapper.Map<ReadCinemaDto>(cinemaDb);
      return CreatedAtAction(nameof(UpdateById), new { Id = dtoResponse.Id }, dtoResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOneById(int id)
    {
      var cinemaDb = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
      if (cinemaDb == null) 
      {
        return BadRequest("Cinema não enctrado");
      }

      _context.Cinemas.Remove(cinemaDb);
      await _context.SaveChangesAsync();
      return Ok();
    }

  }
}