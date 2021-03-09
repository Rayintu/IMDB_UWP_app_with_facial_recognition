using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IMDB_UWP_app_with_facial_recognition.DTOs;
using Newtonsoft.Json;

namespace IMDB_UWP_app_with_facial_recognition.Models
{
    class Movie
    {
        public Movie()
        {
        }

        public async Task<MovieDTO> getMovie(string movieId)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:50276/api/Movies?movieId={movieId}");
                    response.EnsureSuccessStatusCode();
                    string responseContent = await response.Content.ReadAsStringAsync();
                    MovieDTO movie = JsonConvert.DeserializeObject<MovieDTO>(responseContent);

                    return movie;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e.Message);
                    throw e;
                }
            }

            return null;
        }

        public string getMovieIdFromUrl(string ImdbUrl)
        {
            var match = Regex.Match(ImdbUrl, "(?<=https:\\/\\/www\\.imdb\\.com\\/title\\/)\\w*");
            return match.Value;
        }
    }
}
