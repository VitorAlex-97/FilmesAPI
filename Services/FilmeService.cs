using AutoMapper;
using Domain.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Services
{
    public class FilmeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FilmeService(AppDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public ReadFilmeDto Create(CreateFilmeDto createFilmeDto)
        {
            Filme filme = _mapper.Map<Filme>(createFilmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();

            return _mapper.Map<ReadFilmeDto>(filme);
        }

        public IEnumerable<ReadFilmeDto> GetAll(int? classificacaoEtaria)
        {
            IEnumerable<Filme> filmes = new List<Filme>();

            filmes = classificacaoEtaria == null
                ? _context.Filmes.ToList()
                : _context.Filmes
                            .Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria)
                            .ToList();

            filmes ??= Enumerable.Empty<Filme>();

            return _mapper.Map<List<ReadFilmeDto>>(filmes);
        }

        public ReadFilmeDto GetOneById(int id)
        {
            var filmeDb = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filmeDb != null)
            {
                return _mapper.Map<ReadFilmeDto>(filmeDb);
            }

            return null;
       
        }

        public Result UpdateOne(UpdateFilmeDto updateFilmeDto, int id)
        {
            var filmeDb = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filmeDb == null)
            {
                return Result.Fail("Filme não encontrado");
            }
            _mapper.Map(updateFilmeDto, filmeDb);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result DeleteOne(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return Result.Fail("Filme não encontrado");
            }
            _context.Filmes.Remove(filme);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
