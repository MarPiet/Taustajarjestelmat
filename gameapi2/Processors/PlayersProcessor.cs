using System;
using System.Threading.Tasks;
using gameapi.Models;
using gameapi.Repositories;

namespace gameapi.Processors
{
    // Business logic (logic for verifying and updating the data) happens here
    public class PlayersProcessor
    {
        private readonly IRepository _repository;
        public PlayersProcessor(IRepository repository)
        {
            _repository = repository;
        }

        public Task<Player[]> GetAll()
        {
            return _repository.GetAllPlayers();
        }

        public Task<Player> Get(Guid id)
        {
            return _repository.GetPlayer(id);
        }

        public Task<Player> Create(NewPlayer newPlayer)
        {
            Player player = new Player()
            {
                Items = new Item[10],
                Id = Guid.NewGuid(),
                Name = newPlayer.Name,
                Level = 1,

            };
            return _repository.CreatePlayer(player);
        }

        public Task<Player> Delete(Guid id)
        {
            return _repository.DeletePlayer(id);
        }

  
        public async Task<Player> Update(Guid id, ModifiedPlayer modifiedPlayer)
        {
            Player player = await _repository.GetPlayer(id);
            player.Level = modifiedPlayer.Level;
            await _repository.UpdatePlayer(player);
            return player;
        }
    }
}