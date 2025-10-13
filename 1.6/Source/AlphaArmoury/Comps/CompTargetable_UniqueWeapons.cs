
using RimWorld;
using System.Collections.Generic;
using Verse;
using VEF.Weapons;

namespace AlphaArmoury
{
    public class CompTargetable_UniqueWeapons : CompTargetable
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
            if (target.Thing!=null&&StaticCollectionsClass.uniqueWeaponsInGame.Contains(target.Thing.def))
            {
                return true;
            }
            return false;
        }
    }
}