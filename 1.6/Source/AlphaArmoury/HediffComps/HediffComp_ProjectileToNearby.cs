
using Verse;
using RimWorld;
using System.Linq;

namespace AlphaArmoury
{
    public class HediffComp_ProjectileToNearby : HediffComp
    {

        public HediffCompProperties_ProjectileToNearby Props => (HediffCompProperties_ProjectileToNearby)props;

      
        public override void CompPostTick(ref float severityAdjustment)
        {
            if (this.parent.pawn.IsHashIntervalTick(60))
            {
                var target = NextTarget();
                if (target != null)
                {
                    FireAt(target);
                }

            }
            
        }

        private Thing NextTarget()
        {
            if (this.parent.pawn.Map != null && this.parent.pawn.PositionHeld.InBounds(this.parent.pawn.Map))
            {
                var things = GenRadial.RadialDistinctThingsAround(this.parent.pawn.PositionHeld, this.parent.pawn.Map, Props.radius, false).Where(t => t as Pawn != null);

                things = things.OrderBy(t => t.Position.DistanceTo(this.parent.pawn.Position));
                Pawn target = things.FirstOrDefault() as Pawn;
                if (target != null && !target.Dead) { 
                    return target;
                }
                
            }
            return null;

        }

        public void FireAt(Thing target)
        {
            var projectile = (Projectile)GenSpawn.Spawn(Props.projectile, this.parent.pawn.Position, this.parent.pawn.Map);

            projectile.Launch(this.parent.pawn, target, target, ProjectileHitFlags.IntendedTarget, false, this.parent.pawn);

        }



    }
}
