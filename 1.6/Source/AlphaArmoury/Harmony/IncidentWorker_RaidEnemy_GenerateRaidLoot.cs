using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using VEF;
using Verse;

namespace AlphaArmoury
{
    [HarmonyPatch(typeof(IncidentWorker_RaidEnemy), "GenerateRaidLoot")]
    public static class AlphaArmoury_IncidentWorker_RaidEnemy_GenerateRaidLoot_Patch
    {
        public static void Postfix(IncidentParms parms, List<Pawn> pawns)
        {
            if (AlphaArmoury_Settings.addWeaponsToAllRaids)
            {
                foreach (Pawn pawn in pawns)
                {

                    ThingSetMakerParams thingSetMakerParams = default(ThingSetMakerParams);
                    thingSetMakerParams.makingFaction = Faction.OfAncientsHostile;
                    thingSetMakerParams.countRange = new IntRange(1, 1);
                    thingSetMakerParams.techLevel = parms.faction.def.techLevel;
                    ThingSetMakerParams parms2 = thingSetMakerParams;
                    List<Thing> list = InternalDefOf.AArmoury_Reward_UniqueWeaponExpanded.root.Generate(parms2);
                    if (list.Count != 1)
                    {
                        Log.Error("Expected 1 unique weapon, got " + list.Count);
                    }
                    ThingWithComps thingWithComps = list.First() as ThingWithComps;

                    if (AlphaArmoury_Settings.makeRaidWeaponsBiocoded)
                    {
                        CompBiocodable compBiocodable = thingWithComps.TryGetComp<CompBiocodable>();
                        if (compBiocodable != null && !compBiocodable.Biocoded)
                        {
                            compBiocodable.CodeFor(pawn);
                        }
                    }

                    pawn.equipment.DestroyAllEquipment();
                    pawn.equipment.AddEquipment(thingWithComps);

                }
            }
           
            
        }
    }
}
