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

        public static void LoadModBlacklist(string path = "mods/blacklist.txt")
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
            foreach (IMod mod in mods)
            {
                mod.OnEnable();
                // TODO Handle return false (failed init)
            }
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
    }
}
