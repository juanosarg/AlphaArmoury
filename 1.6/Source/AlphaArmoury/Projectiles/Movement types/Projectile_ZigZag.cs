using RimWorld;
using Verse;
using UnityEngine;

namespace AlphaArmoury
{
    public class Projectile_ZigZag : Bullet
    {
        private Vector3 originVec;
        private Vector3 destinationVec;
        private int startingTicksToImpact;

        private Vector3 direction;
        private Vector3 perp1;

        private float totalDist;
        private float zigCount;
        private float amplitude;

        public override void Launch(Thing launcher, Vector3 origin, LocalTargetInfo usedTarget,
            LocalTargetInfo intendedTarget, ProjectileHitFlags hitFlags, bool preventFriendlyFire = false,
            Thing equipment = null, ThingDef targetCoverDef = null)
        {
            base.Launch(launcher, origin, usedTarget, intendedTarget, hitFlags, preventFriendlyFire, equipment, targetCoverDef);

            originVec = origin.Yto0();
            destinationVec = usedTarget.Cell.ToVector3Shifted().Yto0();
            startingTicksToImpact = ticksToImpact;

            direction = (destinationVec - originVec).normalized;

            // Find perpendicular basis (just one is enough for zig-zag)
            perp1 = Vector3.Cross(direction, Vector3.up).normalized;
            if (perp1 == Vector3.zero)
                perp1 = Vector3.Cross(direction, Vector3.forward).normalized;

            totalDist = (destinationVec - originVec).magnitude;

            // Parameters — constant across distance
            zigCount = Mathf.Max(3f, totalDist * 0.6f); // how many turns
            amplitude = 1.2f; // constant width of zigzag in cells
        }

        public override Vector3 ExactPosition
        {
            get
            {
                float frac = 1f - (float)ticksToImpact / startingTicksToImpact;
                Vector3 forwardPos = originVec + direction * (frac * totalDist);

                // --- Zig-zag offset ---
                float segmentFrac = frac * zigCount;
                int segmentIndex = Mathf.FloorToInt(segmentFrac);
                float localT = segmentFrac - segmentIndex;

                // Alternate side per segment
                bool upward = (segmentIndex % 2 == 0);
                float offsetAmount = Mathf.Lerp(-amplitude, amplitude, upward ? localT : 1f - localT);

                Vector3 offset = perp1 * offsetAmount;

                return forwardPos + offset + Vector3.up * def.Altitude;
            }
        }

        protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            // No spin, just draw it at its rotated trajectory
            Graphics.DrawMesh(
                MeshPool.plane10,
                Matrix4x4.TRS(ExactPosition, ExactRotation, Vector3.one),
                def.graphic.MatSingle,
                0
            );
        }
    }
}
