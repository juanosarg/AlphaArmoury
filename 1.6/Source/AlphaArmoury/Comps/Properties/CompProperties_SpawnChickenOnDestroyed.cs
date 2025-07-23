
using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI.Group;
namespace AlphaArmoury
{
    public class CompProperties_SpawnChickenOnDestroyed : CompProperties
    {
        public PawnKindDef pawnKind;

      

        public CompProperties_SpawnChickenOnDestroyed()
        {
            compClass = typeof(CompSpawnChicken);
        }

      
    }
}