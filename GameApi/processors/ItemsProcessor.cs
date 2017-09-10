using System.Threading.Tasks;
using System;
using GameApi.models;
namespace GameApi.processors
{
    public class ItemsProcessor
    {
        private readonly IRepository repo;

        public ItemsProcessor(IRepository repository)
        {
            repo = repository;
        }

        public Item GetItem(Guid id)
        {
            return repo.Get(id).Items[0];
        }

        public Item[] Get(Guid id)
        {
            return repo.GetAllItems(id);

        }

        public Item Create(Guid playerId, NewItem item)
        {
            var itemi = new Item()
            {
                Level = item.Level,
                Price = item.Price,
                CreationDate = DateTime.Now,
                id = Guid.NewGuid()
            };
            repo.AddItem(playerId, itemi);
            return itemi;

        }


        public Item Modify(Guid id, Guid itemId, ModifiedItem item)
        {
            Item i = new Item();
            i.Level = item.Level;
            i.Price = item.Price;
            i.CreationDate = DateTime.Now;
            i.id = itemId;
            repo.UpdateItem(id, itemId, i);
            return i;

        }

        public Item Delete(Guid playerId, Guid id)
        {
            var item = repo.DeleteItem(playerId, id);
            return item;
        }
    }
}