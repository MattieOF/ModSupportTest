using System;
using MSTGame.Mods;
using MSTGame.Logging;

namespace MSTGame
{
    public class Game
    {
        public static string modsDir;
        private volatile static bool shouldClose = false;

        public Game(string modsDir)
        {
            Game.modsDir = modsDir;
        }

        public void Run()
        {
            Log.InitLog();
            Log.Info("Initialising game!");

            ModLoader.InitLoaderAndMods(modsDir);

            while (!shouldClose)
            {
                Console.WriteLine("Enter to stop");
                Console.ReadLine();
                shouldClose = true;
            }

            ModLoader.CloseLoaderAndMods();
            Log.DisposeMainLog();
        }

        public static void Close()
        {
            shouldClose = true;
        }
    }
}
