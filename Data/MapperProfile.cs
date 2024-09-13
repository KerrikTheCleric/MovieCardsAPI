using AutoMapper;
using MovieCardsAPI.Models.Dtos;
using MovieCardsAPI.Models.Entities;

namespace MovieCardsAPI.Data {
    public class MapperProfile : Profile {
        public MapperProfile() {
            CreateMap<Movie, MovieForCreationDTO>().ReverseMap();

            CreateMap<Movie, MovieForUpdateDTO>().ReverseMap();

            CreateMap<Movie, MovieDto>().ConstructUsing(src => new MovieDto(src.Id, src.Title, src.Rating, src.ReleaseDate, src.Description, src.Director.Name));

            CreateMap<Movie, MovieDtoExtra>().ConstructUsing(src => new MovieDtoExtra {
                Id = src.Id,
                Title = src.Title,
                Rating = src.Rating,
                ReleaseDate = src.ReleaseDate,
                Description = src.Description,
                DirectorName = src.Director.Name
            });


            CreateMap<Movie, MovieDetailsDTO>().ReverseMap();

            CreateMap<Director, DirectorDTO>().ConstructUsing(src => new DirectorDTO(src.Name, src.DateOfBirth, src.ContactInformation.Email, src.ContactInformation.PhoneNumber));

            CreateMap<Actor, ActorDTO>().ReverseMap();

            CreateMap<Genre, GenreDTO>().ReverseMap();
        }
    }
}
