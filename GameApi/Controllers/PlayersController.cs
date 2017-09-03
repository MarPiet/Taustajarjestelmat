using Microsoft.AspNetCore.Mvc;
using GameApi.models;
using System.Threading.Tasks;
using System;

namespace GameApi.Controllers
{
    [Route("api/players")]
    public class PlayersController : Controller
    {
        static readonly IRepository repo = new InMemoryRepository();

        [HttpGet]
        public async Task<Player[]> GetAll()
        {
            return repo.GetAll();
        }
        [Route("api/players/{id:int}")]
        [HttpGet]
        public async Task<Player> Get(Guid id)
        {
            return repo.Get(id);
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
        /*    [HttpPut]
            public async Task<Player> Modify(Guid id, ModifiedPlayer player)
            {
                Player p = new Player();
                p.Name = player.Name;
                p.Id = player.Id;
                p.Level = player.Level;
                repo.Update(p);
            }
    */


        [HttpDelete]
        public async Task<Player> Delete(Guid id)
        {
            var player = repo.Delete(id);
            return player;
        }



    }



}