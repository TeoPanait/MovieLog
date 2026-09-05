namespace MovieLog.DTOs;

public record TopRatedMovieDto
	(
	int MovieId,
	string Title,
	string? ImageUrl,
	double AverageRating,
	int ReviewCount
);