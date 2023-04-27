using HarmonyLib;
using PulsarModLoader;
using PulsarModLoader.MPModChecks;

namespace InsaneFire
{
    class O2Rate : ModMessage
    {
        public static string ModMessageName = "InsaneFire.O2Rate";
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
            if (PhotonNetwork.isMasterClient && MPModCheckManager.Instance.NetworkedPeerHasMod(newPhotonPlayer, Mod.CachedHarmonyIdent))
            {
                ModMessage.SendRPC(Mod.CachedHarmonyIdent, O2Rate.ModMessageName, newPhotonPlayer, new object[] { Global.O2Consumption });
            }
        }
    }
}
