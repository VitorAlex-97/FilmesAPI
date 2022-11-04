using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
  public class FilmeProfiles : Profile
  {
    public FilmeProfiles()
    {
      CreateMap<CreateFilmeDto, Filme>();
      CreateMap<UpdateFilmeDto, Filme>();
      CreateMap<Filme, ReadFilmeDto>();
      CreateMap<Filme[], ReadFilmeDto[]>();
    }
  }
}