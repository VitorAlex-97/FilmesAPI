using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class CinemaProfiles : Profile
    {
        public CinemaProfiles()
        {
            CreateMap<CreateCinemaDto, Cinema>();
            CreateMap<UpdateCinemaDto, Cinema>();
            CreateMap<UpdateCinemaDto, Cinema >();
            CreateMap<Cinema, ReadCinemaDto>();
            CreateMap<Cinema[], ReadCinemaDto[]>();
        }
    }
}