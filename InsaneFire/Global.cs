using PulsarModLoader;
using PulsarModLoader.MPModChecks;
using PulsarModLoader.Utilities;

namespace InsaneFire
{
    public static class Global
    {
        public static SaveValue<bool> ModEnabled = new SaveValue<bool>("ModEnabled", true);

        public static int FireCap = 10000;
        public static SaveValue<int> SavedFireCap = new SaveValue<int>("SavedFireCap", 10000);

        public static float O2Consumption = 0.0005f;
        public static SaveValue<float> SavedO2Consumption = new SaveValue<float>("SavedO2Consumption", 0.0005f);

        public static void Toggle()
        {
            ModEnabled.Value = !ModEnabled.Value;
            if (ModEnabled)
            {
                Disable();
            }
            else
            {
                Enable();
            }
            if (PLServer.Instance != null)
            {
                string message = ModEnabled ? "On" : "Off";
                Messaging.Notification($"Mod is now {message}");
            }
        }


        public static void UpdatePlayerO2()
        {
            foreach (PhotonPlayer player in MPModCheckManager.Instance.NetworkedPeersWithMod(Mod.CachedHarmonyIdent))
            {
                ModMessage.SendRPC(Mod.CachedHarmonyIdent, O2Rate.ModMessageName, player, new object[] { O2Consumption });
            }
        }


        public static void Enable()
        {
            ModEnabled.Value = true;
            FireCap = SavedFireCap;
            O2Consumption = SavedO2Consumption;
            UpdatePlayerO2();
        }


        public static void Disable()
        {
            ModEnabled.Value = false;
            FireCap = 20;
            O2Consumption = 0.0005f;
            UpdatePlayerO2();
        }
    }
}
