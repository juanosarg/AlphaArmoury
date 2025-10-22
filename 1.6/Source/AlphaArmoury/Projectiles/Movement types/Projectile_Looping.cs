
﻿using RimWorld;
using Verse;
using UnityEngine;
using System;

namespace AlphaArmoury

{
    public class Projectile_Looping : Bullet
 
    {
        private Vector3 originVec;
        private Vector3 destinationVec;
        private int startingTicksToImpact;

        private Vector3 direction;
        private Vector3 perp1;
        private Vector3 perp2;

        public override void Launch(Thing launcher, Vector3 origin, LocalTargetInfo usedTarget,
            LocalTargetInfo intendedTarget, ProjectileHitFlags hitFlags, bool preventFriendlyFire = false,
            Thing equipment = null, ThingDef targetCoverDef = null)
        {
            base.Launch(launcher, origin, usedTarget, intendedTarget, hitFlags, preventFriendlyFire, equipment, targetCoverDef);

            originVec = origin.Yto0();
            destinationVec = usedTarget.Cell.ToVector3Shifted().Yto0();
            startingTicksToImpact = ticksToImpact;

            // Normalize trajectory direction
            direction = (destinationVec - originVec).normalized;

            // Find a perpendicular basis (two vectors perpendicular to direction)
            perp1 = Vector3.Cross(direction, Vector3.up).normalized;
            if (perp1 == Vector3.zero)
                perp1 = Vector3.Cross(direction, Vector3.forward).normalized;

            perp2 = Vector3.Cross(direction, perp1).normalized;
        }



        protected override void DrawAt(Vector3 drawLoc, bool flip = false)
        {
            float frac = 1f - (float)ticksToImpact / (float)startingTicksToImpact;
            float totalDist = (destinationVec - originVec).magnitude;

            // Parameters
            float loops = 5f;          // number of loops before impact
            float loopRadius = 2f;   // loop radius (in cells)
            float forwardCompression = 1f;

            // Progress along trajectory
            float baseForward = frac * totalDist * forwardCompression;
            float angle = frac * loops * Mathf.PI * 2f;

            // Slight forward/back to close loops
            float forwardOffset = Mathf.Sin(angle) * loopRadius * 0.4f;

            // Circular offset (in X/Z plane)
            Vector3 circularOffset = (Mathf.Cos(angle) * perp1 + Mathf.Sin(angle) * perp2) * loopRadius;

            // Combine, keep Y=0 and add altitude at the end
            Vector3 flatOrigin = originVec.Yto0();
            Vector3 flatDir = direction.Yto0();

            Vector3 forwardPos = flatOrigin + flatDir * (baseForward + forwardOffset);
            Vector3 visualPos = forwardPos + circularOffset + Vector3.up * def.Altitude;

            // Draw projectile mesh
            Graphics.DrawMesh(
                MeshPool.plane10,
                Matrix4x4.TRS(visualPos, ExactRotation, new Vector3(1f, 1f, 1f)),
            def.graphic.MatSingle,
                0
            );

        }


    }
}