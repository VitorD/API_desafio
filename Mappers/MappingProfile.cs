using API_desafivo_v2.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_desafivo_v2.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioCreateUpdateDTO>();
            CreateMap<Usuario, UsuarioListDTO>();
        }
    }
}