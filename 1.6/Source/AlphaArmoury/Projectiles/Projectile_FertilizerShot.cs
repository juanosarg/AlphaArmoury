using Verse;
using RimWorld;
using System;
using UnityEngine;
using UnityEngine.Analytics;
using Verse.Noise;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using VanillaPlantsExpanded;

namespace AlphaArmoury
{
    public class Projectile_FertilizerShot : Projectile_Explosive
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
                bool flag = false;
                foreach (TillableTerrainDef element in DefDatabase<TillableTerrainDef>.AllDefs)
                {
                    foreach (string terrain in element.terrains)
                    {
                        if (Map.terrainGrid.TerrainAt(intVec).defName == terrain)

                        {
                            flag = true;
                        }
                    }
                }
                if (flag)
                {
                    Map.terrainGrid.SetTerrain(intVec,InternalDefOf.VCE_TilledSoil);
                }

            }
            base.Explode();
        }
    }
}
