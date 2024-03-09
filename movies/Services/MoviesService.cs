using Microsoft.EntityFrameworkCore;
using movies.Dtos;
using movies.Filters;
using movies.Models;

namespace movies.Services
{
    
    public class MoviesService : IMoviesService
    {
        
        private readonly ApplicationDbContext _context;

        public MoviesService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Movie> Create(Movie movie)
        {
            await _context.Movies.AddAsync(movie);

            _context.SaveChanges();
            return movie;
        }

        public Movie DeleteMovie(Movie movie)
        {
            _context.Movies.Remove(movie);

            _context.SaveChanges();

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAllMovies(byte genreId = 0)
        {
           return await _context.Movies
                .Where(m=>m.GenreId == genreId|| genreId==0)
                .Include(m => m.Genre)
                .OrderByDescending(x => x.Rate)
                .ToListAsync();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(x => x.Id == id);
        }

        public Movie Update(Movie movie)
        {
            _context.Movies.Update(movie);
        
            _context.SaveChanges();

            return movie;
        }

    }
}
