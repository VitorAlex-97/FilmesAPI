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

        public Task<List<ReadEnderecoDto>>GetAll()
        {
            var enderecos = await _context.Enderecos.ToListAsync().Result;
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

        public async Task<Result<ReadEnderecoDto>> UpdateOne(UpdateEnderecoDto dto, int id)
        {
            var endereco = _mapper.Map<Endereco>(dto);
            var enderecoDb = await _context.Enderecos.FirstOrDefaultAsync(e => e.Id == id);

            if (enderecoDb == null)
            {
                return Result.Fail("Endereço não encontrado");
            }

            _mapper.Map(endereco, enderecoDb);
            await _context.SaveChangesAsync();

            return Result.Ok(_mapper.Map<ReadEnderecoDto>(enderecoDb));
        }
    }
}
