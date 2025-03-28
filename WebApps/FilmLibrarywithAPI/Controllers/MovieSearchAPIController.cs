using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Homework4.Services;
using Homework4.Models;

namespace Homework4.Controllers
{
    [ApiController]
    [Route("api/search")] 
    public class TmdbApiController : Controller
    {
        private readonly ITmdbService _TmdbService; 
        private readonly ILogger<TmdbApiController> _logger; 
        public TmdbApiController(ITmdbService TmdbService, ILogger<TmdbApiController> logger)
        {
            _logger = logger; 
            _TmdbService = TmdbService; 
        }

        // Endpoint to search for movies based on a query string
        [HttpGet("movies")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MoviesReturn>))] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public async Task<ActionResult<List<MoviesReturn>>> GetMovies(string query)
        {
            // Validate the query string to ensure it's not empty
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Query cannot be empty."); // Return a 400 Bad Request response
            }

            try
            {
                // Use the TMDB service to search for movies
                var movies = await _TmdbService.Search(query);

                // If no movies are found, return an empty list with a 200 OK response
                if (movies == null || !movies.Any())
                {
                    return Ok(new List<MoviesReturn>());
                }

                // Return the list of movies with a 200 OK response
                return Ok(movies);
            }
            catch (Exception ex)
            {
                // Log the error and return a 500 Internal Server Error response
                _logger.LogError(ex, "Failed to search movies");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // Endpoint to get detailed information about a specific movie by ID
        [HttpGet("details/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailsReturn))] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public async Task<ActionResult<DetailsReturn>> GetDetails(int id)
        {
            try
            {
                // Use the TMDB service to get movie details by ID
                var detail = await _TmdbService.ID(id);

                // Return the movie details with a 200 OK response
                return Ok(detail);
            }
            catch (Exception e)
            {
                // Log the error and return a 500 Internal Server Error response
                _logger.LogError(e, "Failed to search");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while searching for movies.");
            }
        }

        // Endpoint to get credits (cast and crew) for a specific movie by ID
        [HttpGet("{id}/credits")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreditsReturn))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
        public async Task<ActionResult<CreditsReturn>> GetCredits(int id)
        {
            try
            {
                // Use the TMDB service to get credits for the specified movie ID
                var credits = await _TmdbService.Credits(id);

                // Return the credits with a 200 OK response
                return Ok(credits);
            }
            catch (Exception e)
            {
                // Log the error and return a 500 Internal Server Error response
                _logger.LogError(e, "Failed to search");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while searching for movies.");
            }
        }
    }
}
