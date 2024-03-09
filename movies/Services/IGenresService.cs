using movies.Models;

namespace movies.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetALl();
        Task<Genre> CreateGenre(Genre genre);
        Task<Genre> GetById(Byte id);
        Genre UpdateGenre(Genre genre);

        Genre DeleteGenre(Genre genre);

        Task<bool> isValidGenre(Byte id);

    }
}
