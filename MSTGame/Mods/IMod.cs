using System;
using System.Collections.Generic;
using System.Text;

namespace MSTGame.Mods
{
    public interface IMod
    {
        string ModName { get; }

        bool OnEnable();
        bool OnDisable();
    }
}
