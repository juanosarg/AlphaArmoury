using Verse;
using RimWorld;
using Verse.Noise;

namespace AlphaArmoury
{
    public class Projectile_DehydratorShot : Projectile_Explosive
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
                TerrainDef terrainDef = Map.terrainGrid.TopTerrainAt(intVec);
                TerrainDef terrainToDryTo = GetTerrainToDryTo(Map, terrainDef);
                if (terrainToDryTo != null)
                {
                    Map.terrainGrid.SetTerrain(intVec, terrainToDryTo);
                }
                TerrainDef terrainDef2 = Map.terrainGrid.UnderTerrainAt(intVec);
                if (terrainDef2 != null)
                {
                    TerrainDef terrainToDryTo2 = GetTerrainToDryTo(Map, terrainDef2);
                    if (terrainToDryTo2 != null)
                    {
                        Map.terrainGrid.SetUnderTerrain(intVec, terrainToDryTo2);
                    }
                }

            }
            base.Explode();
        }

        public static TerrainDef GetTerrainToDryTo(Map map, TerrainDef terrainDef)
        {
            if (terrainDef.driesTo == null)
            {
                return null;
            }
            if (map.Biome == BiomeDefOf.SeaIce)
            {
                return TerrainDefOf.Ice;
            }
            return terrainDef.driesTo;
        }


    }
}
