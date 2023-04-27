using HarmonyLib;
using System;

namespace InsaneFire
{
    [HarmonyPatch(typeof(PhotonNetwork), "CreateRoom", new Type[] { typeof(string), typeof(RoomOptions), typeof(TypedLobby), typeof(string[])})]
    internal class LoadSettingsOnGameStart
    {
        static void Postfix()
        {
            if (Global.ModEnabled)
            {
                Global.FireCap = Global.SavedFireCap;
                Global.O2Consumption = Global.SavedO2Consumption;
            }
            else
            {
                Global.FireCap = 20;
                Global.O2Consumption = 0.0005f;
            }
        }
    }
}
