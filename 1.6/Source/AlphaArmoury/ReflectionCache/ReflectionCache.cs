using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace AlphaArmoury
{
    public class ReflectionCache
    {

        public static readonly AccessTools.FieldRef<CompEquippableAbility, Ability> ability =
          AccessTools.FieldRefAccess<CompEquippableAbility, Ability>(AccessTools.Field(typeof(CompEquippableAbility), "ability"));

        public static readonly AccessTools.FieldRef<ThingWithComps, List<ThingComp>> comps =
         AccessTools.FieldRefAccess<ThingWithComps, List<ThingComp>>(AccessTools.Field(typeof(ThingWithComps), "comps"));

    }
}
