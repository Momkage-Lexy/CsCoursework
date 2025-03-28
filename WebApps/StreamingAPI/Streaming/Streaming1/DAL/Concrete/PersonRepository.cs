using System.Collections.Immutable;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Streaming1.DAL.Abstract;
using Streaming1.Models;

// And the associated implementation, stubbed out for you.
// Put this in folder DAL/Concrete

namespace Streaming1.DAL.Concrete;

public class PersonRepository : Repository<Person>, IPersonRepository
{
    public PersonRepository(StreamingDbContext context) : base(context)
    {
    }
    public bool JustWatchPersonIdAlreadyInUse(int JustWatchPersonID)
    {
        return GetAll().Any(s => s.JustWatchPersonId == JustWatchPersonID);
    }

}