﻿using System.ComponentModel.DataAnnotations;

namespace FilmesApi.DTO;

public class UpdateCinemaDTO
{
    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    public string Nome { get; set; }
}