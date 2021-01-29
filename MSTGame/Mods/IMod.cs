using System;
using System.Collections.Generic;
using System.Text;

namespace MSTGame.Mods
{
    public interface IMod
    {
        /// <summary>
        /// Name of the mod. Used for mod blocklists, console logging, etc
        /// </summary>
        string ModName { get; }
        
        /// <summary>
        /// Other mods required by this mod
        /// </summary>
        string[] ModDeps { get; }

        /// <summary>
        /// Called when mods are enabled
        /// </summary>
        /// <returns>True if the function completed successfully, false if not</returns>
        bool OnEnable();

        /// <summary>
        /// Called when mods are disabled
        /// </summary>
        /// <returns>True if the function completed successfully, false if not</returns>
        bool OnDisable();
    }
}
