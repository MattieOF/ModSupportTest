using System.Collections.Generic;
using System.Reflection;
using MSTGame.Logging;

namespace MSTGame.Mods
{
    public static class ModUtil
    {
        public static void Trace(string msg)
        {
            string modName = Assembly.GetCallingAssembly().GetName().Name;
            Log.Trace(msg, modName);
        }

        public static void Info(string msg)
        {
            string modName = Assembly.GetCallingAssembly().GetName().Name;
            Log.Info(msg, modName);
        }

        public static void Warning(string msg)
        {
            string modName = Assembly.GetCallingAssembly().GetName().Name;
            Log.Warning(msg, modName);
        }

        public static void Error(string msg)
        {
            string modName = Assembly.GetCallingAssembly().GetName().Name;
            Log.Error(msg, modName);
        }

        public static void Fatal(string msg)
        {
            string modName = Assembly.GetCallingAssembly().GetName().Name;
            Log.Fatal(msg, modName);
        }

        /// <summary>
        /// Get the mod list as a list of strings
        /// </summary>
        /// <returns>Returns the names of mods as a list of strings</returns>
        public static List<string> GetModList()
        {
            return ModLoader.modNames;
        }

        /// <summary>
        /// Get the mod list as a string array
        /// </summary>
        /// <returns>Returns the names of mods as a string array</returns>
        public static string[] GetModListArray()
        {
            return ModLoader.modNames.ToArray();
        }

        /// <summary>
        /// Returns the mod in the mod list with the name given
        /// </summary>
        /// <param name="modName">Name to find a mod with</param>
        /// <returns></returns>
        public static IMod GetMod(string modName)
        {
            return ModLoader.mods.Find(m => m.ModName == "ModName");
        }
    }
}
