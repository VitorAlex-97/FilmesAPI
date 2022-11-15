using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using FilmesAPI.Data.Dtos;

namespace FilmesAPI.Profiles
{
    public class SessaoProfiles : Profile
    {
        public SessaoProfiles()
        {
            CreateMap<CreateSessaoDto, Sessao>();
            CreateMap<Sessao, ReadSessaoDto>()
            .ForMember(
                dto => dto.HorarioInicio, opts => {
                    opts.MapFrom(
                        dto => dto.HorarioEnceramento.AddMinutes(dto.Filme.Duracao * (-1))
                    );
                }
            );
        }
    }
}