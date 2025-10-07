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

        public static ThingDef AArmoury_SwarmRocket, AArmoury_Bullet_Shrapnel, AArmoury_Bullet_Shrapnel_Lesser, 
            AArmoury_Hemovoric_Feedback, AArmoury_Psychic_Feedback;

        public static WeaponTraitDef AArmoury_PsychicResilience;

        [MayRequire("Ludeon.RimWorld.Royalty")]
        public static WeaponCategoryDef BladeLink;

    }
}