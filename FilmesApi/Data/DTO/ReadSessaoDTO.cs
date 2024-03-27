using FilmesApi.Models;

namespace FilmesApi.DTO;

public class ReadSessaoDTO
{
    public int Id { get; set; }
    public ReadFilmeDTO FilmeDto { get; set; }
}