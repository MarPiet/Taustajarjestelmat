using System;
using System.ComponentModel.DataAnnotations;

namespace GameApi.models
{
    public class Item
    {
        public Guid id { get; set; }
        [Range(1, 60)]
        public int Level { get; set; }
        public int Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string Type { get; set; }
    }
    public class NewItem
    {
        public Guid id { get; set; }
        [Range(1, 60)]
        public int Level { get; set; }
        public int Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string Type { get; set; }
    }

    public class ModifiedItem
    {
        public Guid id { get; set; }
        [Range(1, 60)]
        public int Level { get; set; }
        public int Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string Type { get; set; }
    }
}