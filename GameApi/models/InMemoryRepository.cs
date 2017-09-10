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
            player.Id = Guid.NewGuid();
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


        public bool Update(Guid id, Player player)
        {
            foreach (var p in dict)
            {
                if (p.Key == id)
                {
                    if (player.Name != null)
                        p.Value.Name = player.Name;
                    p.Value.Level = player.Level;
                    p.Value.Id = player.Id;
                    return true;
                }

            }
            return false;

        }

        public void AddItem(Guid id, Item item)
        {
            foreach (var p in dict)
            {
                if (p.Key == id)
                    for (int i = 0; i < p.Value.Items.Length; i++)
                    {
                        if (p.Value.Items[i] == null)
                        {
                            p.Value.Items[i] = item;
                            return;
                        }

                    }

            }

        }

        public Item[] GetAllItems(Guid id)
        {
            foreach (var p in dict)
            {
                if (p.Key == id)
                {
                    return p.Value.Items;
                }

            }
            return null;

        }

        public bool UpdateItem(Guid id, Guid itemid, Item item)
        {
            foreach (var p in dict)
            {
                if (p.Key == id)
                {
                    for (int i = 0; i < p.Value.Items.Length; i++)
                        if (p.Value.Items[i].id == itemid)
                        {
                            p.Value.Items[i].CreationDate = item.CreationDate;
                            p.Value.Items[i].Level = item.Level;
                            p.Value.Items[i].Price = item.Price;

                            return true;
                        }

                }

            }
            return false;
        }

        public Item DeleteItem(Guid id, Guid itemId)
        {
            var item = new Item();
            foreach (var p in dict)
            {
                if (p.Key == id)
                {
                    for (int i = 0; i < p.Value.Items.Length; i++)
                    {
                        if (itemId == p.Value.Items[i].id)
                        {
                            item = p.Value.Items[i];
                            p.Value.Items[i] = null;
                            return item;
                        }

                    }


                }

            }
            return null;

        }



    }
}