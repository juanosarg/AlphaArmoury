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

            var traitsRangeField = AccessTools.Field(typeof(CompUniqueWeapon), "NumTraitsRange");
            var changeTraitsAmount = AccessTools.Method(typeof(AlphaArmoury_CompUniqueWeapon_InitializeTraits_Patch), "changeTraitsAmount");
           
          
            for (var i = 0; i < codes.Count; i++)
            {

                if (codes[i].opcode == OpCodes.Ldsfld && codes[i].OperandIs(traitsRangeField)
                  )
                {
                    codes[i].opcode = OpCodes.Call;
                    codes[i].operand = changeTraitsAmount;
                    yield return codes[i];

                }
               
              
                else yield return codes[i];
            }
        }


        public static Verse.IntRange changeTraitsAmount()
        {
            return new IntRange(AlphaArmoury_Settings.minWeaponTraits,AlphaArmoury_Settings.maxWeaponTraits);
        }

    }
}