﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class EFController<TEntity, TRepository> : ControllerBase
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository repository;

        public EFController(TRepository repository)
        {
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await repository.GetAll();
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var movie = await repository.Get(id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntity movie)
        {
//            if (id != movie.Id)
//            {
//                return BadRequest();
//            }
            await repository.Update(movie);
            return NoContent();
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity movie)
        {
            await repository.Add(movie);
//            return CreatedAtAction("Get", new { id = movie.Id }, movie);
            return null;
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var movie = await repository.Delete(id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }
    }
}
