using AutoMapper;
using FilmesApi.Data;
using FilmesApi.DTO;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    public FilmeContext _filmeContext;
    public IMapper _mapper;

    public FilmeController(FilmeContext filmeContext, IMapper mapper)
    {
        _filmeContext = filmeContext;
        _mapper = mapper;
    }

    
    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDTO filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);
        _filmeContext.Add(filme);
        _filmeContext.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmePeloId), new { id = filme.Id }, filme);
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes([FromQuery] int skip = 0, int take = 10)
    {
        return _filmeContext.Filmes.Skip(skip).Take(take).ToList();
    }

    [HttpGet("{id:int}")]
    public IActionResult RecuperaFilmePeloId(int id)
    {
        var filme = _filmeContext.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        var filmeDto = _mapper.Map<ReadFilmeDTO>(filme);
        return Ok(filmeDto);
    }
    
    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmeParcial(int id,
        JsonPatchDocument<UpdateFilmeDTO> patch)
    {
        var filme = _filmeContext.Filmes.FirstOrDefault(
            filme => filme.Id == id);
        if (filme == null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDTO>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(filmeParaAtualizar, filme);
        _filmeContext.SaveChanges();
        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDTO filmeDto)
    {
        var filme = _filmeContext.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        _mapper.Map(filmeDto, filme);
        _filmeContext.SaveChanges();
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeletaFilme(int id)
    {
        var filme = _filmeContext.Filmes.FirstOrDefault(
            filme => filme.Id == id);
        if (filme == null) return NotFound();
        _filmeContext.Remove(filme);
        _filmeContext.SaveChanges();
        return NoContent();
    }
}