using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using VEF.Abilities;
using Verse;

namespace AlphaArmoury
{

    [HarmonyPatch(typeof(CompUniqueWeapon), "InitializeTraits")]
    public static class AlphaArmoury_CompUniqueWeapon_InitializeTraits_Patch
    {

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var codes = codeInstructions.ToList();

            var myMethod = AccessTools.Method(typeof(IntRange), nameof(IntRange.RandomInRange));
            var changeTraitsAmount = AccessTools.Method(typeof(AlphaArmoury_CompUniqueWeapon_InitializeTraits_Patch), "changeTraitsAmount");

           
            for (var i = 0; i < codes.Count; i++)
            {

                if (codes[i].opcode == OpCodes.Call && codes[i].OperandIs(myMethod)
                  )
                {
                 
                   
                    yield return new CodeInstruction(OpCodes.Call, changeTraitsAmount);

                }
              
                else yield return codes[i];
            }
        }


        public static SoundDef changeTraitsAmount(IntRange range)
        {
           


           
        }

    }
}