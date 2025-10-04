using UnityEngine;
using Verse;
using RimWorld;

namespace AlphaArmoury
{
    public class Projectile_PsychicBeam : Bullet
    {

        public override Vector3 ExactPosition => destination + Vector3.up * def.Altitude;

        public override void Launch(Thing launcher, Vector3 origin, LocalTargetInfo usedTarget, LocalTargetInfo intendedTarget, ProjectileHitFlags hitFlags, bool preventFriendlyFire = false, Thing equipment = null, ThingDef targetCoverDef = null)
        {
            base.Launch(launcher, origin, usedTarget, intendedTarget, hitFlags, preventFriendlyFire, equipment, targetCoverDef);
            Vector3 offsetA = (ExactPosition - launcher.Position.ToVector3Shifted()).Yto0().normalized * def.projectile.beamStartOffset;
            if (def.projectile.beamMoteDef != null)
            {
                MoteMaker.MakeInteractionOverlay(def.projectile.beamMoteDef, launcher, usedTarget.ToTargetInfo(base.Map), offsetA, Vector3.zero);
            }
            ImpactSomething();
        }

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            Map map = base.Map;
            base.Impact(hitThing, blockedByShield);
            Pawn pawn = hitThing as Pawn;
            if (pawn != null && pawn.RaceProps.Humanlike)
            {
                Thing feedbackProjectile = ThingMaker.MakeThing(InternalDefOf.AArmoury_Psychic_Feedback);

                Thing feedbackProjectileLaunched = GenSpawn.Spawn(feedbackProjectile, ExactPosition.ToIntVec3().RandomAdjacentCell8Way(), map);
                if (feedbackProjectileLaunched is Projectile_PsychicFeedback piece) piece.Launch(pawn, launcher, launcher, ProjectileHitFlags.All);


            }
        }

    }
}