using System;
using System.Threading.Tasks;
using gameapi.Exceptions;
using gameapi.Models;
using gameapi.ModelValidation;
using gameapi.Processors;
using Microsoft.AspNetCore.Mvc;

namespace gameapi.Controllers
{

    [Route("api/players")]
    public class PlayersController : Controller
    {
        private PlayersProcessor _processor;
        public PlayersController(PlayersProcessor processor)
        {
            _processor = processor;
        }
        //tht1, 4, 11
        [HttpGet]
        public Task<Player[]> GetAll(int minScore, string itemType)
        {
            return _processor.GetAll(minScore, itemType);
        }
        //tht5
        [HttpGet("{num:int}")]
        public Task<Player[]> GetBySize(int num)
        {
            return _processor.GetBySize(num);
        }
        [HttpGet("{id:guid}")]
        public Task<Player> Get(Guid id)
        {
            return _processor.Get(id);
        }
        //tht9
        [HttpGet("{asd:bool}")]
        public Task<Player[]> GetTopTen()
        {
            return _processor.GetTopTen();
        }
        //tht2
        [HttpGet("{name}")]
        public Task<Player> Get(string name)
        {
            return _processor.GetByName(name);
        }
        [HttpPost]
        [ValidateModel]
        public Task<Player> Create(/*[FromBody]*/NewPlayer player)
        {
            return _processor.Create(player);
        }
        //tht8
        [HttpPost("{id}")]
        public Task<Player> PushItem(Guid id, string type, int level)
        {
            return _processor.PushItem(id, type, level);
        }

        [HttpDelete("{id}")]
        public Task<Player> Delete(Guid id)
        {
            return _processor.Delete(id);
        }

        [HttpPut("{id:guid}")]
        public Task<Player> Update(Guid id, /*[FromBody]*/ModifiedPlayer player)
        {
            return _processor.Update(id, player);
        }
        //tht6, 7
        [HttpPut("{name:minlength(1)}")]
        public Task<Player> UpdatePlayerNameAndScore(string name, string newName, int score)
        {
            return _processor.UpdatePlayerNameAndScore(name, newName, score);
        }


    }
}