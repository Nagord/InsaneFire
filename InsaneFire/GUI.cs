using PulsarModLoader;
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
        string FireCap;
        string O2Consumption;

        public override void OnOpen()
        {
            FireCap = Global.FireCap.ToString();
            O2Consumption= (Global.O2Consumption / .0005f).ToString();
        }

        public override void Draw()
        {
            BeginHorizontal();
            if (Global.ModEnabled)
            {
                if (Button("Disable"))
                {
                    Global.Disable();
                }
                EndHorizontal();
                BeginHorizontal();
                Label("Fire Capacity", MaxWidth(120));
                FireCap = TextField(FireCap, MaxWidth(100));
                if (int.TryParse(FireCap, out int newFireCap) && newFireCap != Global.FireCap)
                {
                    Global.FireCap = newFireCap;
                    Global.SavedFireCap.Value = Global.FireCap;
                }
                EndHorizontal();
                BeginHorizontal();
                Label("O2 Consumption Rate", MaxWidth(120));
                O2Consumption = TextField(O2Consumption, MaxWidth(100));
                if (float.TryParse(O2Consumption, out float newO2Rate) && (newO2Rate * .0005f) != Global.O2Consumption)
                {
                    Global.O2Consumption = newO2Rate * .0005f;
                    Global.SavedO2Consumption.Value = Global.O2Consumption;
                    ModMessage.SendRPC(Mod.CachedHarmonyIdent, O2Rate.ModMessageName, PhotonTargets.Others, new object[] { Global.O2Consumption });
                }
            }
            else
            {
                if (Button("Enable"))
                {
                    Global.Enable();
                    OnOpen();
                }
            }
            EndHorizontal();
        }
    }
}
