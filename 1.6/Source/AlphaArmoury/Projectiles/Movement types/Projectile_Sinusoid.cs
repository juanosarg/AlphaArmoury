using UnityEngine;
using Verse;
using RimWorld;
namespace AlphaArmoury
{
    public class Projectile_Sinusoid : Bullet
    {
        // Randomized arc properties per shot
        public float arcDirection;   // +1 or -1 (upward vs downward)
        public float arcStrength;    // random multiplier for arc size

        public override void Launch(Thing launcher, Vector3 origin, LocalTargetInfo usedTarget,
            LocalTargetInfo intendedTarget, ProjectileHitFlags hitFlags, bool preventFriendlyFire = false,
            Thing equipment = null, ThingDef targetCoverDef = null)
        {

            base.Launch(launcher, origin, usedTarget, intendedTarget, hitFlags, preventFriendlyFire, equipment, targetCoverDef);

            // Randomize arc direction:
            arcDirection = Rand.Value < 0.5f ? 1f : -1f;

            // Randomize arc strength:
            arcStrength = Rand.Range(0.5f, 1.5f);
        }

        public override Vector3 ExactPosition
        {
            get
            {

                Vector3 linear = origin.Yto0() + (destination - origin).Yto0() * DistanceCoveredFraction;

                float t = DistanceCoveredFractionArc;

                float arcOffset = def.projectile.arcHeightFactor * arcStrength * GenMath.InverseParabola(t);


                Vector3 dir = (destination - origin).Yto0().normalized;
                Vector3 perp = new Vector3(-dir.z, 0f, dir.x);

                return linear + Vector3.up * def.Altitude + perp * arcOffset * arcDirection;
            }
        }

        public override Quaternion ExactRotation
        {
            get
            {

                return Quaternion.LookRotation((destination - origin).Yto0());
            }
        }

    }
}