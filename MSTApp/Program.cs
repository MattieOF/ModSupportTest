using System;
using MSTGame;

namespace MSTApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string modDir = "mods";
            if (args.Length != 0)
                modDir = args[0];

            Game game = new Game(modDir);
            game.Run();
        }
    }
}
