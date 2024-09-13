﻿using MovieCardsAPI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MovieCardsAPI.Models.Dtos {
    public record MovieDto(
        long Id,
        string Title,
        short Rating,
        DateTime ReleaseDate,
        string Description,
        string DirectorName
        );

    public class MovieDtoExtra {

        public MovieDtoExtra() { }

        public long Id { get; set; }
        public string Title { get; set; }
        public short Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string DirectorName { get; set; }
        public IEnumerable<ActorDTO> Actors { get; set; }
    };

    public class MovieDetailsDTO {
        public long Id { get; set; }
        public string Title { get; set; }
        public short Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public DirectorDTO Director { get; set; }
        public IEnumerable<ActorDTO> Actors { get; set; }
        public IEnumerable<GenreDTO> Genres { get; set; }
    }
    public record MovieForCreationDTO(
        string Title,
        [Required, Range(0, 10)]
        short Rating,
        DateTime ReleaseDate,
        string Description,
        long DirectorId
        );

    public record MovieForUpdateDTO(
        long Id,
        string Title,
        [Required, Range(0, 10)]
        short Rating,
        DateTime ReleaseDate,
        string Description,
        long DirectorId
        );

    public record DirectorDTO(string Name, DateTime DateOfBirth, string Email, string PhoneNumber);

    public record ActorDTO(string Name, DateTime DateOfBirth);

    public record GenreDTO(string Name);

}
