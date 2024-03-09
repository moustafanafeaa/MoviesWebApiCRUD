using AutoMapper;
using movies.Dtos;
using movies.Models;

namespace movies.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MoviesDetailsDto>();
            CreateMap<MoviesDto, Movie>().ForMember(src => src.Poster, options => options.Ignore());

        }
    }
}
