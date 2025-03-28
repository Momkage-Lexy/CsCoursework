using System.Collections.Immutable;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Streaming1.DAL.Abstract;
using Streaming1.Models;

// And the associated implementation, stubbed out for you.
// Put this in folder DAL/Concrete

namespace Streaming1.DAL.Concrete;

public class ProductionCountryAssignmentRepository : Repository<ProductionCountryAssignment>, IProductionCountryAssignmentRepository
{
    public ProductionCountryAssignmentRepository(StreamingDbContext context) : base(context)
    {
    }

}