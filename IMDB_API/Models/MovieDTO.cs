using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB_API.Models
{
    public class MovieDTO
    {
        public string MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string MoviePoster { get; set; }
    }

    public class MovieDetailsDTO
    {
        public string Id { get; set; }
    }
}
