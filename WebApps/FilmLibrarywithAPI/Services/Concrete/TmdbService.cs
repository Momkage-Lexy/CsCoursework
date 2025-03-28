using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Homework4.Models;

namespace Homework4.Services
{
    public class TmdbService : ITmdbService
    {
        private readonly HttpClient _httpClient; 
        private readonly ILogger<TmdbService> _logger; 


        public TmdbService(HttpClient httpClient, ILogger<TmdbService> logger)
        {
            _httpClient = httpClient; 
            _logger = logger; 
        }

        // Search for movies by a query string
        public async Task<List<MoviesReturn>> Search(string query)
        {
            // Construct the API endpoint using the query string
            string endpoint = $"/3/search/movie?query={Uri.EscapeDataString(query)}";
            try
            {
                // Make an asynchronous GET request to the API endpoint
                var response = await _httpClient.GetAsync(endpoint);

                // Check if the response is successful (status code 200 OK)
                if (response.IsSuccessStatusCode)
                {
                    // Read the response body as a string
                    var responseBody = await response.Content.ReadAsStringAsync();

                    // Configure JSON deserialization options to ignore case sensitivity
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    // Deserialize the response into a MoviesResponse object
                    var results = JsonSerializer.Deserialize<MoviesResponse>(responseBody, options);

                    // Map the deserialized results into a list of MoviesReturn objects
                    return results.Results.Select(movie =>
                    {
                        // Format the release date or set it to "Not Available" if invalid
                        string formattedReleaseDate = "Not Available";
                        if (!string.IsNullOrEmpty(movie.ReleaseDate) && DateTime.TryParse(movie.ReleaseDate, out var parsedDate))
                        {
                            formattedReleaseDate = parsedDate.ToString("MMMM dd, yyyy");
                        }

                        return new MoviesReturn
                        {
                            BackdropPath = movie.BackdropPath,
                            Overview = movie.Overview,
                            Title = movie.Title,
                            ReleaseDate = formattedReleaseDate,
                            Popularity = movie.Popularity,
                            Id = movie.Id,
                            PosterPath = movie.PosterPath
                        };
                    }).ToList();
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    // Log a warning if no results are found (404 Not Found)
                    _logger.LogWarning($"No results found for query '{query}'");
                    return new List<MoviesReturn>(); // Return an empty list
                }
                else
                {
                    // Log an error for other non-successful responses
                    _logger.LogError($"Failed to search: {response.StatusCode}\n{await response.Content.ReadAsStringAsync()}");
                    return new List<MoviesReturn>(); // Return an empty list
                }
            }
            catch (Exception ex)
            {
                // Log any unexpected exceptions
                _logger.LogError(ex, $"An error occurred while searching for '{query}'");
                return new List<MoviesReturn>(); // Return an empty list in case of an exception
            }
        }

        // Get detailed information about a movie by its ID
        public async Task<DetailsReturn> ID(int id)
        {
            // Construct the API endpoint for movie details
            string endpoint = $"/3/movie/{id}";

            // Make an asynchronous GET request to the API endpoint
            var response = await _httpClient.GetAsync(endpoint);

            // Check if the response is successful (status code 200 OK)
            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response body
                var responseBody = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var results = JsonSerializer.Deserialize<DetailsResponse>(responseBody, options);

                // Format runtime into hours and minutes, or "Not Available" if invalid
                string formattedRuntime = results.Runtime > 0
                    ? (results.Runtime >= 60
                        ? $"{results.Runtime / 60}h {results.Runtime % 60}m"
                        : $"{results.Runtime}m")
                    : "Not Available";

                // Format release date or set it to "Not Available" if invalid
                string formattedReleaseDate = !string.IsNullOrWhiteSpace(results.ReleaseDate)
                    ? DateTime.Parse(results.ReleaseDate).ToString("MMMM d, yyyy")
                    : "Not Available";

                // Map the response to a DetailsReturn object
                return new DetailsReturn
                {
                    Genres = results.Genres,
                    Overview = results.Overview,
                    Popularity = results.Popularity,
                    PosterPath = results.PosterPath,
                    ReleaseDate = formattedReleaseDate,
                    Revenue = results.Revenue,
                    Runtime = formattedRuntime,
                    Title = results.Title
                };
            }
            else
            {
                // Log an error for non-successful responses
                _logger.LogError($"Failed to fetch details: {response.StatusCode}\n{response.Content}");
                return new DetailsReturn(); 
            }
        }

        // Get cast and crew credits for a movie by its ID
        public async Task<CreditsReturn> Credits(int id)
        {
            // Construct the API endpoint for movie credits
            string endpoint = $"/3/movie/{id}/credits";

            // Make an asynchronous GET request to the API endpoint
            var response = await _httpClient.GetAsync(endpoint);

            // Check if the response is successful (status code 200 OK)
            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response body
                var responseBody = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var results = JsonSerializer.Deserialize<CreditsResponse>(responseBody, options);

                // Map the response to a CreditsReturn object
                return new CreditsReturn
                {
                    Cast = results.Cast
                };
            }
            else
            {
                // Log an error for non-successful responses
                _logger.LogError($"Failed to fetch credits: {response.StatusCode}\n{response.Content}");
                return new CreditsReturn(); 
            }
        }
    }
}
