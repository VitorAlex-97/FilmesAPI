using AutoMapper;
using Domain.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Services
{
    public class CinemaService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public CinemaService(IMapper mapper, AppDbContext context) 
        { 
            _context = context;
            _mapper = mapper;    
        }

        public List<Cinema> GetAll()
        {
            return _context.Cinemas.ToList();
        }

        public ReadCinemaDto GetOneById(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(c => c.Id == id);
            if (cinema == null)
            {
                return null;
            }

            return _mapper.Map<ReadCinemaDto>(cinema);

        }

        public Result<ReadCinemaDto> Create(CreateCinemaDto createCinemaDto)
        {
            var cinema = _mapper.Map<Cinema>(createCinemaDto);
            var enderecoCinema = _context.Cinemas.FirstOrDefault(c => c.EnderecoId == createCinemaDto.EnderecoId);
            if (enderecoCinema == null)
            {
                var endereco = _context.Enderecos.FirstOrDefault(e => e.Id == createCinemaDto.EnderecoId);
                if (endereco == null) 
                {
                    return Result.Fail("Endereço não existe!");        
                }
                _context.Cinemas.Add(cinema);
                _context.SaveChanges();
                return _mapper.Map<ReadCinemaDto>(cinema);
            }
            return Result.Fail("Já existe um cinema no endereço informado!");
        }

        public Result UpdateById(int id, UpdateCinemaDto dto)
        {
            var cinema = _mapper.Map<Cinema>(dto);
            var cinemaDb = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinemaDb == null)
            {
                return Result.Fail("Cinema não encontrado");
            }

            _mapper.Map(cinema, cinemaDb);          
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result DeleteOneById(int id)
        {
            var cinemaDb = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinemaDb == null)
            {
                return Result.Fail("Cinema não enctrado");
            }

            _context.Cinemas.Remove(cinemaDb);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
