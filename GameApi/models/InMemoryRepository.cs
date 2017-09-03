using System.Collections.Generic;
using System;
using System.Linq;

namespace GameApi.models
{
    public class InMemoryRepository : IRepository
    {
        Dictionary<Guid, Player> dict = new Dictionary<Guid, Player>();

        public void Add(Player player)
        {
            dict.Add(player.Id, player);
        }

        public Player Delete(Guid id)
        {
            var p = new Player();
            foreach (var player in dict.ToList())
            {
                if (player.Key == id)
                {
                    p = player.Value;
                    dict.Remove(player.Key);
                }
            }
            return p;
        }

        public Player Get(Guid id)
        {
            foreach (var player in dict.ToList())
            {
                if (player.Key == id)
                    return player.Value;
            }

            return null;
        }

        public Player[] GetAll()
        {
            return dict.Values.ToArray();
        }


        public bool Update(Player player)
        {
            foreach (var p in dict)
            {
                if (p.Key == player.Id)
                {
                    p.Value.Name = player.Name;
                    p.Value.Level = player.Level;
                    p.Value.Id = player.Id;
                    return true;
                }

            }
            return false;

        }



    }
}