using RimWorld;
using UnityEngine;
using Verse;

namespace AlphaArmoury
{
    public class Projectile_Splitting : Bullet
    {
        private bool forkSpawned = false;
        private float lateralAngle;
        private Vector3 customOffset = Vector3.zero;
        private ProjectileHitFlags hitFlagsInt;
        private Vector3 localOrigin;

        public override void Launch(Thing launcher, Vector3 origin, LocalTargetInfo usedTarget, LocalTargetInfo intendedTarget,
            ProjectileHitFlags hitFlags, bool preventFriendlyFire = false, Thing equipment = null, ThingDef targetCoverDef = null)
        {

            base.Launch(launcher, origin, usedTarget, intendedTarget, hitFlags, preventFriendlyFire, equipment, targetCoverDef);
            hitFlagsInt = hitFlags;
            localOrigin = origin;
            float angle = Rand.Range(75f, 90f);
            if (Rand.Chance(0.5f))
                angle = -angle;

            lateralAngle = angle * Mathf.Deg2Rad;
        }

        protected override void Tick()
        {
            base.Tick();

            if (ticksToImpact <= 0)
                return;

            float progress = DistanceCoveredFraction;

          
            if (!forkSpawned)
            {
                forkSpawned = true;
                SpawnForkedProjectile();
            }

          
            Vector3 flatDir = (destination - localOrigin).normalized;
            Vector3 lateral = Vector3.Cross(flatDir, Vector3.up).normalized;
            float curveStrength = Mathf.Sin(progress * Mathf.PI);
            customOffset = lateral * Mathf.Sin(lateralAngle) * curveStrength;
        }

        public override Vector3 DrawPos
        {
            get
            {
                float arcHeight = def.projectile.arcHeightFactor * GenMath.InverseParabola(DistanceCoveredFractionArc);
                Vector3 basePos = localOrigin + (destination - localOrigin) * DistanceCoveredFraction + Vector3.up * arcHeight;
                return basePos + customOffset;
            }
        }

        private void SpawnForkedProjectile()
        {
            Projectile_Splitting_Secondary fork = (Projectile_Splitting_Secondary)GenSpawn.Spawn(DefDatabase<ThingDef>.GetNamed(def+"_Secondary"), Position, Map);
            fork.Launch(launcher,localOrigin,usedTarget,intendedTarget,hitFlagsInt,preventFriendlyFire,equipment,targetCoverDef);

            float currentProgress = DistanceCoveredFraction; 
            fork.InitMirrorAngle(-lateralAngle, currentProgress);
        }
    }

    public class Projectile_Splitting_Secondary : Bullet
    {
        private float lateralAngle;
        private float verticalInvert = 1f;
        private float spawnProgress;      
        private Vector3 localOrigin;
        private Vector3 target;

        public void InitMirrorAngle(float angle, float progressAtSpawn)
        {
            lateralAngle = angle;
            verticalInvert = -1f;
            spawnProgress = progressAtSpawn;
        }

        public override void Launch(Thing launcher, Vector3 origin, LocalTargetInfo usedTarget, LocalTargetInfo intendedTarget,
            ProjectileHitFlags hitFlags, bool preventFriendlyFire = false, Thing equipment = null, ThingDef targetCoverDef = null)
        {
            base.Launch(launcher, origin, usedTarget, intendedTarget, hitFlags, preventFriendlyFire, equipment, targetCoverDef);
            localOrigin = origin;
            target = destination;
        }

        public override Vector3 DrawPos
        {
            get
            {
             
                float progress = spawnProgress + (1f - spawnProgress) * DistanceCoveredFraction;

                float arcHeight = def.projectile.arcHeightFactor * GenMath.InverseParabola(progress) * verticalInvert;

                Vector3 flatDir = (target - localOrigin).normalized;
                Vector3 basePos = localOrigin + flatDir * (target - localOrigin).magnitude * progress + Vector3.up * arcHeight;

             
                Vector3 lateral = Vector3.Cross(flatDir, Vector3.up).normalized;
                float directionFactor = 1f;
                float curveStrength = Mathf.Sin(progress * Mathf.PI);
                Vector3 lateralOffset = lateral * Mathf.Sin(lateralAngle) * curveStrength * directionFactor;

                return basePos + lateralOffset;
            }
        }
    }
}
