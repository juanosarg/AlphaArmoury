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
            AArmoury_Hemovoric_Feedback, AArmoury_Psychic_Feedback, AArmoury_LifestealFeedback;

        public static WeaponTraitDef AArmoury_PsychicResilience;

        [MayRequire("Ludeon.RimWorld.Royalty")]
        public static WeaponCategoryDef BladeLink;

        public static SoundDef Standard_Reload, AArmoury_Cryo_Sustainer;

        public static HediffDef HypothermicSlowdown, VFEP_HypothermicSlowdown;

        public static ThingCategoryDef AArmoury_WeaponKits;

        public static ThingSetMakerDef AArmoury_Reward_UniqueWeaponExpanded;

    }
}