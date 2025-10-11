using HarmonyLib;
using RimWorld;
using System;
using Verse;


namespace AlphaArmoury
{
    [HarmonyPatch(typeof(CompUniqueWeapon), nameof(CompUniqueWeapon.AddTrait))]
    public static class AlphaArmoury_CompUniqueWeapon_AddTrait_Patch
    {
        public static void Postfix(WeaponTraitDef traitDef, CompUniqueWeapon __instance)
        {
            Type type = traitDef.workerClass;
            if (typeof(WeaponTraitWorker_Extended).IsAssignableFrom(type))
            {
                WeaponTraitWorker_Extended workerExtended =
                    (WeaponTraitWorker_Extended)Activator.CreateInstance(type);

                workerExtended.Notify_Added(__instance.parent);
            }
            
        }
    }
}
