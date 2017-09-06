using System.Threading.Tasks;
using System;
using GameApi.models;
namespace GameApi.processors
{
    public class PlayerProcessor
    {
        private readonly IRepository repo;

        public PlayerProcessor(IRepository repository)
        {
            repo = repository;
        }

        public Player GetPlayer(Guid id)
        {
            return repo.Get(id);

        }

        public Player[] Get()
        {
            return repo.GetAll();

        }

        public Player Create(NewPlayer player)
        {
            var pelaaja = new Player()
            {
                Name = player.Name
            };
            repo.Add(pelaaja);
            return pelaaja;

        }


        public Player Modify(Guid id, ModifiedPlayer player)
        {
            Player p = new Player();
            p.Name = player.Name;
            p.Id = player.Id;
            p.Level = player.Level;
            repo.Update(id, p);
            return p;

        }

        public Player Delete(Guid id)
        {
            var player = repo.Delete(id);
            return player;
        }

    }
}