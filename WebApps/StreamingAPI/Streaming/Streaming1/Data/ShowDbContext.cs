using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Streaming1.Models;

    public class ShowDbContext : DbContext
    {
        public ShowDbContext (DbContextOptions<ShowDbContext> options)
            : base(options)
        {
        }

        public DbSet<Streaming1.Models.Show> Show { get; set; } = default!;
    }
