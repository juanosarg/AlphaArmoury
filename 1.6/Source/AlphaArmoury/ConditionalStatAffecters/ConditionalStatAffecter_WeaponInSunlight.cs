
using RimWorld;
using Verse;
namespace AlphaArmoury

{
    public class ConditionalStatAffecter_WeaponInSunlight : ConditionalStatAffecter
    {
        public override string Label => "AArmoury_StatsReport_InSunlight".Translate();

        public override bool Applies(StatRequest req)
        {
            Pawn pawn = (req.Thing.ParentHolder as Pawn_EquipmentTracker)?.pawn;
            

            if (req.HasThing && req.Thing.def.IsWeapon)
            {

                if ((req.Thing.Position != IntVec3.Invalid && req.Thing.Map != null && req.Thing.Position.InSunlight(req.Thing.Map)) ||
                    (pawn?.Map != null && pawn.Position.InSunlight(pawn.Map)))
                {
                    return true;
                }


            }


            return false;
        }
    }
}
