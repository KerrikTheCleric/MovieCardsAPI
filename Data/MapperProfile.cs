using AutoMapper;
using MovieCardsAPI.Models.Dtos;
using MovieCardsAPI.Models.Entities;


namespace MovieCardsAPI.Data {
    public class MapperProfile : Profile {
        public MapperProfile() {
            CreateMap<Movie, MovieForCreationDTO>().ReverseMap();

            CreateMap<Movie, MovieForUpdateDTO>().ReverseMap();

            CreateMap<Movie, MovieDto>()
                .ConstructUsing(src => new MovieDto(src.Id, src.Title, src.Rating, src.ReleaseDate, src.Description));


            CreateMap<Movie, MovieDetailsDTO>().ReverseMap();
        }
    }
}
