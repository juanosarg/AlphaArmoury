using RimWorld;
using Verse;
using UnityEngine;
using System;

namespace AlphaArmoury
{
    public class Projectile_Chakram : Bullet
    {
        private Vector3 originVec;
        private Vector3 destinationVec;
        private int startingTicksToImpact;

        private Vector3 direction;
        private Vector3 perp1;
        private Vector3 perp2;

        // Cached values
        private float totalDist;
        private float loops;
        private float loopRadius;
        private float forwardCompression;

        private const float LoopBack = 1f; 

        // Precomputed position cache
        private Vector3 currentOffset;

        public override void Launch(Thing launcher, Vector3 origin, LocalTargetInfo usedTarget,
            LocalTargetInfo intendedTarget, ProjectileHitFlags hitFlags, bool preventFriendlyFire = false,
            Thing equipment = null, ThingDef targetCoverDef = null)
        {
            base.Launch(launcher, origin, usedTarget, intendedTarget, hitFlags, preventFriendlyFire, equipment, targetCoverDef);

            // --- Static setup ---
            originVec = origin.Yto0();
            destinationVec = usedTarget.Cell.ToVector3Shifted().Yto0();
            startingTicksToImpact = ticksToImpact;

            // Direction & perpendiculars
            direction = (destinationVec - originVec).normalized;
            perp1 = Vector3.Cross(direction, Vector3.up).normalized;
            if (perp1 == Vector3.zero)
                perp1 = Vector3.Cross(direction, Vector3.forward).normalized;
            perp2 = Vector3.Cross(direction, perp1).normalized;

            // Total distance
            totalDist = (destinationVec - originVec).magnitude;

            // Flight parameters
            float baseLoops = 2.5f; // Base loops for 10-cell range
            loops = baseLoops * (totalDist / 10f);
            loopRadius = 3f;
            forwardCompression = 1f;
        }

        public override Vector3 ExactPosition
        {
            get
            {
                float flightFrac = 1f - (float)ticksToImpact / (float)startingTicksToImpact;

                // --- Dynamic motion ---
                float dynamicRadius = loopRadius * flightFrac; // grows over time
                float baseForward = flightFrac * totalDist * forwardCompression;
                float angle = flightFrac * loops * Mathf.PI * 2f;

                // Slight forward/back wobble to close loops
                float forwardOffset = Mathf.Sin(angle) * dynamicRadius * LoopBack;

                // Circular offset
                Vector3 circularOffset = (Mathf.Cos(angle) * perp1 + Mathf.Sin(angle) * perp2) * dynamicRadius;

                // Combine base + offset
                Vector3 forwardPos = originVec + direction * (baseForward + forwardOffset);
                currentOffset = circularOffset;

                return forwardPos + circularOffset + Vector3.up * def.Altitude;
            }
        }

        protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            // --- Chakram spin only ---
            float flightFrac = 1f - (float)ticksToImpact / (float)startingTicksToImpact;
            float spinRate = 1440f; // degrees per second
            float spinAngle = flightFrac * spinRate;

            Quaternion selfSpin = Quaternion.AngleAxis(spinAngle, Vector3.up);
            Quaternion finalRot = ExactRotation * selfSpin;

            Graphics.DrawMesh(
                MeshPool.plane10,
                Matrix4x4.TRS(ExactPosition, finalRot, Vector3.one),
                def.graphic.MatSingle,
                0
            );
        }
    }
}
