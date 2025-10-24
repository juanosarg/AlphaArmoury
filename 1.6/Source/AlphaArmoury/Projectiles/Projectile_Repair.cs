using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace AlphaArmoury
{
    public class Projectile_Repair : Projectile_Sinusoid
    {

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            Map map = base.Map;
           // base.Impact(hitThing, blockedByShield);
            Building building = hitThing as Building;
            if (building != null)
            {
                Thing feedbackProjectile = ThingMaker.MakeThing(InternalDefOf.AArmoury_LifestealFeedback);

                Thing feedbackProjectileLaunched = GenSpawn.Spawn(feedbackProjectile, ExactPosition.ToIntVec3().RandomAdjacentCell8Way(), map);
                if (feedbackProjectileLaunched is Projectile piece) piece.Launch(pawn, launcher, launcher, ProjectileHitFlags.All);


            }
        }
    }
}
