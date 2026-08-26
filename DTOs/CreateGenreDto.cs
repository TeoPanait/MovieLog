using System.ComponentModel.DataAnnotations;

namespace MovieLog.DTOs;

public record CreateGenreDto
(
    [Required] string Name    
);

public record UpdateGenreDto
(
    [Required] string Name
);
