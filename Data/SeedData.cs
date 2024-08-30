using Bogus;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using MovieCardsAPI.Models.Entities;
using MovieCardsAPI.Models;
using Bogus.DataSets;

namespace MovieCardsAPI.Data {
    internal class SeedData {

        private static Faker faker = new Faker("sv");
        internal static async Task InitAsync(MovieContext context) {
            if (await context.Movies.AnyAsync()) return;

            var directors = GenerateDirectors(100);
            await context.AddRangeAsync(directors);

            //var contactInfo = GenerateContactInfo(100, directors);
            //await context.AddRangeAsync(contactInfo);

            /*var students = GenerateStudents(100);
            await context.AddRangeAsync(students);*/

            /*var courses = GenerateCourses(20);
            await context.AddRangeAsync(courses);

            var enrollments = GenerateEnrollments(students, courses);
            await context.AddRangeAsync(enrollments);*/

            await context.SaveChangesAsync();

        }

        private static IEnumerable<Director> GenerateDirectors(int generationAmount) {
            var faker = new Faker<Director>("sv").Rules((faker, director) => {
                director.Name = faker.Name.FullName();
                director.DateOfBirth = faker.Date.Past(25, DateTime.Now);
                director.ContactInformation = new ContactInformation {
                    Email = faker.Internet.Email(),
                    PhoneNumber = faker.Phone.PhoneNumber(),
                };
            });
            return faker.Generate(generationAmount);
        }

        /*private static IEnumerable<ContactInformation> GenerateContactInfo(int generationAmount, IEnumerable<Director> directors) {

            foreach (var director in directors) {

            }
            
            var faker = new Faker<ContactInformation>("sv").Rules((faker, contactInfo) => {
                contactInfo.Email = faker.Internet.Email();
                contactInfo.PhoneNumber = faker.Phone.PhoneNumber();
                contactInfo.Director = new Author { Name = faker.Name.FullName() };
            });
            return faker.Generate(numberOfBooks);
        }*/

        /*private static IEnumerable<Enrollment> GenerateEnrollments(IEnumerable<Student> students, IEnumerable<Course> courses) {
            var rnd = new Random();

            var enrollments = new List<Enrollment>();

            foreach (var student in students) {
                foreach (var course in courses) {

                    if (rnd.Next(0, 5) == 0) {
                        var enrollment = new Enrollment {
                            Student = student,
                            Grade = rnd.Next(1, 6),
                            Course = course
                        };

                        enrollments.Add(enrollment);
                    }

                }
            }
            return enrollments;
        }*/

        /*private static List<Course> GenerateCourses(int numberOfCourses) {

            var books = GenerateBooks(20);

            var courses = new List<Course>();

            for (int i = 0; i < numberOfCourses; i++) {

                var rndBooks = faker.Random.ListItems(books, faker.Random.Number(1, books.Count));

                var title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Company.Bs());
                var course = new Course { Title = title, CourseBooks = books };
                courses.Add(course);
            }
            return courses;
        }*/

        /*private static List<Book> GenerateBooks(int numberOfBooks) {
            var faker = new Faker<Book>("sv").Rules((faker, book) => {
                book.Title = faker.Company.CatchPhrase();
                book.NrOfPages = faker.Random.Int(45, 2000);
                book.Author = new Author { Name = faker.Name.FullName() };
            });

            return faker.Generate(numberOfBooks);
        }*/

        /*private static IEnumerable<Student> GenerateStudents(int numnerOfStudents) {
            var students = new List<Student>(numnerOfStudents);

            for (int i = 0; i < numnerOfStudents; i++) {
                var fName = faker.Name.FirstName();
                var lName = faker.Name.LastName();
                var avatar = faker.Internet.Avatar();

                var student = new Student() {
                    FirstName = fName,
                    LastName = lName,
                    Avatar = avatar,
                    Address = new Address {
                        City = faker.Address.City(),
                        Street = faker.Address.StreetName(),
                        ZipCode = faker.Address.ZipCode()
                    },
                };

                students.Add(student);

            }
            return students;
        }*/
    }
}
