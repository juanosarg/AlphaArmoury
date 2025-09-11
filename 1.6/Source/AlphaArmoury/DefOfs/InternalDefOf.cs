using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
using System.Net;

namespace AlphaArmoury
{

    [DefOf]
    public static class InternalDefOf
    {
        static InternalDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
        }


        [MayRequire("VanillaExpanded.VPlantsE")]
        public static TerrainDef VCE_TilledSoil;

        public static MentalStateDef AArmoury_Manhunter;

        public static ThingDef AArmoury_SwarmRocket;

        public static WeaponTraitDef AArmoury_PsychicResilience;

    }
}