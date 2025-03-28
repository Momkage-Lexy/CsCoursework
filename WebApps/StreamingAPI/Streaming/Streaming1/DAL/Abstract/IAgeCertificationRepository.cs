using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Streaming1.Models;

// Here is a starting point to help you use a good testable design for your application logic
// It uses the generic repository pattern, which is a common pattern for data access in .NET
// Put this in a DAL/Abstract folder (DAL stands for Data Access Layer)

namespace Streaming1.DAL.Abstract;

public interface IAgeCertificationRepository : IRepository<AgeCertification>
{
}