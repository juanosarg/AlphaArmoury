using HarmonyLib;
using RimWorld;
using VEF;
using Verse;

namespace AlphaArmoury
{
    [HarmonyPatch(typeof(VerbProperties), nameof(VerbProperties.GetForceMissFactorFor))]
    public static class AlphaArmoury_VerbProperties_GetForceMissFactorFor_Patch
    {
        public static void Postfix(Thing equipment, ref float __result)
        {
          
            if (StaticCollections.noMissRadiusWeapons.Contains(equipment))
            {
                __result = 0;
            }
        }
    }
}
