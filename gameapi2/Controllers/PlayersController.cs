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
        
        [HttpGet]
        public Task<Player[]> GetAll()
        {
            return _processor.GetAll();
        }

        [HttpGet("{id}")]
        public Task<Player> Get(Guid id)
        {
            return _processor.Get(id);
        }

        [HttpPost]
        [ValidateModel]
        public Task<Player> Create(/*[FromBody]*/NewPlayer player)
        {
            return _processor.Create(player);
        }

        [HttpDelete("{id}")]
        public Task<Player> Delete(Guid id)
        {
            return _processor.Delete(id);
        }

        [HttpPut("{id}")]
        public Task<Player> Update(Guid id, /*[FromBody]*/ModifiedPlayer player)
        {
            return _processor.Update(id, player);
        }
    }
}