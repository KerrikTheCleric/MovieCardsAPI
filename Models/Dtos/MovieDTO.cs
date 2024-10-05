using MovieCardsAPI.Models.Entities;
using MovieCardsAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace MovieCardsAPI.Models.Dtos {
    public record MovieDto(
        long Id,
        string Title,
        short Rating,
        DateOnly ReleaseDate,
        string Description,
        string DirectorName
        );

    public class MovieDtoExtra {

        public MovieDtoExtra() { }

        public long Id { get; set; }
        public string Title { get; set; }
        public short Rating { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Description { get; set; }
        public string DirectorName { get; set; }
        public IEnumerable<ActorDTO> Actors { get; set; }
    };

    public class MovieDetailsDTO {
        public long Id { get; set; }
        public string Title { get; set; }
        public short Rating { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Description { get; set; }
        public DirectorDTO Director { get; set; }
        public IEnumerable<ActorDTO> Actors { get; set; }
        public IEnumerable<GenreDTO> Genres { get; set; }
    }
    public record MovieForCreationDTO(
        [Required, UniqueMovieTitle]
        string Title,
        [Required, Range(0, 10)]
        short Rating,
        DateOnly ReleaseDate,
        string Description,
        long DirectorId
        );

    public record MovieForUpdateDTO(
        long Id,
        [Required, UniqueMovieTitle]
        string Title,
        [Required, Range(0, 10)]
        short Rating,
        DateOnly ReleaseDate,
        string Description,
        long DirectorId
        );

    public record DirectorDTO(string Name, DateOnly DateOfBirth, string Email, string PhoneNumber);

    public record ActorDTO(string Name, DateOnly DateOfBirth);

    public record GenreDTO(string Name);

}
