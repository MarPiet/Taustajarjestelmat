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

        public Task<Item> CreateItem(Guid playerId, Item item)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = _collection.Find(filter);
            var player = cursor.First();

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
            return Task.Run(() => item);
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            await _collection.InsertOneAsync(player);
            return player;
        }

        public Task<Item> DeleteItem(Guid playerId, Item item)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = _collection.Find(filter);
            var player = cursor.First();

            for (int i = 0; i < player.Items.Length; i++)
            {
                if (player.Items[i] != null)
                {
                    if (player.Items[i].Id == item.Id)
                    {
                        var filter1 = Builders<Player>.Filter.Eq(p => p.Items[i], item);
                        var update = Builders<Player>.Update.Set(x => x.Items[i], null);
                        var result = _collection.UpdateOne(filter, update);
                        break;
                    }
                }



            }
            return Task.Run(() => item);
        }

        public async Task<Player> DeletePlayer(Guid playerId)
        {
            var result = await _collection.FindAsync(a => a.Id == playerId);
            await _collection.DeleteOneAsync(a => a.Id == playerId);
            return null;
        }

        public Task<Item[]> GetAllItems(Guid playerId)
        {
            Item[] item = new Item[10];
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = _collection.Find(filter);
            var player = cursor.First();

            for (int i = 0; i < player.Items.Length - 1; i++)
            {
                item[i] = player.Items[i];
            }
            return Task.Run(() => item);
        }

        public async Task<Player[]> GetAllPlayers(int minScore, string itemType)
        {
            int counter = 0;
            Player[] player = new Player[2];
            if (minScore == 0 && itemType == null)
            {

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
            }
            else if (minScore > 0)
            {
                FilterDefinition<Player> filter = Builders<Player>.Filter.Gte("Score", minScore);
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

            }
            else if (itemType != null)
            {
                FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("Items.Type", itemType);
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
            }



            return player;
        }

        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = _collection.Find(filter);
            var player = cursor.First();

            for (int i = 0; i < player.Items.Length; i++)
            {
                if (player.Items[i] != null)
                {
                    if (player.Items[i].Id == itemId)
                        return Task.Run(() => player.Items[i]);

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
        public async Task<Player> UpdatePlayerNameAndScore(string name, string newName, int score)
        {
            FilterDefinition<Player> filter1;
            if (newName != null)
            {
                var filter = Builders<Player>.Filter.Eq(p => p.Name, name);
                var update = Builders<Player>.Update.Set("Name", newName);
                await _collection.UpdateOneAsync(filter, update);
                filter1 = Builders<Player>.Filter.Eq(p => p.Name, newName);
            }
            else
            {
                var filter = Builders<Player>.Filter.Eq(p => p.Name, name);
                var update = Builders<Player>.Update.Inc("Score", score);
                await _collection.UpdateOneAsync(filter, update);
                filter1 = Builders<Player>.Filter.Eq(p => p.Name, name);
            }


            //testi

            var cursor = await _collection.FindAsync(filter1);
            var player1 = await cursor.FirstAsync();

            return player1;
        }



        public async Task<Player> GetPlayerByName(string name)
        {
            //Builders<T> static class is where the filters are to be found 
            //We use filters to create queries
            //Eq means Equals
            var filter = Builders<Player>.Filter.Eq(p => p.Name, name);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();
            return player;
        }

        public Task<Item> UpdateItem(Guid playerId, Item item)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = _collection.Find(filter);
            var player = cursor.First();


            for (int i = 0; i < player.Items.Length - 1; i++)
            {
                if (player.Items[i] != null)
                {
                    if (player.Items[i].Id == item.Id)
                    {

                        var filter1 = Builders<Player>.Filter.Eq(p => p.Items[i].Id, item.Id);
                        var update = Builders<Player>.Update.Set(x => x.Items[i].Price, item.Price);
                        var result = _collection.UpdateOne(filter, update);
                        return Task.Run(() => player.Items[i]);
                    }
                }



            }
            return null;
        }

        public async Task<Player> UpdatePlayer(Player player)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, player.Id);
            await _collection.ReplaceOneAsync(filter, player);
            return player;
        }

        public async Task<Player> PushItem(Guid id, Item item)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);


            var update = Builders<Player>.Update.Push(x => x.Items, item);
            await _collection.UpdateOneAsync(filter, update);

            var cursor = _collection.Find(filter);
            var player = cursor.First();
            return player;


        }

        public async Task<Item> DeleteAndAddScore(Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = await _collection.FindAsync(filter);
            var player = await cursor.FirstAsync();


            for (int i = 0; i < player.Items.Length - 1; i++)
            {
                if (player.Items[i] != null)
                {

                    var item = player.Items[i];
                    var update = Builders<Player>.Update.PopFirst(x => x.Items);
                    var result = await _collection.UpdateOneAsync(filter, update);

                    var update2 = Builders<Player>.Update.Inc("Score", 50);
                    var result2 = await _collection.UpdateOneAsync(filter, update2);
                    return item;

                }



            }
            return null;
        }
    }
}
