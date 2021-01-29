using MSTGame.Mods;

namespace MultipleModImplementationsTest
{
    public class MMI1 : IMod
    {
        public string ModName => "Multiple Mod Implementations Test - Mod 1";

        public string[] ModDeps => new string[] { };

        public bool OnDisable()
        {
            ModUtil.Info("Disabling MMI1");
            return true;
        }

        public bool OnEnable()
        {
            ModUtil.Info("Enabling MMI1");
            return true;
        }
    }
}
