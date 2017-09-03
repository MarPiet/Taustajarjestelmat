using System;
namespace GameApi.models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }

    public class NewPlayer
    {
        public Guid Id { get; set; }

        public string Name {get; set;}
    }

        public class ModifiedPlayer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}