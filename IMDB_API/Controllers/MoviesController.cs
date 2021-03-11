using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using IMDB_API.Models;
using IMDB_API.Validators;
using IMDB_UWP_app_with_facial_recognition.JSON;
using Newtonsoft.Json;

namespace IMDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        public ActionResult getMovie(string movieId)
        {
            if (!MovieValidator.ValidateMovieId(movieId))
            {
                return BadRequest("Please enter a valid movieId");
            }

            var url = $"https://www.imdb.com/title/{movieId}";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            MovieDTO movieDto = createMovieDto(movieId, doc);

            return Ok(movieDto);
        }

        [HttpGet]
        [Route("{action=Details}/{movieId?}")]
        public ActionResult Details(string movieId)
        {
            var url = $"https://www.imdb.com/title/{movieId}";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            MovieDetailsDTO movieDetailsDto = createMovieDetailsDto(movieId, doc);

            return Ok(movieDetailsDto);
        }

        private MovieDTO createMovieDto(string movieId, HtmlDocument doc)
        {
            MovieInfo movieInfo = getMovieInfo(doc);

            return new MovieDTO()
            {
                MovieId = movieId,
                MoviePoster = movieInfo.image,
                MovieTitle = movieInfo.name,
            };
        }

        private MovieDetailsDTO createMovieDetailsDto(string movieId, HtmlDocument doc)
        {
            MovieInfo movieInfo = getMovieInfo(doc);

            return new MovieDetailsDTO()
            {
                MovieId = movieId,
                MoviePoster = movieInfo.image,
                MovieTitle = movieInfo.name,
                Source = "Website",
                MovieSynopsis = movieInfo.description,
                MovieRating = movieInfo.aggregateRating.ratingValue,
            };
        }

        private MovieInfo getMovieInfo(HtmlDocument doc)
        {
            var scriptTags = doc.DocumentNode.SelectNodes("//script");

            MovieInfo movieInfo = null;

            foreach (var node in scriptTags)
            {
                if (node.GetAttributeValue("type", "no type found") == "application/ld+json")
                {
                    return JsonConvert.DeserializeObject<MovieInfo>(node.FirstChild.InnerText);
                }
            }

            return null;
        }
    }
}