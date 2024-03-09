using Microsoft.EntityFrameworkCore;
using movies.Models;

namespace movies.Services
{
    public class GenresService : IGenresService
    {
        private readonly ApplicationDbContext _context;

        public GenresService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Genre> CreateGenre(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();

            return genre;
        }

        public Genre DeleteGenre(Genre genre)
        {
            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return genre;
        }

        public async Task<IEnumerable<Genre>> GetALl()
        {
            return await _context.Genres.OrderBy(m => m.Name).ToListAsync();
        }

        public async Task<Genre> GetById(Byte id)
        {
            return await _context.Genres.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> isValidGenre(byte id)
        {
            return await _context.Genres.AnyAsync(x => x.Id == id);
            
        }

        public Genre UpdateGenre(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();

            return genre;
        }

       
    }
}
