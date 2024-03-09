using movies.Controllers;
using movies.Models;

namespace movies.Services
{
    public interface IMoviesService
    {
        //anything in controller need to use _context or database or applicationdbcontext we make a method in this class
        Task<IEnumerable<Movie>> GetAllMovies(byte genreId = 0);
        Task<Movie> GetMovieById(int id);
       // Task<Movie> GetMovieByGenreId();

        Task<Movie> Create(Movie movie);

        Movie Update(Movie movie);
        Movie DeleteMovie(Movie movie);

    }
}
