using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using static PulsarModLoader.Patches.HarmonyHelpers;

namespace InsaneFire
{
    [HarmonyPatch(typeof(PLFire), "Update")]
    class MainUpdatePatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldc_I4_S, (sbyte)20),
            };

            List<CodeInstruction> injectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(Global), "FireCap")),
            };

            instructions = PatchBySequence(instructions, targetSequence, injectedSequence, patchMode: PatchMode.REPLACE);



            targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldc_R4, 0.0005f),
            };

            injectedSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeof(Global), "O2Consumption")),
            };

            return PatchBySequence(instructions, targetSequence, injectedSequence, patchMode: PatchMode.REPLACE);
        }

        static void Postfix(PLFire __instance)
        {
            if (Global.ModEnabled)
            {
                __instance.HasSpread = false;
            }
        }
    }

    [HarmonyPatch(typeof(PLFire), "Spread")]
    class Spreadlocationfix
    {
        static bool Prefix(PLFire __instance)
        {
            if(!Global.ModEnabled)
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
