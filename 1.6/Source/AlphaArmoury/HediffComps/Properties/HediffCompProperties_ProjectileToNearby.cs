using System.Collections.Generic;
using Verse;
namespace AlphaArmoury
{
    public class HediffCompProperties_ProjectileToNearby : HediffCompProperties
    {
        public float radius;
        public ThingDef projectile;


        public HediffCompProperties_ProjectileToNearby()
        {
            compClass = typeof(HediffComp_ProjectileToNearby);
        }
    }
}
