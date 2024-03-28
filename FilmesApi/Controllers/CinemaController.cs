using System.Collections;
using AutoMapper;
using FilmesApi.Data;
using FilmesApi.DTO;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CinemaController : ControllerBase
{
    public FilmeContext _context;
    public IMapper _mapper;

    public CinemaController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult Cadastrarcinema([FromBody] CreateCinemaDTO cinemaDto)
    {
        var cinema = _mapper.Map<Cinema>(cinemaDto);
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaCinemaPeloId), new { id = cinema.Id }, cinema);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaCinemaPeloId(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null) return NotFound();
        var cinemaDto = _mapper.Map<ReadCinemaDTO>(cinema);
        return Ok(cinemaDto);
    }

    [HttpGet]
    public IEnumerable<ReadCinemaDTO> RecuperaTodosOsCinemas()
    {
        var cinemas = _context.Cinemas.ToList();
        return _mapper.Map<List<ReadCinemaDTO>>(cinemas);
    }

    [HttpPut("{id}")]
    public IActionResult Atualizacinema(int id, [FromBody] UpdateCinemaDTO cinemaDto)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null) NotFound();
        _mapper.Map(cinema, cinemaDto);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaCinem(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null) return NotFound();
        _context.Remove(cinema);
        _context.SaveChanges();
        return NoContent();
    }
}