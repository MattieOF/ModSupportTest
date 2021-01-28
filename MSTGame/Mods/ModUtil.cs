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
    }
}
