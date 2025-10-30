using RimWorld;
using Verse;
using System.Collections.Generic;

namespace AlphaArmoury

{
    public class ConditionalStatAffecter_WeaponInSpace : ConditionalStatAffecter
    {
        public override string Label => "AArmoury_StatsReport_InSpace".Translate();

        public override bool Applies(StatRequest req)
        {

            Pawn pawn = (req.Thing.ParentHolder as Pawn_EquipmentTracker)?.pawn;
            if (req.HasThing && req.Thing.def.IsWeapon)
            {

                if ((req.Thing.Position != IntVec3.Invalid && req.Thing.Map?.BiomeAt(req.Thing.Position)?.inVacuum == true) ||
                    (pawn?.Map?.BiomeAt(pawn.Position)?.inVacuum == true))
                {
                    return true;
                }


            }
            return false;



        }
    }
}