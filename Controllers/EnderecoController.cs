using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Endereco;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public EnderecoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateEnderecoDto dto)
        {
            var endereco = _mapper.Map<Endereco>(dto);
            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), new { Id = endereco.Id }, endereco);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Endereco>> GetAll() 
        {
            var enderecosGroup = _context.Enderecos.ToList();
            var endereResponseDto = _mapper.Map<List<ReadEnderecoDto>>(enderecosGroup);
            return Ok(endereResponseDto);
        }

        [HttpGet("{id}")]
        public ActionResult<Endereco> GetOneById(int id)
        {
            var endereco = _context.Enderecos.FirstOrDefault(e => e.Id == id);
            if(endereco == null)
            {
                return BadRequest("Endereço não encontrado");
            }

            var dtoResponse = _mapper.Map<ReadEnderecoDto>(endereco);
            return Ok(dtoResponse);
        }

        [HttpPut]
        public async Task<ActionResult<Endereco>> UpdateOne([FromBody] UpdateEnderecoDto dto, int id)
        {
            var endereco = _mapper.Map<Endereco>(dto);
            var enderecoDb = _context.Enderecos.FirstOrDefault(e => e.Id == id);

            if (enderecoDb == null)
            {
                return BadRequest("Endereço não encontrado");
            }

            _mapper.Map(endereco, enderecoDb);
            await _context.SaveChangesAsync();
            var dtoResponse = _mapper.Map<ReadEnderecoDto>(enderecoDb);
            return Ok(dtoResponse);
        }
    }
}