using MSTGame.Mods;

namespace MultipleModImplementationsTest
{
    public class MMI2 : IMod
    {
        public string ModName => "Multiple Mod Implementations Test - Mod 2";

        public string[] ModDeps => new string[] { };

        public bool OnDisable()
        {
            ModUtil.Info("Disabling MMI2");
            return true;
        }

        public bool OnEnable()
        {
            ModUtil.Info("Enabling MMI2");
            return true;
        }
    }
}
