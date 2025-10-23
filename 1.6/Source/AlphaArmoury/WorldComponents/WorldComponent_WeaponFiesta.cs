using RimWorld;
using Verse;
using System.Collections.Generic;
using RimWorld.Planet;
using AlphaArmoury;
using System.Linq;

namespace AlphaMemes
{
    public class WorldComponent_WeaponFiesta : WorldComponent
    {

        public int tickCounter = 0;
        public int tickInterval = 900000; //1 quadrum

        public static WorldComponent_WeaponFiesta Instance;
        public WorldComponent_WeaponFiesta(World world) : base(world) => Instance = this;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterFiesta", 0, true);

        }

        public override void WorldComponentTick()
        {

            if (AlphaArmoury_Settings.sendWeaponPods)
            {
                tickCounter++;
                LookTargets targets = null;
                if ((tickCounter > tickInterval))
                {
                    int amount = new IntRange(1, 3).RandomInRange;

                    for (int i = 0; i < amount; i++)
                    {
                        Thing thing = ThingMaker.MakeThing(DefDatabase<ThingDef>.AllDefs.Where((ThingDef x) => x.HasComp<CompUniqueWeapon>()).RandomElement());

                        Map map = Find.CurrentMap;
                        
                        if (!map.IsPlayerHome)
                        {
                            map = Find.RandomPlayerHomeMap;
                        }
                        IntVec3 position;
                        Thing caravanSpot = null;
                        List<Thing> caravanSpots = map.listerThings.ThingsOfDef(ThingDefOf.CaravanPackingSpot);
                        if (caravanSpots.Count > 0)
                        {
                            caravanSpot = caravanSpots.RandomElement();
                        }

                        if (caravanSpot == null)
                        {
                            IntVec3 cell = IntVec3.Invalid;
                            RCellFinder.TryFindRandomSpotJustOutsideColony(Find.CameraDriver.MapPosition, map, out cell);
                            position = cell;
                            targets = new LookTargets(thing);
                        }
                        else
                        {
                            position = caravanSpot.Position;
                            targets = new LookTargets(caravanSpot);
                        }
                        DropPodUtility.DropThingsNear(position, map, new List<Thing>() { thing }, 110, false, false, false, false);

                    }
                    Find.LetterStack.ReceiveLetter("AArmoury_FiestaDeliveredLabel".Translate(), "AArmoury_FiestaDelivered".Translate(), LetterDefOf.PositiveEvent, targets);

                    tickCounter = 0;
                }
            }
        }
    }
}
