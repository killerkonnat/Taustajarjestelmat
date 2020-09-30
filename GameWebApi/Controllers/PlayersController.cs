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

        [HttpPost] //{"Name": "Matti"}
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


        [HttpGet] //GetPlayersWithXscore?minScore=x
        [Route("GetPlayersWithXscore")]
        public async Task<Player> GetPlayersWithXscore(int minScore)
        {
            await _irepository.GetPlayersWithXscore(minScore);
            return null;
        }

        [HttpGet] //"M"
        [Route("name:string")]
        public async Task<Player> GetPlayerWithName(string name)
        {
            await _irepository.GetPlayerWithName(name);
            return null;
        }

        [HttpGet] //"M"
        [Route("GetPlayersWithTag/tag:string")]
        public async Task<Player> GetPlayersWithTag(string tag)
        {
            await _irepository.GetPlayersWithTag(tag);
            return null;
        }

  

        [HttpGet] //"M"
        [Route("GetPlayersWithNumItems/itemAmount:int")]
        public async Task<Player> GetPlayersWithNumItems(int itemAmount)
        {
            await _irepository.GetPlayersWithNumItems(itemAmount);
            return null;
        }

        [HttpGet] //"M"
        [Route("UpdatePlayerName/new_name:string")]
        public async Task<Player> ChangePlayerName(Guid id, string name)
        {
            await _irepository.ChangePlayerName(id, name);
            return null;
        }

        [HttpGet]
        [Route("IncrementScore/score_add:int")]
        public async Task<Player> IncrementScore(Guid id, int points)
        {
            await _irepository.IncrementScore(id, points);
            return null;
        }

        [HttpPost] //{"Score":5}
        [Route("PushItem/{id:Guid}")]
        public async Task<Player> PushItem(Guid id, [FromBody] Item item)
        {
            await _irepository.PushItem(id, item);
            return null;
        }

    }
}
