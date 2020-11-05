using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using API_desafivo_v2.Models;

namespace API_desafivo_v2.Mappers
{
    public class AutomapperConfig
    {
        public static IMapper  RegisterMappings()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Usuario, UsuarioListDTO>();
                cfg.CreateMap<Usuario, UsuarioCreateUpdateDTO>();
                cfg.CreateMap<UsuarioCreateUpdateDTO, Usuario>().ForMember(dest => dest.Ativo, opt => opt.Ignore());

            });

            IMapper _mapper = configuration.CreateMapper();
            return _mapper;  
        }

        public static IConfigurationProvider Criar_Mapeamento()
        {

             MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Usuario, UsuarioListDTO>();
                cfg.CreateMap<Usuario, UsuarioCreateUpdateDTO>();
            });
            return config;
        }


    }
}