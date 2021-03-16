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
        private MovieContext context;

        public EFCoreMovieRepository(MovieContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<string> AddMovie(Movie movie)
        {
            await context.Movies.AddAsync(movie);
            context.SaveChanges();
            return "Save successful";
        }

        public async Task<Movie> GetMovie(string movieId)
        {
            return await context.Movies.FindAsync(movieId); ;
        }

        public async Task<bool> IsMovieInDB(string movieId)
        {
            return await GetMovie(movieId) != null ? true : false;
        }

    }
}
