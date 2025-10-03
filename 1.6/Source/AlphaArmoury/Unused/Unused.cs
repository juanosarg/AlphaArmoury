using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace AlphaArmoury.Unused
{
    /*internal class Unused
    {

        // stored launch data
        private Vector3 originVec;
        private Vector3 destinationVec;
        private int startingTicksToImpact = 1;
        private int ticksExisted = 0;

        // spiral tuning (tweak these)
        public float spiralRate = 0.6f;    // radians per tick (how fast it spins)
        public float spiralGrowth = 0.25f; // how much radius grows per tick (game units/cells per tick)
        public float fixedRadius = 0f;     // if >0, use fixed radius instead of growth
        public float visualScale = 1f;     // extra multiplier if you want bigger/smaller visuals

        // perpendicular basis for spiral plane
        private Vector3 perp1;
        private Vector3 perp2;

        public override void Launch(Thing launcher, Vector3 origin, LocalTargetInfo usedTarget, LocalTargetInfo intendedTarget, ProjectileHitFlags hitFlags, bool preventFriendlyFire = false, Thing equipment = null, ThingDef targetCoverDef = null)
        {
            base.Launch(launcher, origin, usedTarget, intendedTarget, hitFlags, preventFriendlyFire, equipment, targetCoverDef);

            // store flattened origin/destination on XZ so spiral is in a stable plane
            originVec = origin.Yto0();
            destinationVec = usedTarget.Cell.ToVector3Shifted().Yto0();

            // be defensive: ensure we have a non-zero starting tick count
            startingTicksToImpact = Mathf.Max(1, this.ticksToImpact);

            // init tick counter
            ticksExisted = 0;

            // compute forward direction in XZ
            Vector3 forward = destinationVec - originVec;
            if (forward.sqrMagnitude <= 1e-6f)
            {
                // fallback - aim at target raw (no flatten), still defensive
                forward = (usedTarget.CenterVector3 - origin).Yto0();
                if (forward.sqrMagnitude <= 1e-6f) forward = Vector3.forward; // last resort
            }
            forward.Normalize();

            // perpendicular basis: perp1, perp2 (both unit-length, XZ plane oriented)
            perp1 = Vector3.Cross(Vector3.up, forward);
            if (perp1.sqrMagnitude <= 1e-6f)
                perp1 = Vector3.Cross(Vector3.forward, forward); // fallback if collinear with up
            perp1.Normalize();

            perp2 = Vector3.Cross(forward, perp1).normalized;

            // OPTIONAL: debug once so you know Launch ran
            // Log.Message($"[Projectile_Spiral] Launched. startTicks={startingTicksToImpact} forward={forward} perp1={perp1}");
        }

        protected override void Tick()
        {
            // increment our own tick counter first (so ExactPosition at t=1 is after 1 tick)
            ticksExisted++;

            base.Tick();

            // OPTIONAL: small-once debug to see offset values (comment out for release)
            // if (ticksExisted == 1) Log.Message($"[Projectile_Spiral] ticksExisted={ticksExisted}");
        }

        public override Vector3 ExactPosition
        {
            get
            {
                float t = Mathf.Clamp01((float)ticksExisted / (float)startingTicksToImpact);

                Vector3 basePos = Vector3.Lerp(originVec, destinationVec, t);

                float radius = 0.4f;                 // fixed corkscrew radius
                float angle = ticksExisted * spiralRate;

                Vector3 offset = perp1 * Mathf.Cos(angle) * radius
                               + perp2 * Mathf.Sin(angle) * radius;

                return basePos + offset + Vector3.up * def.Altitude;
            }
        }
    }*/



    //FleckMaker.AttachedOverlay(this, DefDatabase<FleckDef>.GetNamed("Fleck_RadialSparks"), Vector3.zero, 0.25f, -1f);
    //FleckMaker.AttachedOverlay(this, DefDatabase<FleckDef>.GetNamed("ShockwaveFast"), Vector3.zero, 0.1f, -1f);
}
