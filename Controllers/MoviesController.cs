using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using MovieCardsAPI.Models;
using MovieCardsAPI.Models.Dtos;
using MovieCardsAPI.Models.Entities;
using Humanizer;
using System.Net;

namespace MovieCardsAPI.Controllers {
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase {
        private readonly MovieContext _context;
        private readonly IMapper mapper;

        public MoviesController(MovieContext context, IMapper mapper) {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Movies
        /* [HttpGet]
         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDto))]
         [ProducesResponseType(StatusCodes.Status404NotFound)]

         [SwaggerOperation(Summary = "Get all movies by search", Description = "Get all available movies by search", OperationId = "GetMoviesSearch")]
         [SwaggerResponse(StatusCodes.Status200OK, "Movies found", Type = typeof(MovieDto))]
         [SwaggerResponse(StatusCodes.Status404NotFound, "No movies found")]
         public async Task<ActionResult<IEnumerable<MovieDto>>> GetMoviesSearch([FromQuery] string? title = "", string? genre = "") {

             //var test = _context.Movies.Include(m => m.Actors).Include(m => m.Genres).ToList();
             var dto = _context.Movies.Select(m => new MovieDto(m.Id, m.Title, m.Rating, m.ReleaseDate, m.Description, m.Director.Name));


             if (dto == null) {
                 return NotFound();
             }

             return Ok(await dto.ToListAsync());
         }*/

        // GET: api/Movies/
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDtoExtra))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [SwaggerOperation(Summary = "Get all movies by search", Description = "Get all available movies by search", OperationId = "GetMoviesSearch")]
        [SwaggerResponse(StatusCodes.Status200OK, "Movies found", Type = typeof(MovieDtoExtra))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No movies found")]
        public async Task<ActionResult<IEnumerable<MovieDtoExtra>>> GetMoviesSearch([FromQuery] bool includeActors = false, string? searchTitle = "", string? searchGenre = "") {

            IQueryable<MovieDtoExtra> dto = null;

            if (searchTitle != "" && searchGenre != "") {

                if (includeActors) {
                    dto = _context.Movies.Where(m => m.Title.Contains(searchTitle) && m.Genres.Any(g => searchGenre.Contains(g.Name))).
                   Select(m => new MovieDtoExtra {
                       Id = m.Id,
                       Title = m.Title,
                       Rating = m.Rating,
                       ReleaseDate = m.ReleaseDate,
                       Description = m.Description,
                       DirectorName = m.Director.Name,
                       Actors = m.Actors.Select(a => new ActorDTO(a.Name, a.DateOfBirth))
                   });
                } else {
                    dto = _context.Movies.Where(m => m.Title.Contains(searchTitle) && m.Genres.Any(g => searchGenre.Contains(g.Name))).
                   Select(m => new MovieDtoExtra {
                       Id = m.Id,
                       Title = m.Title,
                       Rating = m.Rating,
                       ReleaseDate = m.ReleaseDate,
                       Description = m.Description,
                       DirectorName = m.Director.Name
                   });
                }
            } else if (searchTitle != "" && searchGenre == "") {

                if (includeActors) {
                    dto = _context.Movies.Where(m => m.Title.Contains(searchTitle)).
                   Select(m => new MovieDtoExtra {
                       Id = m.Id,
                       Title = m.Title,
                       Rating = m.Rating,
                       ReleaseDate = m.ReleaseDate,
                       Description = m.Description,
                       DirectorName = m.Director.Name,
                       Actors = m.Actors.Select(a => new ActorDTO(a.Name, a.DateOfBirth))

                   });
                } else {
                    dto = _context.Movies.Where(m => m.Title.Contains(searchTitle)).
                   Select(m => new MovieDtoExtra {
                       Id = m.Id,
                       Title = m.Title,
                       Rating = m.Rating,
                       ReleaseDate = m.ReleaseDate,
                       Description = m.Description,
                       DirectorName = m.Director.Name,
                   });
                }

            } else if (searchTitle == "" && searchGenre != "") {

                if (includeActors) {
                    dto = _context.Movies.Where(m => m.Genres.Any(g => searchGenre.Contains(g.Name))).
                   Select(m => new MovieDtoExtra {
                       Id = m.Id,
                       Title = m.Title,
                       Rating = m.Rating,
                       ReleaseDate = m.ReleaseDate,
                       Description = m.Description,
                       DirectorName = m.Director.Name,
                       Actors = m.Actors.Select(a => new ActorDTO(a.Name, a.DateOfBirth))
                   });
                } else {
                    dto = _context.Movies.Where(m => m.Genres.Any(g => searchGenre.Contains(g.Name))).
                   Select(m => new MovieDtoExtra {
                       Id = m.Id,
                       Title = m.Title,
                       Rating = m.Rating,
                       ReleaseDate = m.ReleaseDate,
                       Description = m.Description,
                       DirectorName = m.Director.Name
                   });
                }
            } else {

                if (includeActors) {
                    dto = _context.Movies.
                   Select(m => new MovieDtoExtra {
                       Id = m.Id,
                       Title = m.Title,
                       Rating = m.Rating,
                       ReleaseDate = m.ReleaseDate,
                       Description = m.Description,
                       DirectorName = m.Director.Name,
                       Actors = m.Actors.Select(a => new ActorDTO(a.Name, a.DateOfBirth))
                   });
                } else {
                    dto = _context.Movies.
                    Select(m => new MovieDtoExtra {
                        Id = m.Id,
                        Title = m.Title,
                        Rating = m.Rating,
                        ReleaseDate = m.ReleaseDate,
                        Description = m.Description,
                        DirectorName = m.Director.Name
                    });
                }
            }

            if (dto == null) {
                return NotFound();
            }

            return Ok(await dto.ToListAsync());
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [SwaggerOperation(Summary = "Get a movie by id", Description = "Get a movie by id", OperationId = "GetMovieById")]
        [SwaggerResponse(StatusCodes.Status200OK, "The movie was found", Type = typeof(MovieDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The movie was not found")]
        public async Task<ActionResult<MovieDto>> GetMovie(long id) {
            var dto = await mapper.ProjectTo<MovieDto>(_context.Movies.Where(m => m.Id == id)).FirstOrDefaultAsync();

            if (dto == null) {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpGet("{id}/details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDetailsDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [SwaggerOperation(Summary = "Get a detailed movie by id", Description = "Get a detailed movie by id", OperationId = "GetDetailedMovieById")]
        [SwaggerResponse(StatusCodes.Status200OK, "The movie was found", Type = typeof(MovieDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The movie was not found")]
        public async Task<ActionResult<MovieDetailsDTO>> GetMovieDetails(long id) {
            var dto = await mapper.ProjectTo<MovieDetailsDTO>(_context.Movies.Where(m => m.Id == id)).FirstOrDefaultAsync();
            //var dto = await mapper.ProjectTo<MovieDetailsDTO>(_context.Movies.Where(m => m.Id == id).Include(m => m.Director.ContactInformation)).FirstOrDefaultAsync();
            
            /*var dto = await _context.Movies
                .Where(m => m.Id == id)
                .Select(m => new MovieDetailsDTO {
                    Id = m.Id,
                    Title = m.Title,
                    Rating = m.Rating,
                    ReleaseDate = m.ReleaseDate,
                    Description = m.Description,
                    Director = new DirectorDTO(m.Director.Name, m.Director.DateOfBirth, m.Director.ContactInformation.Email, m.Director.ContactInformation.PhoneNumber),
                    Actors = m.Actors.Select(a => new ActorDTO(a.Name, a.DateOfBirth)),
                    Genres = m.Genres.Select(g => new GenreDTO(g.Name))
                }).FirstOrDefaultAsync();*/

            if (dto == null) {
                return NotFound();
            }
            return Ok(dto);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [SwaggerOperation(Summary = "Edit a movie by id", Description = "Edit a movie by id", OperationId = "EditMovieById")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Movie edited successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The movie was not found")]

        public async Task<IActionResult> PutMovie(long id, MovieForUpdateDTO dto) {

            if (id != dto.Id || !ModelState.IsValid) {
                return BadRequest();
            }

            var movieFromDB = await _context.Movies.Include(m => m.Director).FirstOrDefaultAsync(m => m.Id == id);

            if ((movieFromDB is null)) {
                return NotFound();
            }

            mapper.Map(dto, movieFromDB);

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!MovieExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [SwaggerOperation(Summary = "Create a new movie", Description = "Create a new movie", OperationId = "CreateMovie")]
        [SwaggerResponse(StatusCodes.Status201Created, "Movie created successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        public async Task<ActionResult<MovieDto>> PostMovie(MovieForCreationDTO dto) {
            var movie = mapper.Map<Movie>(dto);

            if (!ModelState.IsValid) {
                return BadRequest();
            }

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var createdMovie = await _context.Movies.Include(m => m.Director).FirstOrDefaultAsync(m => m.Id == movie.Id);
            var movieDTO = mapper.Map<MovieDto>(createdMovie);

            return CreatedAtAction(nameof(GetMovie), new { id = movieDTO.Id }, movieDTO);
        }

        // DELETE: api/Movies/5
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [SwaggerOperation(Summary = "Delete a movie by id", Description = "Delete a movie by id", OperationId = "DeleteMovie")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Movie deleted successfully")]
        public async Task<IActionResult> DeleteMovie(long id) {

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(long id) {
            return _context.Movies.Any(m => m.Id == id);
        }
    }
}
