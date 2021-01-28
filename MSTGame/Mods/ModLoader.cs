using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MSTGame.Logging;

namespace MSTGame.Mods
{
    internal class ModLoader
    {
        public static List<IMod> mods = new List<IMod>();
        public static List<string> blacklistedMods = new List<string>();

        public static List<Assembly> GetValidAssemblies(string path)
        {
            List<Assembly> assemblies = new List<Assembly>();

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            foreach (string dir in Directory.GetFiles(path, "*.dll"))
            {
                Assembly assembly = Assembly.LoadFrom(dir);
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IMod).IsAssignableFrom(type))
                    {
                        IMod mod = (IMod) Activator.CreateInstance(type);
                        mods.Add(mod);
                        assemblies.Add(assembly);
                    }
                }
            }

            return assemblies;
        }

        public static void LoadMods(string modPath = "mods")
        {
            List<string> modNames = new List<string>();
            foreach (Assembly assembly in GetValidAssemblies(modPath))
            {
                modNames.Add(assembly.Location);
            }

            Log.Info($"Found mod files: {string.Join(", ", modNames)}");
        }

        public static void EnableMods()
        {
            List<IMod> modsToRemove = new List<IMod>();
            foreach (IMod mod in mods)
            {
                if (blacklistedMods.Contains(mod.ModName))
                {
                    Log.Info($"Mod with name {mod.ModName} found in mod blocklist, not enabled and removed from mod list.");
                    modsToRemove.Add(mod);
                    continue;
                }
                mod.OnEnable();
                // TODO Handle return false (failed init)
            }

            foreach (IMod mod in modsToRemove) mods.Remove(mod);
        }

        public static void DisableMods()
        {
            foreach (IMod mod in mods)
            {
                mod.OnDisable();
                // TODO Handle return false (failed disable)
            }
            mods.Clear();
        }

        public static void LoadModBlacklist(string path = "mods/blocklist.txt")
        {
            if (!File.Exists(path))
            {
                File.CreateText(path).Close();
                return;
            }

            StreamReader stream = File.OpenText(path);
            string line;
            while ((line = stream.ReadLine()) != null)
            {
                blacklistedMods.Add(line);
            }

            if (blacklistedMods.Count != 0)
            {
                Log.Info($"Loaded mod blacklist: {string.Join(", ", blacklistedMods)}");
            }
        }

        public static void SaveModBlacklist(string path = "mods/blocklist.txt")
        {
            StreamWriter writer = File.CreateText(path);
            foreach (string modName in blacklistedMods)
                writer.WriteLine(modName);

            writer.Flush();
            writer.Close();
            writer.Dispose();
        }

        public static void AddModToBlacklist(string modName)
        {
            blacklistedMods.Add(modName);
        }

        public static void AddModToBlacklist(IMod mod)
        {
            blacklistedMods.Add(mod.ModName);
        }

        public static void RemoveModFromBlacklist(string modName)
        {
            blacklistedMods.Add(modName);
        }

        public static void RemoveModFromBlacklist(IMod mod)
        {
            blacklistedMods.Add(mod.ModName);
        }

    }
}
