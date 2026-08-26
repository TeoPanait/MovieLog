using MovieLog.DTOs;
using MovieLog.Models;
using MovieLog.Repositories;

namespace MovieLog.Services;

public class GenreService : IGenreService
{
    private readonly IUnitOfWork _unitOfWork;

    public GenreService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<GenreDto>> GetAllGenresAsync(CancellationToken cancellationToken = default)
    {
        var genres = await _unitOfWork.GenreRepository.GetAllAsync(cancellationToken);
        return genres.Select(g => new GenreDto(g.Id, g.Name));
    }

    public async Task<GenreDto?> GetGenreByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(id, cancellationToken);
        if (genre == null) return null;
        return new GenreDto(genre.Id, genre.Name);
    }

    public async Task<GenreDto> CreateGenreAsync(CreateGenreDto dto, CancellationToken cancellationToken = default)
    {
        var genre = new Genre { Name = dto.Name };

        await _unitOfWork.GenreRepository.AddAsync(genre, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new GenreDto(genre.Id, genre.Name);
    }

    public async Task UpdateGenreAsync(int id, UpdateGenreDto dto, CancellationToken cancellationToken = default)
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(id, cancellationToken);
        if (genre == null) throw new KeyNotFoundException($"Genre with ID {id} not found");
        //suprascriem campurile cu val noi din dto
        genre.Name = dto.Name;
        _unitOfWork.GenreRepository.Update(genre);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteGenreAsync(int id, CancellationToken cancellationToken = default)
    {
        //aducem genre existent
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(id, cancellationToken);
        if (genre == null) throw new KeyNotFoundException($"Genre with ID {id} not found");
        //sttergem si salvam
        _unitOfWork.GenreRepository.Delete(genre);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }


}