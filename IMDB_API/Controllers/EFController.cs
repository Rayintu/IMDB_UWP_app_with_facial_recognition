using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Controllers
{
    [Route("api/EF")]
    [ApiController]
    public abstract class EFController<TEntity, TRepository> : ControllerBase
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository repository;

        public EFController(TRepository repository)
        {
        }
    }
}
