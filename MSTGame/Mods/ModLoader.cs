using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using MSTGame.Logging;

namespace MSTGame.Mods
{
    internal class ModLoader
    {
        public static List<IMod> mods = new List<IMod>();
        public static List<string> modNames = new List<string>();
        public static List<string> blacklistedMods = new List<string>();

        /// <summary>
        /// Loads mods, loads the mods blocklist and enables mods (while skipping those in the blocklist)
        /// </summary>
        /// <param name="modsPath">Path to load mods from</param>
        public static void InitLoaderAndMods(string modsPath)
        {
            LoadMods(modsPath);
            LoadModBlacklist();
            EnableMods();
        }

        /// <summary>
        /// Disable all mods and save the mod blocklist
        /// </summary>
        public static void CloseLoaderAndMods()
        {
            DisableMods();
            SaveModBlacklist();
        }

        /// <summary>
        /// Check the path given for assembly files with one or more types that implement IMod. These types are also added to the static mods list
        /// </summary>
        /// <param name="path">The directory to check for mod files</param>
        /// <returns>A list containing all assemblies that contain an IMod implementation</returns>
        public static List<Assembly> GetValidAssemblies(string path)
        {
            List<Assembly> assemblies = new List<Assembly>();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return assemblies;
            }

            foreach (string dir in Directory.GetFiles(path, "*.dll"))
            {
                Assembly assembly = Assembly.LoadFrom(dir);
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IMod).IsAssignableFrom(type))
                    {
                        IMod mod = (IMod) Activator.CreateInstance(type);
                        mods.Add(mod);
                        if (assemblies.Contains(assembly)) Log.Warning($"Assembly {assembly.GetName().Name} has multiple mod implementations.\n" +
                            $"This is fine, but ALL mod implementations will use the prefix [{assembly.GetName().Name}] for logging.", "MODLOADER");
                        else assemblies.Add(assembly);
                    }
                }
            }

            return assemblies;
        }

        /// <summary>
        /// Calls GetValidAssemblies with the provided path and also logs the mod files found
        /// </summary>
        /// <param name="modPath">Path to look for mods in</param>
        public static void LoadMods(string modPath = "mods")
        {
            List<string> modNames = new List<string>();
            foreach (Assembly assembly in GetValidAssemblies(modPath))
            {
                modNames.Add(assembly.Location);
            }

            if (modNames.Count != 0)
                Log.Info($"Found mod files: {string.Join(", ", modNames)}", "MODLOADER");
        }

        /// <summary>
        /// Checks each mod agaisnt the mod blocklist (if found on the blocklist, it is skipped
        /// and removed from the static mods list), and each mod has it's OnEnable function called
        /// </summary>
        public static void EnableMods()
        {
            List<IMod> modsToRemove = new List<IMod>();
            foreach (IMod mod in mods)
            {
                if (blacklistedMods.Contains(mod.ModName))
                {
                    Log.Info($"Mod with name \"{mod.ModName}\" found in mod blocklist, not enabled and removed from mod list.", "MODLOADER");
                    modsToRemove.Add(mod);
                    continue;
                }
                mod.OnEnable();
                modNames.Add(mod.ModName);
                // TODO Handle return false (failed init)
            }

            foreach (IMod mod in modsToRemove) mods.Remove(mod);
        }

        /// <summary>
        /// Calls the OnDisable mod on every loaded mod and clears the static mod list
        /// </summary>
        public static void DisableMods()
        {
            foreach (IMod mod in mods)
            {
                mod.OnDisable();
                // TODO Handle return false (failed disable)
            }
            ClearMods();
        }

        /// <summary> 
        /// <para>Loads the mod blocklist from the given file and adds all entries to a string list</para>
        /// <para>If the blocklist file doesn't exist, it is created</para>
        /// </summary>
        /// <param name="path">The file path for the blocklist txt</param>
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
                Log.Info($"Loaded mod blocklist: {string.Join(", ", blacklistedMods)}", "MODLOADER");
            }

            stream.Close();
            stream.Dispose();
        }

        /// <summary>
        /// <para>Saves the blocklist to the given path</para>
        /// <para>Format: each line is the modname for a blocklisted mod</para>
        /// </summary>
        /// <param name="path">Path to save the blocklist to</param>
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

        /// <summary>
        /// Adds mod.ModName to the mod blocklist
        /// </summary>
        /// <param name="mod">Mod to add to the blocklist</param>
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

        public static void ClearMods()
        {
            mods.Clear();
            modNames.Clear();
        }

    }
}
