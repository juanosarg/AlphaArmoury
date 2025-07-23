using Verse;
using RimWorld;
using System;
using UnityEngine;

namespace AlphaArmoury
{
    public class Projectile_Distortion : Projectile_Explosive
    {

        protected override void Tick()
        {
            base.Tick();
            if (this.IsHashIntervalTick(10))
            {
                try
                {
                    FleckMaker.AttachedOverlay(this, FleckDefOf.PsycastAreaEffect, Vector3.zero, 2f, -1f);
               
                    //FleckMaker.AttachedOverlay(this, DefDatabase<FleckDef>.GetNamed("Fleck_RadialSparks"), Vector3.zero, 0.25f, -1f);
                    //FleckMaker.AttachedOverlay(this, DefDatabase<FleckDef>.GetNamed("ShockwaveFast"), Vector3.zero, 0.1f, -1f);
                    
                }
                catch (Exception) { }

            }
        }
    }
}