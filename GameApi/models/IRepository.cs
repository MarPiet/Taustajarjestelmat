using System;
using System.Collections.Generic;

namespace GameApi.models
{
    public interface IRepository
    {
        void Add(Player player);
        Player Delete(Guid id);
        Player Get(Guid id);
        Player[] GetAll();
        bool Update(Guid id, Player player);

        void AddItem(Guid id, Item item);
        Item[] GetAllItems(Guid id);

        bool UpdateItem(Guid id, Guid itemId, Item item);
        Item DeleteItem(Guid id, Guid itemId);

    }
}