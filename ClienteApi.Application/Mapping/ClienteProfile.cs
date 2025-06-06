using AutoMapper;
using ClienteApi.Application.DTOs;
using ClienteApi.Domain.Entities;
using ClienteApi.Domain.ValueObjects;

namespace ClienteApi.Application.Mapping
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Endereco, EnderecoDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>()
                .ForMember( dest => dest.Email, opt => opt.MapFrom( src => src.Email.Value ) )
                .ReverseMap();

        }

    }
}
