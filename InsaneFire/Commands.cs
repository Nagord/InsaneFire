using PulsarPluginLoader.Chat.Commands;
using PulsarPluginLoader.Utilities;

namespace InsaneFire
{
    class Commands : IChatCommand
    {
        public string[] CommandAliases()
        {
            return new string[] { "insanefire" , "if" };
        }

        public string Description()
        {
            return "controls subcommands.";
        }

        public bool Execute(string arguments)
        {
            string[] Args = arguments.Split(' ');
            bool[] ArgConvertSuccess = new bool[2];
            int[] commandarg = new int[2];
            for(int i = 1; i < 2; i++)
            {
                if (Args.Length > i)
                {
                    ArgConvertSuccess[i] = int.TryParse(Args[i], out commandarg[i]);
                }
            }
            float CommandFloat = 0f;
            if(!ArgConvertSuccess[1])
            {
                ArgConvertSuccess[1] = float.TryParse(Args[1], out CommandFloat);
            }
            switch(Args[0].ToLower())
            {
                case "limit":
                    if(ArgConvertSuccess[1])
                    {
                        Global.FireCap = commandarg[1];
                        Messaging.Notification($"Set fire limit to {commandarg[1]}");
                    }
                    else
                    {
                        Messaging.Notification("Couldn't set fire limit, try using a number. ex: /if limit 30");
                    }
                    break;
                case "o2rate":
                case "o2r":
                    if (ArgConvertSuccess[1])
                    {
                        Global.O2Consumption = CommandFloat * .0005f;
                        string o2percent = (CommandFloat * 100).ToString("000") + "%";
                        Messaging.Notification($"Set O2 consumption to {o2percent}");
                    }
                    else
                    {
                        Messaging.Notification("Couldn't set fire limit, try using a number. ex: /if o2rate .5");
                    }
                    break;
                default:
                    Messaging.Notification("no Subcommand Detected. Subcommands: limit, o2Rate. capitalized letters can be initialized");
                    break;
            }
            return false;
        }

        public string UsageExample()
        {
            return $"{CommandAliases()[0]} ( limit | o2Rate ) (ammount)";
        }
    }
}
