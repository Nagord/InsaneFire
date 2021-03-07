using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using static PulsarPluginLoader.Patches.HarmonyHelpers;

namespace InsaneFire
{
    [HarmonyPatch(typeof(PLFire), "Update")]
    class MainUpdatePatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> targetSequence1 = new List<CodeInstruction>()
            {
                //new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(PLShipInfo), "CountNonNullFires")),
                new CodeInstruction(OpCodes.Ldc_I4_S, (sbyte)20),
            };

            List<CodeInstruction> injectedSequence1 = new List<CodeInstruction>()
            {
                //new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(PLShipInfo), "CountNonNullFires")),
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(Global), "FireCap")),
            };

            IEnumerable<CodeInstruction> Modified1st = PatchBySequence(instructions, targetSequence1, injectedSequence1, patchMode: PatchMode.REPLACE);

            List<CodeInstruction> targetSequence2 = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldc_R4, 0.0005f),
            };

            List<CodeInstruction> injectedSequence2 = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(Global), "O2Consumption")),
            };

            return PatchBySequence(Modified1st, targetSequence2, injectedSequence2, patchMode: PatchMode.REPLACE);
        }
        static void Postfix(PLFire __instance)
        {
            if (Global.PluginIsOn)
            {
                __instance.HasSpread = false;
            }
        }
    }
    /*[HarmonyPatch(typeof(PLFire), "Update")]
    public static class SpreadRateFix
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldc_R4, 0.006),
            };

            List<CodeInstruction> injectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldsfld, Global.SpreadRatePercent),
            };

            return PatchBySequence(instructions, targetSequence, injectedSequence, patchMode: PatchMode.REPLACE);
        }
    }*/
    /*[HarmonyPatch(typeof(PLFire), "Update")]
    class SpreadRateFix
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionList = instructions.ToList();

            instructionList[74].operand = Numbers.SpreadRatePercent;

            return instructionList.AsEnumerable();
        }
    }*/
    /*[HarmonyPatch(typeof(PLFire), "Update")]
    public static class FireDamageFix
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldc_R4, 15),
            };

            List<CodeInstruction> injectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldfld, Global.PlayerDamage),
            };

            return PatchBySequence(instructions, targetSequence, injectedSequence, patchMode: PatchMode.REPLACE).ToList();
        }
    }*/
    /*[HarmonyPatch(typeof(PLFire), "Update")]
    class FireDamageFix
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionList = instructions.ToList();

            instructionList[230].operand = Numbers.PlayerDamage;

            return instructionList.AsEnumerable();
        }
    }*/
    [HarmonyPatch(typeof(PLFire), "Spread")]
    class Spreadlocationfix
    {
        static bool Prefix(PLFire __instance)
        {
            PulsarPluginLoader.Utilities.Logger.Info("Spreading");
            if(!Global.PluginIsOn)
            {
                return true;
            }
            bool tryspread = true;
            Vector3 inOffset = new Vector3();
            while (tryspread)
            {
                inOffset = UnityEngine.Random.onUnitSphere * 2f;
                inOffset.y = 0f;
                tryspread = false;
                foreach (PLFire fire in __instance.MyShip.AllFires.Values)
                {
                    float distance = Vector3.Distance(fire.transform.position, inOffset);
                    if (distance <= 1.5f)
                    {
                        tryspread = true;
                        break;
                    }
                }
            }
            if (PLServer.Instance != null)
            {
                PLServer.Instance.CreateFireAtOffset(__instance, inOffset);
            }
            return false;
        }
    }
}
