﻿using AutoMapper;
using FilmesApi.Data;
using FilmesApi.DTO;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SessaoController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public SessaoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaSessao(CreateSessaoDTO dto)
    {
        var sessao = _mapper.Map<Sessao>(dto);
        _context.Sessoes.Add(sessao);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaSessoesPorId), new { filmeId = sessao.FilmeId, cinemaId = sessao.CinemaId}, sessao);
    }

    [HttpGet]
    public IEnumerable<ReadSessaoDTO> RecuperaSessoes()
    {
        return _mapper.Map<List<ReadSessaoDTO>>(_context.Sessoes.ToList());
    }

    [HttpGet("{filmeId}/{cinemaId}")]
    public IActionResult RecuperaSessoesPorId(int filmeId, int cinemaId)
    {
        var sessao = _context.Sessoes
            .FirstOrDefault
            (sessao => sessao.FilmeId == filmeId &&
                       sessao.CinemaId == cinemaId);
        if (sessao != null)
        {
            ReadSessaoDTO sessaoDto = _mapper.Map<ReadSessaoDTO>(sessao);

            return Ok(sessaoDto);
        }

        return NotFound();
    }
}