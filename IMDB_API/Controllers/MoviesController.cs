using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using IMDB_API.Context;
using IMDB_API.Models;
using IMDB_API.Repositories;
using IMDB_API.Validators;
using IMDB_UWP_app_with_facial_recognition.JSON;
using Mapster;
using Newtonsoft.Json;

namespace IMDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : EFController<Movie, EFCoreMovieRepository>
    {
        private EFCoreMovieRepository _repository;

        public MoviesController(EFCoreMovieRepository repository) : base(repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult getMovie(string movieId)
        {
            if (!MovieValidator.ValidateMovieId(movieId))
            {
                return BadRequest("Please enter a valid movieId");
            }

            var doc = getWebsiteHtmlDocument(movieId);

            MovieDTO movieDto = CreateMovieDto(movieId, doc);

            return Ok(movieDto);
        }

        [HttpGet]
        [Route("{action=Details}/{movieId?}")]
        public async Task<ActionResult> Details(string movieId)
        {
            MovieDetailsDTO movieDetailsDto = null;

            if (!_repository.IsMovieInDB(movieId).Result)
            {
                var doc = getWebsiteHtmlDocument(movieId);

                movieDetailsDto = createMovieDetailsDto(movieId, doc);

                Movie movie = movieDetailsDto.Adapt<Movie>();
                movie.Source = "Database";

                await _repository.AddMovie(movie);
            }
            else
            {
                Movie movie = await _repository.GetMovie(movieId);
                movieDetailsDto = movie.Adapt<MovieDetailsDTO>();
            }

            return Ok(movieDetailsDto);
        }

        private MovieDTO CreateMovieDto(string movieId, HtmlDocument doc)
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
                DatePublished = movieInfo.datePublished,
            };
        }

        private HtmlDocument getWebsiteHtmlDocument(string movieId)
        {
            var url = $"https://www.imdb.com/title/{movieId}";
            var web = new HtmlWeb();
            return web.Load(url);
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