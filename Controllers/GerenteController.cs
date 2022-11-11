using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public GerenteController(AppDbContext contex, IMapper mapper)
        {
            _context = contex;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateOne(CreateGerenteDto dto)
        {
            var gerente = _mapper.Map<Gerente>(dto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(CreateOne), new {Id = gerente.Id }, gerente);
        }

        [HttpGet("{id}")]
        public IActionResult GetOneById(int id)
        {
            var gerente = _context.Gerentes.FirstOrDefault(filme => filme.Id == id);
            if (gerente != null)
            {
                var gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);
                return Ok(gerenteDto);
            }

            return BadRequest();
        }
    }
}