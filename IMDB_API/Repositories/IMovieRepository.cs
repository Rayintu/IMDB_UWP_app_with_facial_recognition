using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB_API.Models;

namespace IMDB_API.Repositories
{
    interface IMovieRepository
    {
        void Add(Movie movie);

        Task<Movie> FindMovieAsync(string movieId);
    }
}
