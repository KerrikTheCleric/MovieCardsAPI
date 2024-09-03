using Bogus;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using MovieCardsAPI.Models.Entities;
using MovieCardsAPI.Models;
using Bogus.DataSets;

namespace MovieCardsAPI.Data {

    /*
     drop-database & update-database to clear out the database for fresh seeding.
     */
    internal class SeedData {

        private static Faker faker = new Faker("sv");
        internal static async Task InitAsync(MovieContext context) {
            if (await context.Movies.AnyAsync()) return;

            var directors = GenerateDirectors(100);
            await context.AddRangeAsync(directors);

            var actors = GenerateActors(100);
            await context.AddRangeAsync(actors);

            string[] genreList = ["Action", "Comedy", "Drama", "Fantasy", "Horror", "Mystery", "Romance", "Thriller", "Western", "Horror", "Science Fiction", "Apocalypse", "Family", "Martial Arts", "Sports",];
            var genres = GenerateGenres(genreList);
            await context.AddRangeAsync(genres);

            var movies = GenerateMovies(500, directors, actors, genres);
            await context.AddRangeAsync(movies);

            await context.SaveChangesAsync();
        }

        private static IEnumerable<Director> GenerateDirectors(int generationAmount) {
            var faker = new Faker<Director>("sv").Rules((faker, director) => {
                director.Name = faker.Name.FullName();
                director.DateOfBirth = faker.Date.Past(50, DateTime.Now);
                director.ContactInformation = new ContactInformation {
                    Email = faker.Internet.Email(),
                    PhoneNumber = faker.Phone.PhoneNumber(),
                };
            });
            return faker.Generate(generationAmount);
        }

        private static IEnumerable<Actor> GenerateActors(int generationAmount) {
            var faker = new Faker<Actor>("sv").Rules((faker, actor) => {
                actor.Name = faker.Name.FullName();
                actor.DateOfBirth = faker.Date.Past(50, DateTime.Now);
            });
            return faker.Generate(generationAmount);
        }

        private static IEnumerable<Genre> GenerateGenres(string[] genreList) {
            List<Genre> genres = new List<Genre>();
            foreach (string genre in genreList) {
                genres.Add(new Genre {
                    Name = genre
                }); ;
            }
            return genres;
        }

        private static IEnumerable<Movie> GenerateMovies(int generationAmount, IEnumerable<Director> directorList, IEnumerable<Actor> actorList, IEnumerable<Genre> genreList) {
            var movies = new List<Movie>(generationAmount);
            var random = new Random();

            for (int i = 0; i < generationAmount; i++) {
                var title = faker.Hacker.Verb() + " " + faker.Hacker.Adjective() + " " + faker.Hacker.Noun();
                var rating = random.Next(0, 10);
                var releaseDate = faker.Date.Past(50, DateTime.Now);
                var description = faker.Lorem.Sentences(3);
                var director = faker.PickRandom<Director>(directorList);
                var actors = actorList.OrderBy(x => random.Next()).Take(3);
                var genres = genreList.OrderBy(x => random.Next()).Take(2);

                var movie = new Movie() {
                    Title = title,
                    Rating = (short)rating,
                    ReleaseDate = releaseDate,
                    Description = description,
                    Director = director,
                    Actors = actors.ToList(),

                    Genres = genres.ToList()
                };

                movies.Add(movie);

            }
            return movies;
        }
    }
}
