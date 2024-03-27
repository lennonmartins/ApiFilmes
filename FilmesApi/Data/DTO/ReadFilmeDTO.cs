using System.ComponentModel.DataAnnotations;
using FilmesApi.Models;

namespace FilmesApi.DTO;

public class ReadFilmeDTO
{
    public string Titulo { get; set; }

    public string Genero { get; set; }

    public int Duracao { get; set; }

    public DateTime DataDaConsulta { get; set; } = DateTime.Now;

    public ICollection<ReadSessaoDTO> SessoesDto { get; set; }
}