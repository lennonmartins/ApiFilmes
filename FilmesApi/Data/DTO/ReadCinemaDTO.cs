namespace FilmesApi.DTO;

public class ReadCinemaDTO
{
    public int  Id { get; set; }
    public string Nome { get; set; }
    public ReadEnderecoDTO EnderecoDto { get; set; }
    public DateTime DataDaConsulta { get; set; } = DateTime.Now;
    public ICollection<ReadSessaoDTO> SessoesDto { get; set; }
}