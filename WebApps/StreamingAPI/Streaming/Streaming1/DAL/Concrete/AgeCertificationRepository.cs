using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Streaming1.DAL.Abstract;
using Streaming1.Models;

// And the associated implementation, stubbed out for you.
// Put this in folder DAL/Concrete

namespace Streaming1.DAL.Concrete;

public class AgeCertificationRepository : Repository<AgeCertification>, IAgeCertificationRepository
{
    public AgeCertificationRepository(StreamingDbContext context) : base(context)
    {

    }

}