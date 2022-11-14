using FilmesAPI.Data.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace FilmesAPI.Profiles
{
    public class GerenteProfiles : Profile
    {
        public GerenteProfiles()
        {
            CreateMap<CreateGerenteDto, Gerente>();
            CreateMap<Gerente, ReadGerenteDto>()
            .ForMember(
                gerente => gerente.Cinemas, opts => {
                    opts.MapFrom(
                        gerente => gerente.Cinemas.Select(
                            c => new { 
                                c.Id, 
                                c.Nome,
                                c.Endereco, 
                                c.EnderecoId
                            }
                        )
                    );
                }
            );
        }
    }
}