using AutoMapper;
using API_desafio.Models;

namespace API_desafio.Mappers
{
    public class AutomapperConfig
    {
        public static IMapper RegisterMappings()
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





