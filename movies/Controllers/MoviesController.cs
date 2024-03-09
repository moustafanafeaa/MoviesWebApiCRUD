using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movies.Dtos;
using movies.Filters;
using movies.Models;
using movies.Services;

namespace movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [LogSensitiveAction]
	public class MoviesController : ControllerBase
    {
        //inject
        private readonly IMapper _mapper;
        private readonly IMoviesService _moviesService;
        private readonly IGenresService _genresService;
        private readonly ILogger<MoviesController> _logger;


        public MoviesController(IMoviesService moviesService, IGenresService genresService, ILogger<MoviesController> logger, IMapper mapper)
        {
            _moviesService = moviesService;
            _genresService = genresService;
            _logger = logger;
            _mapper = mapper;
        }

        public new List<string> _validExtentions = new List<string> { ".jpg", ".png" };
        public long _maxsize = 1024 * 1024;

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {

            var movies = await _moviesService.GetAllMovies();

            var data = _mapper.Map<IEnumerable<MoviesDetailsDto>>(movies);
            return Ok(data);
        }





        [HttpGet("{id}",Name = "MovieDetailsRoute")]//"{name}:alpha"

        //(Model binder)==>
        //1-take data from user (client side)
        //data will be |
        //2-primitive(int,double,float,string) =>in route (url) parameter occr querystring
        //3-complex => in req body
        public async Task<IActionResult> GetMovie(int id)
        {
            _logger.LogDebug("Getting movie with #{id}", id);

			var movie = await _moviesService.GetMovieById(id);

            if(movie == null)
            {
                return NotFound("movie not found");
            }

            //MoviesDetailsDto ==> to include genre name not in object
            var dto = _mapper.Map<MoviesDetailsDto>(movie);

            return Ok(dto);
        }

        [HttpGet("ByGenreId")]
        public async Task<IActionResult> GetAllMoviesByGenreId(byte id)
        {

            var movie = await _genresService.GetById(id);

            if (movie == null)
            {
                return NotFound("movie not found");
            }
            var dto = _mapper.Map<MoviesDetailsDto>(movie);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]MoviesDto dto)
        {

            //if (ModelState.IsValid) { } modelstate=> moviesdto 
            if(dto.Poster == null)
            {
                return BadRequest("poster is reqqq");
            }
            if (!_validExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("the valid Extentions is jpg and png only");
            if(dto.Poster.Length> _maxsize)
                return BadRequest("the size must be less than 1MB");

            var isValidGenre = await _genresService.isValidGenre(dto.GenreId); 
            if (!isValidGenre)
                return BadRequest($"it is no genre with ID:{dto.GenreId}");


            using var datastream = new MemoryStream();
            await dto.Poster.CopyToAsync(datastream);
            var movie = _mapper.Map<Movie>(dto);
            movie.Poster = datastream.ToArray();

            await _moviesService.Create(movie);

            
            string url = Url.Link("MovieDetailsRoute", new {id = movie.Id});

            //              response header (location:)                  , response body
            //this ""https://localhost:7162/api/movies/"" is static!!
            // 
            //return Created(url, movie); 
             return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MoviesDto dto)
        {

            var movie = await _moviesService.GetMovieById(id);
            if (movie == null)
                return NotFound("not found movie with this id");



            var isValidGenre = await _genresService.isValidGenre(dto.GenreId);
            if (!isValidGenre)
                return BadRequest($"it is no genre with ID:{dto.GenreId}");


            if (dto.Poster != null)
            {
                if (!_validExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("the valid Extentions is jpg and png only");
                if (dto.Poster.Length > _maxsize)
                    return BadRequest("the size must be less than 1MB");

                using var datastream = new MemoryStream();
                await dto.Poster.CopyToAsync(datastream);

                movie.Poster = datastream.ToArray();
            }


            movie.Title = dto.Title;
            movie.StoreLine = dto.StoreLine;
            movie.Rate = dto.Rate;
            movie.GenreId = dto.GenreId;
            movie.year = dto.year;


            _moviesService.Update(movie);
            return Ok(movie);
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _moviesService.GetMovieById(id);

            if (movie == null)
                return NotFound("not movie with this id");

            _moviesService.DeleteMovie(movie);

            return Ok(movie);
        }


        
    }


}
