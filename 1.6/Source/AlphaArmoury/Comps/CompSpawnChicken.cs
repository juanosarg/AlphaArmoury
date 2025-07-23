
using RimWorld;
using System;
using Verse.AI.Group;
using Verse;
namespace AlphaArmoury
{
    public class CompSpawnChicken : ThingComp
    {

        public CompProperties_SpawnChickenOnDestroyed Props => (CompProperties_SpawnChickenOnDestroyed)props;

        public override void PostDestroy(DestroyMode mode, Map previousMap)
        {
          
           
            PawnKindDef pawnKind = Props.pawnKind;
            Faction faction = parent.Faction;
            
            Pawn pawn = PawnGenerator.GeneratePawn(new PawnGenerationRequest(pawnKind, faction, PawnGenerationContext.NonPlayer, null, fixedBiologicalAge:2));
            GenSpawn.Spawn(pawn, parent.Position, previousMap, WipeMode.VanishOrMoveAside);
            if (CellFinder.TryFindRandomSpawnCellForPawnNear(parent.Position, previousMap, out var result, 5, (IntVec3 c) => GenSight.LineOfSight(parent.Position, c, previousMap, skipFirstCell: true)))
            {
                pawn.rotationTracker.FaceCell(result);
                PawnFlyer pawnFlyer = PawnFlyer.MakeFlyer(ThingDefOf.PawnFlyer_Stun, pawn, result, null, null);
                if (pawnFlyer != null)
                {
                    GenSpawn.Spawn(pawnFlyer, result, previousMap);
                }
            }
            pawn.mindState.mentalStateHandler.TryStartMentalState(InternalDefOf.AArmoury_Manhunter, null, true);
        }
    }
}