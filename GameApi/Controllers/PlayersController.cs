using Microsoft.AspNetCore.Mvc;
using GameApi.models;
using GameApi.processors;
using System.Threading.Tasks;
using System;


namespace GameApi.Controllers
{
    [Route("api/players")]
    public class PlayersController : Controller
    {
        private PlayerProcessor playerProcessor;

        public PlayersController(PlayerProcessor playerProcessor)
        {
            this.playerProcessor = playerProcessor;
        }

        [HttpGet("{id}")]
        public async Task<Player> Get(Guid id)
        {
            return playerProcessor.GetPlayer(id);
        }
        [HttpGet]
        public async Task<Player[]> GetAll()
        {
            return playerProcessor.Get();
        }


        [HttpPost]
        public async Task<Player> Create(NewPlayer player)
        {
            return playerProcessor.Create(player);

        }
        [HttpPut]
        public async Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            return playerProcessor.Modify(id, player);
        }



        [HttpDelete]
        public async Task<Player> Delete(Guid id)
        {
            return playerProcessor.Delete(id);
        }



    }



}