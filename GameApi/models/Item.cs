using System;

namespace GameApi.models
{
    public class Item
    {
        public Guid id { get; set; }
        public int Level { get; set; }
        public int Price { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public class NewItem
    {
        public Guid id { get; set; }
        public int Level { get; set; }
        public int Price { get; set; }
        public DateTime CreationDate { get; set; }
    }

    public class ModifiedItem
    {
        public Guid id { get; set; }
        public int Level { get; set; }
        public int Price { get; set; }
        public DateTime CreationDate { get; set; }
    }
}