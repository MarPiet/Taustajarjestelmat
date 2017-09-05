using Microsoft.AspNetCore.Mvc;
using GameApi.models;
using System.Threading.Tasks;
using System;

namespace GameApi.Controllers
{
    [Route("api/players")]
    public class PlayersController : Controller
    {
        private readonly IRepository repo;

        public PlayersController(IRepository playerRepository)
        {
            this.repo = playerRepository;
        }
        
        [HttpGet("{id}")]
        public async Task<Player> Get(Guid id)
        {
            return repo.Get(id);
        }
        [HttpGet]
        public async Task<Player[]> GetAll()
        {
            return repo.GetAll();
        }


        [HttpPost]
        public async Task<Player> Create(NewPlayer player)
        {
            var pelaaja = new Player()
            {
                Id = Guid.NewGuid(),
                Name = player.Name

            };
            repo.Add(pelaaja);
            return pelaaja;

        }
        [HttpPut]
        public async Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            Player p = new Player();
            p.Name = player.Name;
            p.Id = player.Id;
            p.Level = player.Level;
            repo.Update(id, p);
            return p;
        }



        [HttpDelete]
        public async Task<Player> Delete(Guid id)
        {
            var player = repo.Delete(id);
            return player;
        }



    }



}