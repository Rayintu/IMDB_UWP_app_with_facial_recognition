using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB_API.Models;
using Mapster;

namespace IMDB_API.Cache
{
    public class ConcurrentCache
    {
        private readonly ConcurrentDictionary<string, Movie> _concurrentCacheDictionary;

        public ConcurrentCache()
        {
            _concurrentCacheDictionary = new ConcurrentDictionary<string, Movie>();
        }

        public Movie GetMovie(string movieId)
        {
            bool result = _concurrentCacheDictionary.TryGetValue(movieId, out var movie);

            return movie;

        }

        public void AddMovie(Movie _movie)
        {
            Movie movie = new Movie();
            movie = _movie.Adapt<Movie>();
            movie.Source = "Concurrent cache";

            _concurrentCacheDictionary.AddOrUpdate(movie.MovieId, movie, (key, oldValue) => movie);
//            if(concurrentDictionary != null) concurrentDictionary.TryAdd(movie.MovieId, movie);
        }
    }

    public sealed class ConcurrentCacheSingleton
    {
        private readonly ConcurrentDictionary<string, Movie> _concurrentCacheDictionary;

        private static readonly Lazy<ConcurrentCacheSingleton>
            lazy =
                new Lazy<ConcurrentCacheSingleton>
                    (() => new ConcurrentCacheSingleton());

        public static ConcurrentCacheSingleton Instance { get { return lazy.Value; } }

        private ConcurrentCacheSingleton()
        {
            _concurrentCacheDictionary = new ConcurrentDictionary<string, Movie>();
        }

        public Movie GetMovie(string movieId)
        {
            bool result = _concurrentCacheDictionary.TryGetValue(movieId, out var movie);

            return movie;

        }

        public void AddMovie(Movie _movie)
        {
            Movie movie = new Movie();
            movie = _movie.Adapt<Movie>();
            movie.Source = "Concurrent cache";

            _concurrentCacheDictionary.AddOrUpdate(movie.MovieId, movie, (key, oldValue) => movie);
        }
    }
}
