using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Streaming1.Models;
using Microsoft.AspNetCore.Http.Features;
using Streaming1.DAL.Abstract;
using Streaming1.ViewModels;
using System.Net.WebSockets;
using System.Linq;
namespace Streaming1.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    StreamingDbContext _context;
    private readonly IShowRepository _showRepository;

    private readonly ICreditRepository _creditRepository;

    public HomeController(ILogger<HomeController> logger, StreamingDbContext context, 
    IShowRepository showRepo, ICreditRepository creditRepo)
    {
        _logger = logger;
        _context = context;
        _showRepository = showRepo;
        _creditRepository = creditRepo;
    }

    public IActionResult Index()
    {
        List<Show> shows = _context.Shows.ToList();
        return View(shows);
    }
    
    public IActionResult Info()
    {
        // Retrieve all shows from the repository and convert to a list
        List<Show> shows = _showRepository.GetAll().ToList();

        // Retrieve a list of show descriptions from the repository
        List<string> descriptionList = _showRepository.DescriptionList();

        // Get counts of different show types (e.g., show, movie, tv)
        (int show, int movie, int tv) Type = _showRepository.NumberOfShowsByType();

        // Retrieve the show with the highest TMDB popularity rating
        Show tmdb = _showRepository.ShowWithHighestTMDBPopularity();

        // Retrieve the show with the most IMDB votes
        Show imdb = _showRepository.ShowWithMostIMDBVotes();

        // Retrieve a distinct, ordered list of genres associated with all shows
        List<Genre> Genre = _showRepository.GetAll()
                        .SelectMany(s => s.GenreAssignments)  // Flatten GenreAssignments for each show
                        .Select(ga => ga.Genre)               // Select the Genre from each assignment
                        .Distinct()                          // Ensure each genre appears only once
                        .OrderBy(g => g.GenreString)         // Sort genres alphabetically
                        .ToList();

        // Get the PersonId of the individual with the most frequent RoleId = 2 in credits
        int? mostFrequentPersonId = _creditRepository.GetAll()
            .Where(c => c.RoleId == 2)                    // Filter for RoleId = 2
            .GroupBy(c => c.PersonId)                     // Group by PersonId
            .OrderByDescending(g => g.Count())            // Order by frequency in descending order
            .Select(g => g.Key)                           // Select the PersonId
            .FirstOrDefault();                            // Take the first result, if available

        // Create a ViewModel instance and populate it with retrieved data
        ShowsVM vm = new ShowsVM
        {
            DescriptionList = descriptionList,
            Shows = shows,
            ShowCount = shows.Count(),
            Type = Type,
            Popularity = tmdb,
            Votes = imdb,
            Genres = Genre
        };

        // If a most frequent PersonId was found
        if (mostFrequentPersonId.HasValue)
        {
            // Retrieve the name and associated show titles for the most frequent person
            var personData = _context.People
                .Where(p => p.Id == mostFrequentPersonId.Value)  // Filter by PersonId
                .Select(p => new
                {
                    PersonName = p.FullName,                      // Get the full name
                    ShowTitles = p.Credits                        // Get associated show titles for RoleId = 2
                        .Where(c => c.RoleId == 2)
                        .Select(c => c.Show.Title)
                        .Distinct()                               // Remove duplicate titles
                        .ToList()
                })
                .FirstOrDefault();

            // If person data was found, populate ViewModel with name and show titles
            if (personData != null)
            {
                vm.PersonName = personData.PersonName;
                vm.ShowTitles = personData.ShowTitles;
            }
        }

        // Return the populated ViewModel to the view
        return View(vm);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
