using PulsarPluginLoader.Utilities;

namespace InsaneFire
{
    public static class Global
    {
        public static bool PluginIsOn = true;
        public static int FireCap = 10000;
        public static int SavedFireCap = 10000;
        //public static float SpreadRatePercent = 0.25f;
        //public static float PlayerDamage = 50;
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
                Messaging.Notification("Couldn't load settings");
            }
            return (worked[0] && worked[1] && worked[2]);
        }
        public static void SaveSettings()
        {
            string settings = $"{PluginIsOn} {SavedFireCap} {O2Consumption}";
            PLXMLOptionsIO.Instance.CurrentOptions.SetStringValue("InsaneFireSettings", settings);
        }
    }
}
