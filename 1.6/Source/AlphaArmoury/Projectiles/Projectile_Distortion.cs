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
               
                    
                    
                }
                catch (Exception) { }

            }
        }
    }
}