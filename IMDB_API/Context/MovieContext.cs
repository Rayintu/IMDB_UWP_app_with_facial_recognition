using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB_API.Models;
using Microsoft.EntityFrameworkCore;

namespace IMDB_API.Context
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
    }
}
