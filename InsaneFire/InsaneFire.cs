using Harmony;
using PPL.CommonExtensions.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using static PPL.CommonExtensions.Patches.HarmonyHelpers;

namespace InsaneFire
{
    public static class Numbers
        {
        public static int FireCap = 10000;
        public static float SpreadRatePercent = 0.25f;
    //    public static float PlayerDamage = 50;
        }
    //[HarmonyPatch(typeof(PLFire), "Update")]
    //public static class FireCapFix
    //{
    //    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    //    {
    //        List<CodeInstruction> targetSequence = new List<CodeInstruction>()
    //{
    //    new CodeInstruction(OpCodes.Ldc_I4_S, 20),
    //};

    //        List<CodeInstruction> injectedSequence = new List<CodeInstruction>()
    //{
    //    new CodeInstruction(OpCodes.Ldc_I4, Numbers.FireCap),
    //};

    //        return HarmonyHelpers.PatchBySequence(instructions, targetSequence, injectedSequence, patchMode: PatchMode.REPLACE).ToList();
    //    }
    //}
    [HarmonyPatch(typeof(PLFire), "Update")]
    class FireCapFix
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionList = instructions.ToList();

            instructionList[85].opcode = OpCodes.Ldc_I4;
            instructionList[85].operand = Numbers.FireCap;

            return instructionList.AsEnumerable();
        }
    }
    //[HarmonyPatch(typeof(PLFire), "Update")]
    //public static class SpreadRateFix
    //{
    //    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    //    {
    //        List<CodeInstruction> targetSequence = new List<CodeInstruction>()
    //{
    //    new CodeInstruction(OpCodes.Ldc_R4, 0.006),
    //};

    //        List<CodeInstruction> injectedSequence = new List<CodeInstruction>()
    //{
    //    new CodeInstruction(OpCodes.Ldc_R4, Numbers.SpreadRatePercent),
    //};

    //        return HarmonyHelpers.PatchBySequence(instructions, targetSequence, injectedSequence, patchMode: PatchMode.REPLACE).ToList();
    //    }
    //}
    [HarmonyPatch(typeof(PLFire), "Update")]
    class SpreadRateFix
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> instructionList = instructions.ToList();

            instructionList[74].operand = Numbers.SpreadRatePercent;

            return instructionList.AsEnumerable();
        }
    }
    //[HarmonyPatch(typeof(PLFire), "Update")]
    //public static class FireDamageFix
    //{
    //    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    //    {
    //        List<CodeInstruction> targetSequence = new List<CodeInstruction>()
    //{
    //    new CodeInstruction(OpCodes.Ldc_R4, 15),
    //};

    //        List<CodeInstruction> injectedSequence = new List<CodeInstruction>()
    //{
    //    new CodeInstruction(OpCodes.Ldc_R4, Numbers.PlayerDamage),
    //};

    //        return HarmonyHelpers.PatchBySequence(instructions, targetSequence, injectedSequence, patchMode: PatchMode.REPLACE).ToList();
    //    }
    //}
    //[HarmonyPatch(typeof(PLFire), "Update")]
    //class FireDamageFix
    //{
    //    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    //    {
    //        List<CodeInstruction> instructionList = instructions.ToList();

    //        instructionList[230].operand = Numbers.PlayerDamage;

    //        return instructionList.AsEnumerable();
    //    }
    //}
}
