using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GameWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Route("api/players")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _log;
        private readonly IRepository _irepository;
        public PlayersController(ILogger<PlayersController> log, IRepository repository)
        {
            _log = log;
            _irepository = repository;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<Player> Get(Guid id)
        {
            await _irepository.Get(id);
            return null;
        }
        [HttpGet]
        [Route("Getall")]
        public Task<Player[]> GetAll()
        {
            return _irepository.GetAll();
        }

        [HttpPost]
        [Route("Create")]

        public async Task<Player> Create([FromBody] NewPlayer pl)
        {
            Player newPl = new Player() { Id = Guid.NewGuid(), Name = pl.Name };

            await _irepository.Create(newPl);
            return null;
        }


        [HttpPost]
        [Route("Delete")]
        public async Task<Player> Delete([FromBody] Guid id)
        {
            await _irepository.Delete(id);
            return null;
        }
    }
}
