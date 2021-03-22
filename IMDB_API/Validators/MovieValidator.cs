using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IMDB_API.Validators
{
    public static class MovieValidator
    {
        public static bool ValidateMovieId(string movieId)
        {
            var match = Regex.Match(movieId, "\\btt\\d*\\b");
            return match.Length > 0;
        }
    }
}
