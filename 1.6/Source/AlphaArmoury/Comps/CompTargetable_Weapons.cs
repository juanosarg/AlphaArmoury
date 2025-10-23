
using RimWorld;
using System.Collections.Generic;
using Verse;
using VEF.Weapons;

namespace AlphaArmoury
{
    public class CompTargetable_Weapons : CompTargetable
    {
        protected override bool PlayerChoosesTarget => true;

        protected override TargetingParameters GetTargetingParameters()
        {
            return new TargetingParameters
            {
                canTargetPawns = false,
                canTargetBuildings = false,
                canTargetItems = true,
                mapObjectTargetsMustBeAutoAttackable = false
            };
        }

        public override IEnumerable<Thing> GetTargets(Thing targetChosenByPlayer = null)
        {
            yield return targetChosenByPlayer;
        }

        public override bool ValidateTarget(LocalTargetInfo target, bool showMessages = true)
        {
            if (target.Thing != null && target.Thing.def.IsRangedWeapon && target.Thing.TryGetComp<CompUniqueWeapon>() == null)
            {
                return true;
            }
            return false;
        }
    }
}