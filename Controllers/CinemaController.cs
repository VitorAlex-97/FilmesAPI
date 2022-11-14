using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using Domain.Models;
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
    public ActionResult<ReadCinemaDto> GetOneById(int id)
    {
      var cinema = _context.Cinemas.FirstOrDefault(c => c.Id == id);
      if (cinema == null) {
        return BadRequest("Cinema não encontrado");
      }

      var readCinemaDtoResponse = _mapper.Map<ReadCinemaDto>(cinema);
      return readCinemaDtoResponse;
    }

    [HttpPost]
    public async Task<ActionResult<Cinema>> Create([FromBody] CreateCinemaDto dto)
    {
      var cinema = _mapper.Map<Cinema>(dto);
      var endereco = _context.Enderecos.FirstOrDefault(e => e.Id == dto.EnderecoId);
      if (endereco == null)
      {
        return BadRequest("S");
      }
      await _context.Cinemas.AddAsync(cinema);
      await _context.SaveChangesAsync();
      var readCinemaDtoResponse = _mapper.Map<ReadCinemaDto>(cinema);
      return CreatedAtAction(nameof(Create), new {Id = readCinemaDtoResponse.Id}, readCinemaDtoResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateById(int id, [FromBody] UpdateCinemaDto dto)
    {
      var cinema = _mapper.Map<Cinema>(dto);
      var cinemaDb = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

      if (cinemaDb == null) {
        return BadRequest("Cinema não encontrado");
      }
      
      _mapper.Map(cinema, cinemaDb);
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