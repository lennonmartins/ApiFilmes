﻿using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;

public class Sessao
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    public int FilmeID { get; set; }
    
    public virtual Filme Filme { get; set; }

    public int CinemaId { get; set; }

    public virtual Cinema Cinema { get; set; }
}