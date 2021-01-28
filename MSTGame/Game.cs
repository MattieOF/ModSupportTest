using System;
using MSTGame.Mods;
using MSTGame.Logging;

namespace MSTGame
{
    public class Game
    {
        public static string modsDir;
        private static bool shouldClose = false;

        public Game(string modsDir)
        {
            Game.modsDir = modsDir;
        }

        public void Run()
        {
            Log.InitLog();
            Log.Info("Initialising game!");

            ModLoader.LoadMods(modsDir);
            ModLoader.EnableMods();

            while (!shouldClose)
            {
                Console.WriteLine("Enter to stop");
                Console.ReadLine();
                shouldClose = true;
            }

            ModLoader.DisableMods();
            Log.DisposeMainLog();
        }

        public static void Close()
        {
            shouldClose = true;
        }
    }
}
