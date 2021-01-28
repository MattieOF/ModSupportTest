using System;
using MSTGame.Mods;

namespace TestMod
{
    public class TestMod : IMod
    {
        public string ModName => "TestMod";

        public bool OnDisable()
        {
            ModUtil.Info("Disabling TestMod");
            return true;
        }

        public bool OnEnable()
        {
            ModUtil.Info("Initialising TestMod");

            TestItem item = new TestItem();
            item.Test();

            return true;
        }
    }
}
