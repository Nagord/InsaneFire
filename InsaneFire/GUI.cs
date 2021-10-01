using PulsarModLoader.CustomGUI;
using static UnityEngine.GUILayout;

namespace InsaneFire
{
    class GUI : ModSettingsMenu
    {
        public override string Name()
        {
            return "InsaneFire Settings";
        }
        string FireCap = Global.FireCap.ToString();
        string O2Consumption = (Global.O2Consumption / .0005f).ToString();
        public override void Draw()
        {
            BeginHorizontal();
            if(Global.PluginIsOn)
            {
                if(Button("Disable"))
                {
                    Global.Disable();
                }
                EndHorizontal();
                BeginHorizontal();
                Label("Fire Capacity", MaxWidth(120));
                FireCap = TextField(FireCap, MaxWidth(100));
                if (int.TryParse(FireCap, out Global.FireCap))
                {
                    Global.SavedFireCap = Global.FireCap;
                    Global.SaveSettings();
                }
                EndHorizontal();
                BeginHorizontal();
                Label("O2 Consumption Rate", MaxWidth(120));
                O2Consumption = TextField(O2Consumption, MaxWidth(100));
                if (float.TryParse(O2Consumption, out float num))
                {
                    Global.O2Consumption = num * .0005f;
                    Global.SavedO2Consumption = Global.O2Consumption;
                    Global.SaveSettings();
                }
                EndHorizontal();
            }
            else
            {
                if (Button("Enable"))
                {
                    Global.Enable();
                }
                EndHorizontal();
            }
        }
    }
}
