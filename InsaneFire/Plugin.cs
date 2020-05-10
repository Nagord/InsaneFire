using PulsarPluginLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsaneFire
{
    public class Plugin : PulsarPlugin
    {
        protected override string HarmonyIdentifier()
        {
            return "Dragon.InsaneFire";
        }
    }
}
