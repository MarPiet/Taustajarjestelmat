using System.Threading.Tasks;
using System;
using GameApi.models;
namespace GameApi.processors
{
    public class ItemsProcessor
    {
        private readonly IRepository repo;
        Random rnd = new Random();
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
                Price = rnd.Next(1, 1001),
                CreationDate = DateTime.Now,
                id = Guid.NewGuid(),
                Type = item.Type
            };
            if (repo.Get(playerId).Level < itemi.Level)
            {
                throw new LevelException("Player too low level", new LevelException());

            }
            repo.AddItem(playerId, itemi);
            return itemi;

        }


        public Item Modify(Guid playerId, Guid id, ModifiedItem item)
        {
            Item i = new Item();
            i.Level = item.Level;
            i.Price = rnd.Next(1, 1001);
            i.CreationDate = DateTime.Now;
            i.id = id;
            i.Type = item.Type;
            repo.UpdateItem(id, id, i);
            return i;

        }

        public Item Delete(Guid playerId, Guid id)
        {
            var item = repo.DeleteItem(playerId, id);
            return item;
        }

    }
    public class LevelException : Exception
    {
        public LevelException()
        {

        }

        public LevelException(string message) : base(message)
        {

        }
        public LevelException(string message, Exception inner) : base(message, inner)
        {

        }


    }
}