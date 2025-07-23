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
    public class Projectile_PotatoShot : Projectile_Explosive
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
                if (Map.terrainGrid.TerrainAt(intVec).fertility >= 0.7 && !intVec.GetThingList(Map).Any(x => x.def == ThingDefOf.Plant_Potato))
                {

                    Plant plant = (Plant)ThingMaker.MakeThing(ThingDefOf.Plant_Potato);
                    GenPlace.TryPlaceThing(plant, intVec, Map, ThingPlaceMode.Direct);
                    plant.Growth = 0.0001f;
                    plant.sown = true;
                    Map.mapDrawer.MapMeshDirty(intVec, MapMeshFlagDefOf.Things);
                }
                    
                

            }
            base.Explode();
        }
    }
}
