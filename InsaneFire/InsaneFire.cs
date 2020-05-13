using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;

namespace InsaneFire
{
    /*[HarmonyPatch(typeof(PLFire), "Update")]
    public static class FireCapFix
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(PLShipInfo), "CountNonNullFires")),
                new CodeInstruction(OpCodes.Ldc_I4_S, 20),
            };

            List<CodeInstruction> injectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(PLShipInfo), "CountNonNullFires")),
                new CodeInstruction(OpCodes.Ldsfld, Global.FireCap),
            };

            return PatchBySequence(instructions, targetSequence, injectedSequence, patchMode: PatchMode.REPLACE);
        }
    }*/
    /*[HarmonyPatch(typeof(PLFire), "Update")]
    public static class o2ConsumptionPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldc_R4, 0.0005f),
            };

            List<CodeInstruction> injectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldsfld, Global.FireCap),
            };

            return PatchBySequence(instructions, targetSequence, injectedSequence, patchMode: PatchMode.REPLACE);
        }
    }*/
    [HarmonyPatch(typeof(PLFire), "Update")]
    class MainUpdatePatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionList = instructions.ToList();

            //fix fire cap
            instructionList[97].opcode = OpCodes.Ldsfld;
            instructionList[97].operand = AccessTools.Field(typeof(Global), "FireCap");

            //instructionList[97].opcode = OpCodes.Ldc_I4 ;
            //instructionList[97].operand = Global.FireCap;

            //fix o2 comsumption
            //instructionList[170].operand = Global.O2Consumption;
            instructionList[170].opcode = OpCodes.Ldsfld;
            instructionList[170].operand = AccessTools.Field(typeof(Global), "O2Consumption");
            
            return instructionList.AsEnumerable();
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
                    if (distance <= 1f)
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
    [HarmonyPatch(typeof(PLFire), "Update")]
    class HasSpreadFix
    {
        static void Postfix(PLFire __instance)
        {
            if (Global.PluginIsOn)
            {
                __instance.HasSpread = false;
            }
        }
    }
}
