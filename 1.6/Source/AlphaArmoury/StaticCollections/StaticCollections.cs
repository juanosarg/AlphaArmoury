
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using VEF.Weapons;
using Verse;
namespace AlphaArmoury
{
    [StaticConstructorOnStartup]
    public static class StaticCollections
    {
        public static List<Thing> noMissRadiusWeapons = new List<Thing>();

        static StaticCollections()
        {
           
        }

        public static void AddNoMissRadiusToList(Thing thing)
        {

            if (!noMissRadiusWeapons.Contains(thing))
            {
                noMissRadiusWeapons.Add(thing);
            }
        }

        public static void RemoveNoMissRadiusFromList(Thing thing)
        {
            if (noMissRadiusWeapons.Contains(thing))
            {
                noMissRadiusWeapons.Remove(thing);
            }

        }

    }
}