
using RimWorld;
using System;
using Verse.AI.Group;
using Verse;
namespace AlphaArmoury
{
    public class CompDeleteOnDrop : ThingComp
    {


        public CompProperties_DeleteOnDrop Props => (CompProperties_DeleteOnDrop)props;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            if (parent.ParentHolder as Pawn_EquipmentTracker == null)
            {
                parent.Destroy();
            }
        }



    }
}