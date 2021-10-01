using PulsarModLoader;
using PulsarModLoader.Utilities;

namespace InsaneFire
{
    public static class Global
    {
        public static bool PluginIsOn = true;
        public static int FireCap = 10000;
        public static int SavedFireCap = 10000;
        public static float O2Consumption = 0.0005f;
        public static float SavedO2Consumption = 0.0005f;
        public static bool GetSettings(out bool pluginstatesetting, out int firecap, out float o2consumption)
        {
            bool[] worked = new bool[3] { false, false, false};
            string[] settings = PLXMLOptionsIO.Instance.CurrentOptions.GetStringValue("InsaneFireSettings").Split(' ');
            if (settings.Length > 2)
            {
                worked[0] = bool.TryParse(settings[0], out pluginstatesetting);
                worked[1] = int.TryParse(settings[1], out firecap);
                worked[2] = float.TryParse(settings[2], out o2consumption);
            }
            else
            {
                pluginstatesetting = true;
                firecap = 10000;
                o2consumption = 0.0005f;
                Logger.Info("Couldn't load settings for InsaneFire");
            }
            return (worked[0] && worked[1] && worked[2]);
        }
        public static void SaveSettings()
        {
            string settings = $"{PluginIsOn} {SavedFireCap} {SavedO2Consumption}";
            PLXMLOptionsIO.Instance.CurrentOptions.SetStringValue("InsaneFireSettings", settings);
        }

        public static void Toggle()
        {
            PluginIsOn = !PluginIsOn;
            if (PluginIsOn)
            {
                Disable();
            }
            else
            {
                Enable();
            }
            if (PLServer.Instance != null)
            {
                string message = PluginIsOn ? "On" : "Off";
                Messaging.Notification($"Plugin is now {message}");
            }
        }
        public static void UpdatePlayerO2()
        {
            foreach (PhotonPlayer player in ModMessageHelper.Instance.PlayersWithMods.Keys)//players who join after last message do not know new o2consumptionrate
            {
                if (ModMessageHelper.Instance.GetPlayerMods(player).Contains(ModMessageHelper.Instance.GetModName("InsaneFire")))
                {
                    ModMessage.SendRPC("Dragon.InsaneFire", "InsaneFire.O2Rate", player, new object[] { O2Consumption });
                }
            }
        }
        public static void Enable()
        {
            PluginIsOn = true;
            FireCap = SavedFireCap;
            O2Consumption = SavedO2Consumption;
            UpdatePlayerO2();
            SaveSettings();
        }
        public static void Disable()
        {
            PluginIsOn = false;
            FireCap = 20;
            O2Consumption = 0.0005f;
            UpdatePlayerO2();
            SaveSettings();
        }
    }
}
