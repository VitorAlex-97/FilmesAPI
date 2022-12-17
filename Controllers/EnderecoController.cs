
using Domain.Models;
using FilmesAPI.Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Services;
using FluentResults;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoService _enderecoService;
        public EnderecoController(EnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateEnderecoDto dto)
        {
            var readEnderecoDto = await _enderecoService.Create(dto);
           
            return CreatedAtAction(nameof(Create), new { Id = readEnderecoDto.Id }, readEnderecoDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadEnderecoDto>>> GetAll() 
        {
            List<ReadEnderecoDto> enderecos = await _enderecoService.GetAll();
            return Ok(enderecos);
        }

        [HttpGet("{id}")]
        public ActionResult<ReadEnderecoDto> GetOneById(int id)
        {
            var result = _enderecoService.GetOneById(id);

            if (result.IsSuccess) 
            {
                var readEnderecoDto = result.Value;
                return Ok(readEnderecoDto);
            }

            return BadRequest(result.Errors[0].Message);
        }

        [HttpPut("{id}")]
        public ActionResult<ReadEnderecoDto> UpdateOne([FromBody] UpdateEnderecoDto dto, int id)
        {
            var result = _enderecoService.UpdateOne(dto, id);
            if(result.IsSuccess)
            {
                var readEnderecoDto = result.Value;
                return Ok(readEnderecoDto);
            }

            return BadRequest(result.Errors[0].Message);
        }
    }
}