using AutoMapper;
using Domain.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Services
{
    public class EnderecoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EnderecoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadEnderecoDto> Create(CreateEnderecoDto createEnderecoDto)
        {
            var endereco = _mapper.Map<Endereco>(createEnderecoDto);
            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReadEnderecoDto>(endereco);
        }

        public async Task<List<ReadEnderecoDto>> GetAll()
        {
            var enderecos = await _context.Enderecos.ToListAsync();
            return _mapper.Map<List<ReadEnderecoDto>>(enderecos);
        }

        public Result<ReadEnderecoDto> GetOneById(int id)
        {
            var endereco = _context.Enderecos.FirstOrDefault(e => e.Id == id);
            if (endereco == null)
            {
                return Result.Fail("Endereço não encontrado");
            }

            return Result.Ok(_mapper.Map<ReadEnderecoDto>(endereco));
        }

        public Result<ReadEnderecoDto> UpdateOne(UpdateEnderecoDto enderecoDto, int id)
        {
            var enderecoDb = _context.Enderecos.FirstOrDefault(e => e.Id == id);

            if (enderecoDb == null)
            {
                return Result.Fail("Endereço não encontrado");
            }

            _mapper.Map(enderecoDto, enderecoDb);
            _context.SaveChanges();

            var readEnderecoDto = _mapper.Map<ReadEnderecoDto>(enderecoDb);
            return Result.Ok(readEnderecoDto);
        }
    }
}
