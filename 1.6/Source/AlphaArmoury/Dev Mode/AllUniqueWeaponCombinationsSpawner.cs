using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using LudeonTK;
using VEF.Weapons;

namespace AlphaArmoury
{
    public static class AllUniqueWeaponCombinationsSpawner
    {
        private static Map Map
        {
            get
            {
                return Find.CurrentMap;
            }
        }

        [DebugAction("Alpha Armoury", "Make all possible unique weapons", allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void MakeAllItems()
        {
            MakeColonyItems(new ColonyMakerFlag[1]);
        }


        public static void MakeColonyItems(params ColonyMakerFlag[] flags)
        {
            bool godMode = DebugSettings.godMode;
            DebugSettings.godMode = true;
            Thing.allowDestroyNonDestroyable = true;
            if (usedCells == null)
            {
                 usedCells = new BoolGrid( Map);
            }
            else
            {
                 usedCells.ClearAndResizeTo( Map);
            }
             overRect = new CellRect( Map.Center.x - (Map.Size.x / 2) + 100,  Map.Center.z - (Map.Size.z / 2) + 100, Map.Size.x - 200, Map.Size.z - 200);
           
            GenDebug.ClearArea( overRect, Find.CurrentMap);


            List<ThingDef> uniqueWeapons = DefDatabase<ThingDef>.AllDefsListForReading.Where(x => x.GetCompProperties<CompProperties_UniqueWeapon>() != null).ToList();
            List<WeaponTraitDef> traits = DefDatabase<WeaponTraitDef>.AllDefsListForReading.Where(x => x.weaponCategory != InternalDefOf.BladeLink).ToList();

            FillWithItems(overRect, uniqueWeapons,traits);



             ClearAllHomeArea();
             FillWithHomeArea( overRect);
            DebugSettings.godMode = godMode;
            Thing.allowDestroyNonDestroyable = false;
        }

        private static void FillWithItems(CellRect rect, List<ThingDef> itemDefs, List<WeaponTraitDef> traits)
        {
            List<IntVec3> cells = rect.Cells.OrderByDescending(c => c.z).ThenBy(c => c.x).ToList();
            int cellCounter = 0;
            foreach (ThingDef uniqueWeapon in itemDefs)
            {
                CompProperties_UniqueWeapon compProperties = uniqueWeapon.GetCompProperties<CompProperties_UniqueWeapon>();

                foreach (WeaponTraitDef trait in traits)
                {

                    if (CanAddTrait(trait, compProperties))
                    {
                        while (cellCounter < cells.Count &&(cells[cellCounter].x % 7 == 6 || cells[cellCounter].z % 7 == 6))
                        {
                            cellCounter++;
                        }

                        if (cellCounter >= cells.Count)
                            break;

                        Thing weapon = BetterDebugSpawn(uniqueWeapon, cells[cellCounter], -1, true);
                        CompUniqueWeapon comp = weapon.TryGetComp<CompUniqueWeapon>();
                        List< WeaponTraitDef > traitsToRemove = new List< WeaponTraitDef >();
                        foreach (WeaponTraitDef removeTrait in comp.TraitsListForReading)
                        {
                            traitsToRemove.Add(removeTrait);
                        }
                        foreach(WeaponTraitDef traitToRemove in traitsToRemove)
                        {
                            comp.TraitsListForReading.Remove(traitToRemove);
                        }
                       
                        comp.AddTrait(trait);
                        weapon.Notify_ColorChanged();
                        CompApplyWeaponTraits comp2 = weapon.TryGetComp<CompApplyWeaponTraits>();
                        comp2.DeleteCaches();
                        comp2.Notify_ForceRefresh();
                        
                        cellCounter++;
                    }

                }
                
            }

          


        }

        public static bool CanAddTrait(WeaponTraitDef trait, CompProperties_UniqueWeapon comp)
        {
            
            if (!comp.weaponCategories.Contains(trait.weaponCategory))
            {
                return false;
            }
            if (!trait.canGenerateAlone)
            {
                return false;
            }
            
            return true;
        }


     

        public static Thing BetterDebugSpawn(ThingDef def, IntVec3 c, int stackCount = -1, bool direct = false, ThingStyleDef thingStyleDef = null, bool canBeMinified = true, WipeMode? wipeMode = null)
        {
            
            if (stackCount <= 0)
            {
                stackCount = def.stackLimit;
            }
            ThingDef stuff = GenStuff.RandomStuffFor(def);
            Thing thing = ThingMaker.MakeThing(def, stuff);
            if (thingStyleDef != null)
            {
                thing.StyleDef = thingStyleDef;
            }
            thing.TryGetComp<CompQuality>()?.SetQuality(QualityUtility.GenerateQualityRandomEqualChance(), ArtGenerationContext.Colony);
            if (thing.def.Minifiable && canBeMinified)
            {
                thing = thing.MakeMinified();
            }
            if (thing.def.CanHaveFaction)
            {
                if (thing.def.building != null && thing.def.building.isInsectCocoon)
                {
                    thing.SetFaction(Faction.OfInsects);
                }
                else
                {
                    thing.SetFaction(Faction.OfPlayerSilentFail);
                }
            }
            thing.stackCount = stackCount;
            if (wipeMode.HasValue)
            {
                GenSpawn.Spawn(def, c, Find.CurrentMap, wipeMode.Value);
            }
            else
            {
                GenPlace.TryPlaceThing(thing, c, Find.CurrentMap, (!direct) ? ThingPlaceMode.Near : ThingPlaceMode.Direct);
            }
            thing.Notify_DebugSpawned();
            return thing;
        }



     

        private static void ClearAllHomeArea()
        {
            foreach (IntVec3 c in  Map.AllCells)
            {
                 Map.areaManager.Home[c] = false;
            }
        }

        private static void FillWithHomeArea(CellRect r)
        {
            new Designator_AreaHomeExpand().DesignateMultiCell(r.Cells);
        }

        private static CellRect overRect;

        private static BoolGrid usedCells;

    }
}

