using System.Collections.Immutable;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Streaming1.DAL.Abstract;
using Streaming1.Models;

// And the associated implementation, stubbed out for you.
// Put this in folder DAL/Concrete

namespace Streaming1.DAL.Concrete;

public class ShowRepository : Repository<Show>, IShowRepository
{
    public ShowRepository(StreamingDbContext context) : base(context)
    {

    }

    public (int show, int movie, int tv) NumberOfShowsByType()
    {
        {
            int showCount = _dbSet.ToList().Count();
            int movieCount = _dbSet.Count(s => s.ShowTypeId == 1);
            int tvCount = _dbSet.Count(s => s.ShowTypeId == 2);

            return (showCount, movieCount, tvCount);
        }
    }

    public Show ShowWithHighestTMDBPopularity()
    {
        var show = _dbSet
            .OrderByDescending(s => s.TmdbPopularity)
            .FirstOrDefault();

        return new Show
        {
            Title = show.Title,
            TmdbPopularity = show.TmdbPopularity
        };
    }

    public Show ShowWithMostIMDBVotes()
    {
        var show = _dbSet
            .OrderByDescending(s => s.ImdbVotes)
            .FirstOrDefault();

        return new Show
        {
            Title = show.Title,
            ImdbVotes = show.ImdbVotes
        };
    }

    public List<string> DescriptionList()
    {
        return GetAll().Select(b => b.Description).ToList();
    }
    public bool ShowIdAlreadyInUse(int showID)
    {
        return GetAll().Any(s => s.Id == showID);
    }

}