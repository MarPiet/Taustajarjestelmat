using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gameapi.Models;
using gameapi.mongodb;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace gameapi.Repositories
{
    //Gets from and updates data to MongoDb 
    public class MongoDbRepository : IRepository
    {
        private IMongoCollection<Player> _collection;
        public MongoDbRepository(MongoDBClient client)
        {
            //Getting the database with name "game"
            IMongoDatabase database = client.GetDatabase("game");

            //Getting collection with name "players"
            _collection = database.GetCollection<Player>("players");
        }

        public async Task<Item> CreateItem(Guid playerId, Item item)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();

            for (int i = 0; i < player.Items.Length; i++)
            {
                if (player.Items[i] == null)
                {
                    var filter1 = Builders<Player>.Filter.Eq(p => p.Items[i], null);
                    var update = Builders<Player>.Update.Set(x => x.Items[i], item);
                    var result = _collection.UpdateOne(filter, update);
                    break;
                }


            }
            return item;
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            await _collection.InsertOneAsync(player);
            return player;
        }

        public async Task<Item> DeleteItem(Guid playerId, Item item)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();

            for (int i = 0; i < player.Items.Length; i++)
            {
                if (player.Items[i].Id == item.Id)
                {
                    var filter1 = Builders<Player>.Filter.Eq(p => p.Items[i], item);
                    var update = Builders<Player>.Update.Set(x => x.Items[i], null);
                    var result = _collection.UpdateOne(filter, update);
                    break;
                }


            }
            return item;
        }

        public async Task<Player> DeletePlayer(Guid playerId)
        {
            var result = await _collection.FindAsync(a => a.Id == playerId);
            await _collection.DeleteOneAsync(a => a.Id == playerId);
            return null;
        }

        public Task<Item[]> GetAllItems(Guid playerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Player[]> GetAllPlayers()
        {
            int counter = 0;
            Player[] player = new Player[2];
            var filter = Builders<Player>.Filter.Empty;
            var cursor = await _collection.FindAsync(filter);
            while (await cursor.MoveNextAsync())
            {
                IEnumerable<Player> batch = cursor.Current;
                foreach (Player document in batch)
                {
                    player[counter] = document;
                    counter++;
                }

            }


            return player;
        }

        public async Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();

            for (int i = 0; i < player.Items.Length; i++)
            {
                if (player.Items[i].Id == itemId)
                {
                   return player.Items[i];
                }


            }
            return null;
        }

        public async Task<Player> GetPlayer(Guid playerId)
        {
            //Builders<T> static class is where the filters are to be found 
            //We use filters to create queries
            //Eq means Equals
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();
            return player;
        }

        public Task<Item> UpdateItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }

        public async Task<Player> UpdatePlayer(Player player)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, player.Id);
            await _collection.ReplaceOneAsync(filter, player);
            return player;
        }
    }
}