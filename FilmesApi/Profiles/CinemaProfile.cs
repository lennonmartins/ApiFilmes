using AutoMapper;
using FilmesApi.DTO;
using FilmesApi.Models;

namespace FilmesApi.Profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<CreateCinemaDTO, Cinema>();
        CreateMap<UpdateFilmeDTO, Cinema>();
        CreateMap<Cinema, ReadCinemaDTO>()
            .ForMember(cinemaDto => cinemaDto.EnderecoDto,
                opt => opt.MapFrom(cinema => cinema.Endereco))
            .ForMember(cinemaDto => cinemaDto.SessoesDto,
                opt => opt.MapFrom(cinema => cinema.Sessoes));
    }
}