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

        public static List<Assembly> GetValidAssemblies(string path)
        {
            List<Assembly> assemblies = new List<Assembly>();

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            foreach (string dir in Directory.GetFiles(path, "*.dll"))
            {
                Assembly assembly = Assembly.LoadFrom(dir);
                bool foundModType = false;
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IMod).IsAssignableFrom(type))
                    {
                        if (foundModType)
                        {
                            Log.Error($"Mod file {assembly.FullName} has more than one type implementing IMod. Future implementations will be ignored.", "MODLOADER");
                            continue;
                        }

                        IMod mod = (IMod) Activator.CreateInstance(type);
                        mods.Add(mod);
                        assemblies.Add(assembly);
                        foundModType = true;
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
            foreach (IMod mod in mods)
            {
                mod.OnEnable();
            }
        }

        public static void DisableMods()
        {
            foreach (IMod mod in mods)
            {
                mod.OnDisable();
            }
            mods.Clear();
        }
    }
}
