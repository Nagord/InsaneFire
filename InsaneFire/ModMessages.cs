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
}
