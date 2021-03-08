using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using IMDB_API.Models;
using IMDB_API.Validators;

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

        private MovieDTO createMovieDto(string movieId, HtmlDocument doc)
        {
            var metaTags = doc.DocumentNode.SelectNodes("//meta");

            return new MovieDTO()
            {
                MovieId = movieId,
                MoviePoster = getMetaProperty(metaTags, "og:image"),
                MovieTitle = getMetaProperty(metaTags, "og:title"),
            };
        }

        //Wanted to do this with link and not have to write a whole method for it but it seems like linq wont work on a HtmlNodeCollection
        private string getMetaProperty(HtmlNodeCollection metaTags, string selector)
        {
            foreach (var node in metaTags)
            {
                if (node.GetAttributeValue("property", "property not found") == selector)
                {
                    return node.GetAttributeValue("content", "content not found");
                }
            }

            return null;
        }
    }
}
