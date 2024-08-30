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

            //var res = await _context.Student.Include(s => s.Courses).ToListAsync();
            //var res2 = await _context.Student.Include(s => s.Enrollments).ThenInclude(e => e.Course).ToListAsync();
            //Not Address
            // return await _context.Student.ToListAsync();

            //Include Address
            //return await _context.Student.Include(s => s.Address).ToListAsync();

            //Transform to DTO, no need for include!
            // var dto = includeCourses ? "" : "";

            var dto = _context.Movies/*.Include(s => s.Address)*/.Select(s => new MovieDto(s.Id, s.Title, s.Rating, s.ReleaseDate, s. Description));
            return Ok(await dto.ToListAsync());


            //return await _context.Movies.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [SwaggerOperation(Summary = "Get a movie by id", Description = "Get a movie by id", OperationId = "GetMovieById")]
        [SwaggerResponse(StatusCodes.Status200OK, "The movie was found", Type = typeof(MovieDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The movie was not found")]
        public async Task<ActionResult<MovieDto>> GetMovie(long id) {

            //var r1 = await _context.Student.Include(s => s.Enrollments).ThenInclude(e => e.Course).ToListAsync();
            //var r2 = await _context.Course.Include(c => c.CourseBooks).ToListAsync();

            //var dto = await _context.Student
            //    .Where(s => s.Id == id)
            //    .Select(s => new StudentDetailsDto
            //    {
            //        Id = s.Id,
            //        AddressCity = s.Address.City,
            //        Avatar = s.Avatar,
            //        Courses = s.Enrollments.Select(e => new CourseDto(e.Course.Title, e.Grade)),
            //        FullName = s.FullName
            //    })
            //   .FirstOrDefaultAsync();


            var dto = await mapper.ProjectTo<MovieDto>(_context.Movies.Where(s => s.Id == id)).FirstOrDefaultAsync();

            //var dto = await _context.Student
            //.Where(s => s.Id == id)
            //.ProjectTo<StudentDetailsDto>(mapper.ConfigurationProvider)
            //.FirstOrDefaultAsync();

            if (dto == null) {
                return NotFound();
            }

            return Ok(dto);


            /*var movie = await _context.Movies.FindAsync(id);

            if (movie == null) {
                return NotFound();
            }

            return movie;*/
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(long id, Movie movie) {
            if (id != movie.Id) {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

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

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

           

            var movie = mapper.Map<Movie>(dto);

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var movieDTO = mapper.Map<MovieDto>(movie);

            return CreatedAtAction(nameof(GetMovie), new { id = movieDTO.Id }, movieDTO);


            /*_context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);*/
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
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
