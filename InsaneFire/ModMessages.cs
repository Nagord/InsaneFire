using HarmonyLib;
using PulsarPluginLoader;

namespace InsaneFire
{
    class O2Rate : ModMessage
    {
        public override void HandleRPC(object[] arguments, PhotonMessageInfo sender)
        {
            Global.O2Consumption = (float)arguments[0];
        }
    }
    [HarmonyPatch(typeof(PLServer), "LoginMessage")]
    class PlayerConnectedPatch //used to sync the client with the host's settings
    {
        static void Postfix(ref PhotonPlayer newPhotonPlayer)
        {
            if (PhotonNetwork.isMasterClient && ModMessageHelper.Instance.GetPlayerMods(newPhotonPlayer).Contains(ModMessageHelper.Instance.GetModName("InsaneFire")))
            {
                ModMessage.SendRPC("Dragon.InsaneFire", "InsaneFire.O2Rate", newPhotonPlayer, new object[] { Global.O2Consumption });
            }
        }
    }
}
