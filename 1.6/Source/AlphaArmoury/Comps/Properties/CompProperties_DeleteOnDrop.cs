
using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI.Group;
namespace AlphaArmoury
{
    public class CompProperties_DeleteOnDrop : CompProperties
    {
  
        public CompProperties_DeleteOnDrop()
        {
            compClass = typeof(CompDeleteOnDrop);
        }


    }
}