using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movies.Dtos;
using movies.Models;
using movies.Services;
using System;

namespace movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        ///instance from IGenresService _genresService to communicate with database (appdbcontext);
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            //select all genres  from database
            //_context acts like database
            //genres is the table
            var genres = await _genresService.GetALl();

            return Ok(genres);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(byte id)
        {
            var gener = await _genresService.GetById(id);
            if(gener == null) { return NotFound($"NO genre was found with ID:{id}"); }
            return Ok(gener);
        }
        

        [HttpPost]
        public async Task<IActionResult> CreatAsync(CreatGenresDto dto)
        {

            var genre = new Genre { Name = dto.Name };

            await _genresService.CreateGenre(genre);
            

            return Ok(genre);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateData(byte id, [FromBody] CreatGenresDto dto)
        {
            var genre = await _genresService.GetById(id);

            if (genre == null) { return NotFound($"NO genre was found with ID:{id}"); }

            genre.Name = dto.Name;
           _genresService.UpdateGenre(genre);

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _genresService.GetById(id);

            if (genre == null) { return NotFound($"NO genre was found with ID:{id}"); }
            _genresService.DeleteGenre(genre);

            return Ok(genre);
        }
    }
}
