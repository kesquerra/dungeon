using System;
using Character;

namespace dungeon_crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Player ch = new Character.Player("Kyle");
            Console.WriteLine(ch.details());
        }
    }
}
