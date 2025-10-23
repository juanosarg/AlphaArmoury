using Verse;
using RimWorld;
using System;
using UnityEngine;
using UnityEngine.Analytics;
using Verse.Noise;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using System.Collections.Generic;

namespace AlphaArmoury
{
    public class Projectile_PesticideShot : Projectile_Explosive
    {

        protected override void Explode()
        {


            IntVec3 position = Position;
            float explosionRadius = def.projectile.explosionRadius;

            int num = GenRadial.NumCellsInRadius(explosionRadius);
            List<Thing> blighToDelete = new List<Thing>();
            for (int i = 0; i < num; i++)
            {
                IntVec3 intVec = position + GenRadial.RadialPattern[i];
                if (!intVec.InBounds(Map))
                {
                    continue;
                }
                foreach (Thing victim in intVec.GetThingList(Map))
                {
                    Blight blight = victim as Blight;

                    if (blight != null && !blight.Destroyed)
                    {
                        blighToDelete.Add(blight);
                        

                    }

                }
                if(blighToDelete.Count > 0)
                {
                    foreach(Thing blight in blighToDelete)
                    {
                        blight.Destroy();
                    }
                }

                

            }
            base.Explode();
        }
    }
}
