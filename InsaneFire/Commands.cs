using PulsarModLoader;
using PulsarModLoader.Chat.Commands.CommandRouter;
using PulsarModLoader.Utilities;

namespace InsaneFire
{
    class Commands : ChatCommand
    {
        public override string[] CommandAliases()
        {
            return new string[] { "insanefire", "if" };
        }

        public override string Description()
        {
            return "controls subcommands.";
        }

        public override void Execute(string arguments)
        {
            if (!PhotonNetwork.isMasterClient && arguments != "dbg")
            {
                Messaging.Notification("Must be Host to use commands");
            }

            string[] Args = arguments.Split(' ');

            switch (Args[0].ToLower())
            {
                case "limit":
                    if (Args.Length > 1 && int.TryParse(Args[1], out int newFireCap))
                    {
                        Global.SavedFireCap.Value = newFireCap;
                        Global.FireCap = newFireCap;
                        Messaging.Notification($"Set fire limit to {newFireCap}");
                    }
                    else
                    {
                        Messaging.Notification("Couldn't set fire limit, try using a number. ex: /if limit 30");
                    }
                    break;
                case "o2rate":
                case "o2r":
                    if (Args.Length > 1 && float.TryParse(Args[1], out float newO2Rate))
                    {
                        Global.O2Consumption = newO2Rate * .0005f;
                        Global.SavedO2Consumption.Value = Global.O2Consumption;
                        ModMessage.SendRPC(Mod.CachedHarmonyIdent, O2Rate.ModMessageName, PhotonTargets.Others, new object[] { Global.O2Consumption });
                        Messaging.Notification($"Set O2 consumption to {(newO2Rate * 100).ToString("000") + "%"}");
                    }
                    else
                    {
                        Messaging.Notification("Couldn't set o2 comsumption rate, try using a number. ex: /if o2rate .5");
                    }
                    break;
                case "toggle":
                    Global.Toggle();
                    break;
                case "dbg":
                    Messaging.Notification($"Saved: on: {Global.ModEnabled} Firecap: {Global.SavedFireCap} O2Cons: {Global.SavedO2Consumption}\nCurrent: {Global.ModEnabled} {Global.FireCap} {Global.O2Consumption}");
                    break;
                default:
                    Messaging.Notification("no Subcommand Detected. Subcommands: limit, o2Rate, toggle, dbg. capitalized letters can be initialized");
                    break;
            }
        }

        public override string[] UsageExamples()
        {
            return new string[] { $"/{CommandAliases()[0]} ( limit | o2Rate | toggle ) (ammount)" };
        }
    }
}
