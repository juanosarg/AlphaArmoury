using Verse;
using RimWorld;
using System;
using UnityEngine;
using UnityEngine.Analytics;
using Verse.Noise;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using VanillaPlantsExpanded;
using static HarmonyLib.Code;

namespace AlphaArmoury
{
    public class Projectile_MagmaShot : Projectile_Explosive
    {

        protected override void Explode()
        {


            IntVec3 position = Position;
            float explosionRadius = def.projectile.explosionRadius;

            int num = GenRadial.NumCellsInRadius(explosionRadius);
            for (int i = 0; i < num; i++)
            {
                IntVec3 intVec = position + GenRadial.RadialPattern[i];
                if (!intVec.InBounds(Map))
                {
                    continue;
                }
                if (!Map.terrainGrid.TerrainAt(intVec).IsWater)
                {
                    Map.terrainGrid.SetTempTerrain(intVec, TerrainDefOf.LavaShallow);
                    Map.tempTerrain.QueueRemoveTerrain(intVec, Find.TickManager.TicksGame + 60000);
                }


            }
            base.Explode();
        }
    }
}
