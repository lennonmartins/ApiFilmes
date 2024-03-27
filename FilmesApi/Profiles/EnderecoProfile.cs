using AutoMapper;
using FilmesApi.DTO;
using FilmesApi.Models;

namespace FilmesApi.Profiles;

public class EnderecoProfile : Profile
{
    public EnderecoProfile()
    {
        CreateMap<CreateEnderecoDTO, Endereco>();
        CreateMap<UpdateFilmeDTO, Endereco>();
        CreateMap<Endereco, ReadCinemaDTO>();
    }
}