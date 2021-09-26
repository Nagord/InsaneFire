using PulsarModLoader;
using PulsarModLoader.Chat.Commands.CommandRouter;
using PulsarModLoader.Utilities;

namespace InsaneFire
{
    class Commands : ChatCommand
    {
        public override string[] CommandAliases()
        {
            return new string[] { "insanefire" , "if" };
        }

        public override string Description()
        {
            return "controls subcommands.";
        }

        public override void Execute(string arguments)
        {
            if(!PhotonNetwork.isMasterClient)
            {
                Messaging.Notification("Must be Host to use commands");
            }
            string[] Args = arguments.Split(' ');
            bool ArgConvertSuccess = false;
            bool FloatConvertSuccess = false;
            int CommandArg = 0;
            float CommandFloat = 0f;
            if (Args.Length > 1)
            {
                ArgConvertSuccess = int.TryParse(Args[1], out CommandArg);
                FloatConvertSuccess = float.TryParse(Args[1], out CommandFloat);
            }
            
            switch(Args[0].ToLower())
            {
                case "limit":
                    if(ArgConvertSuccess)
                    {
                        Global.SavedFireCap = CommandArg;
                        Global.FireCap = CommandArg;
                        Global.SaveSettings();
                        Messaging.Notification($"Set fire limit to {CommandArg}");
                    }
                    else
                    {
                        Messaging.Notification("Couldn't set fire limit, try using a number. ex: /if limit 30");
                    }
                    break;
                case "o2rate":
                case "o2r":
                    if (FloatConvertSuccess)
                    {
                        Global.O2Consumption = CommandFloat * .0005f;
                        Global.SavedO2Consumption = Global.O2Consumption;
                        Global.SaveSettings();
                        ModMessage.SendRPC("Dragon.InsaneFire", "InsaneFire.O2Rate", PhotonTargets.Others, new object[] { Global.O2Consumption });//players who join after last message do not know new o2consumptionrate
                        string o2percent = (CommandFloat * 100).ToString("000") + "%";
                        Messaging.Notification($"Set O2 consumption to {o2percent}");
                    }
                    else
                    {
                        Messaging.Notification("Couldn't set o2 comsumption rate, try using a number. ex: /if o2rate .5");
                    }
                    break;
                case "toggle":
                    Global.PluginIsOn = !Global.PluginIsOn;
                    if(Global.PluginIsOn)
                    {
                        Global.FireCap = Global.SavedFireCap;
                        Global.O2Consumption = Global.SavedO2Consumption;
                    }
                    else
                    {
                        Global.FireCap = 20;
                        Global.O2Consumption = 0.0005f;
                    }
                    foreach(PhotonPlayer player in ModMessageHelper.Instance.PlayersWithMods.Keys)//players who join after last message do not know new o2consumptionrate
                    {
                        if (ModMessageHelper.Instance.GetPlayerMods(player).Contains(ModMessageHelper.Instance.GetModName("InsaneFire")))
                        {
                            ModMessage.SendRPC("Dragon.InsaneFire", "InsaneFire.O2Rate", player, new object[] { Global.O2Consumption });
                        }
                    }
                    Global.SaveSettings();
                    string message = Global.PluginIsOn ? "On" : "Off";
                    Messaging.Notification($"Plugin is now {message}");
                    break;
                case "dbg":
                    Global.GetSettings(out bool b, out int i, out float f);
                    Messaging.Notification($"on: {b} Firecap: {i} O2Cons: {f}\nCached: {Global.SavedFireCap} {Global.SavedO2Consumption}\nCurrent: {Global.PluginIsOn} {Global.FireCap} {Global.O2Consumption}");
                    break;
                default:
                    Messaging.Notification("no Subcommand Detected. Subcommands: limit, o2Rate, toggle, dbg. capitalized letters can be initialized");
                    break;
            }
        }

        public override string[] UsageExamples()
        {
            return new string[] {$"/{CommandAliases()[0]} ( limit | o2Rate | toggle ) (ammount)"};
        }
    }
}
