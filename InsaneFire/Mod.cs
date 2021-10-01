using PulsarModLoader;

namespace InsaneFire
{
    public class Mod : PulsarMod
    {
        public Mod()
        {
            Global.GetSettings(out Global.PluginIsOn, out Global.FireCap, out Global.O2Consumption);
            Global.SavedFireCap = Global.FireCap;
            Global.SavedO2Consumption = Global.O2Consumption;
        }

        public override string Version => "1.5.0";

        public override string Author => "Dragon";

        public override string ShortDescription => "Makes Fire Insane";

        public override string LongDescription => "-Modifies fire cap from 20 to 10,000." +
            "\n-Makes fire nodes spread more than once, as opposed to once per node, removing the snake effect" +
            "\n-Modifies oxygen consumption per fire." +
            "\n-Syncs oxygen consumption between players with mod." +
            "\n-Provides Commands for customization of the mod" +
            "\n-Allows toggling between moddified fire and vanilla fire" +
            "\n-Saves and loads settings automatically.";

        public override string Name => "InsaneFire";

        public override int MPFunctionality => (int)MPFunction.HostOnly;

        public override string HarmonyIdentifier()
        {
            return "Dragon.InsaneFire";
        }

        public override bool IsEnabled()
        {
            return Global.PluginIsOn;
        }

        public override bool CanBeDisabled()
        {
            return true;
        }

        public override void Disable()
        {
            Global.Disable();
        }

        public override void Enable()
        {
            Global.Enable();
        }
    }
}
