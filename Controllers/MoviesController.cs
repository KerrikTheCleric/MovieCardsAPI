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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies() {
            //var test = _context.Movies.Include(m => m.Actors).Include(m => m.Genres).ToList();
            var dto = _context.Movies.Select(s => new MovieDto(s.Id, s.Title, s.Rating, s.ReleaseDate, s.Description, s.Director.Name));
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
            var dto = await mapper.ProjectTo<MovieDto>(_context.Movies.Where(s => s.Id == id)).FirstOrDefaultAsync();

            if (dto == null) {
                return NotFound();
            }
            return Ok(dto);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(long id, MovieForUpdateDTO dto) {

            if (id != dto.Id) {
                return BadRequest();
            }


            var movieFromDB = await _context.Movies.Include(s => s.Director).FirstOrDefaultAsync(s => s.Id == id);

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
        public async Task<ActionResult<MovieDto>> PostMovie(MovieForCreationDTO dto) {
            var movie = mapper.Map<Movie>(dto);

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var createdMovie = await _context.Movies.Include(m => m.Director).FirstOrDefaultAsync(m => m.Id ==movie.Id);
            //Mapper breaks here, meaning the movie gets created but not messaged back
            var movieDTO = mapper.Map<MovieDto>(createdMovie);

            return CreatedAtAction(nameof(GetMovie), new { id = movieDTO.Id }, movieDTO);
        }

        // DELETE: api/Movies/5
        /// <summary>
        /// Deleta a movie row from the database
        /// </summary>
        /// <param name="id">The id of the movie to delete</param>
        /// <returns>An HTTP 204 No Content response successfully deleted</returns>
        /// <response code="204">The movie was deleted</response>
        /// <response code="404">The movie was not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
