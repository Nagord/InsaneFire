using PulsarPluginLoader;

namespace InsaneFire
{
    public class Plugin : PulsarPlugin
    {
        public override string Version => "0.3.0";

        public override string Author => "Dragon";

        public override string ShortDescription => "Makes Fire Insane";

        public override string Name => "InsaneFire";

        public override string HarmonyIdentifier()
        {
            return "Dragon.InsaneFire";
        }
    }
}
