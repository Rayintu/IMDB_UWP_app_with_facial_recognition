using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB_API.Context;
using IMDB_API.Models;

namespace IMDB_API.Repositories
{
    public class EFCoreMovieRepository : EFCoreRepository<Movie, MovieContext>
    {
        public EFCoreMovieRepository(MovieContext context) : base(context)
        {
        }
    }
}
