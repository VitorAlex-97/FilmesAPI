using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using FilmesAPI.Data.Dtos;

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