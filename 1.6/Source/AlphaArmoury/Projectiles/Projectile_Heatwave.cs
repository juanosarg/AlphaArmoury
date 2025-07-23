using Verse;
using RimWorld;
using System;
using UnityEngine;

namespace AlphaArmoury
{
    public class Projectile_Heatwave : Projectile_Explosive
    {

        protected override void Tick()
        {
            base.Tick();
            if (this.IsHashIntervalTick(10))
            {
                try
                {
                   FleckMaker.AttachedOverlay(this, DefDatabase<FleckDef>.GetNamed("Fleck_HeatDiffusion"), Vector3.zero, 0.25f, -1f);
                   
                    
                }
                catch (Exception) { }

            }
        }
    }
}