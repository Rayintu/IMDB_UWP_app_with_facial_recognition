using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_UWP_app_with_facial_recognition.DTOs
{
    public class MovieDTO
    {
        public string MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string MoviePoster { get; set; }
    }

    public class MovieDetailsDTO
    {
        public string MovieId { get; set; } // The IMDB id attached to the movie
        public string Source { get; set; } // Did the info come from the cache db or the website
        public string MovieTitle { get; set; } // Title of the movie
        public string MoviePoster { get; set; } // Link to image of movieposter
        public string MovieSynopsis { get; set; } // Synopsis of the movie
        public string MovieRating { get; set; } // Rating of the movie double range: 0 - 10
        public string DatePublished { get; set; } // Release date of the movie yyyy-mm-dd
    }
}
