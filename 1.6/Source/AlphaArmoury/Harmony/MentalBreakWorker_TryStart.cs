using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse.AI;
using Verse;
using VEF.Weapons;
using RimWorld;

namespace AlphaArmoury
{
    [HarmonyPatch]
    public static class AlphaArmoury_MentalWorker_Patches
    {
        [HarmonyTargetMethods]
        public static IEnumerable<MethodBase> TargetMethods()
        {
            var targetMethod = AccessTools.DeclaredMethod(typeof(MentalBreakWorker), "TryStart");
            yield return targetMethod;
            foreach (var subclass in typeof(MentalBreakWorker).AllSubclasses())
            {
                var method = AccessTools.DeclaredMethod(subclass, "TryStart");
                if (method != null)
                {
                    yield return method;
                }
            }
        }

        public static bool Prefix(ref bool __result, MentalBreakWorker __instance, Pawn __0, string __1, bool __2)
        {
            if (__0.Drafted && __0.equipment.Primary != null && StaticCollectionsClass.uniqueWeaponsInGame.Contains(__0.equipment.Primary.def))
            {
                CompUniqueWeapon comp = __0.equipment.Primary.GetComp<CompUniqueWeapon>();
                if (comp != null)
                {
                    foreach (WeaponTraitDef item in comp.TraitsListForReading)
                    {
                        if (item == InternalDefOf.AArmoury_PsychicResilience)
                        {
                            __result = false;
                            return false;
                        }
                    }
                }
               
            }
            return true;
        }
    }
}
