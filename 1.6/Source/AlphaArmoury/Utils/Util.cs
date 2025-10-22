using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEF;
using Verse;

namespace AlphaArmoury
{
    public static class Util
    {

        public static bool CanReceiveHypothermia(this Pawn pawn, out HediffDef hypothermiaHediff)
        {
            if (pawn.RaceProps.FleshType == FleshTypeDefOf.Insectoid)
            {
                hypothermiaHediff = InternalDefOf.HypothermicSlowdown;
                return true;
            }

            if (pawn.RaceProps.IsFlesh)
            {
                hypothermiaHediff = HediffDefOf.Hypothermia;
                return true;
            }

            hypothermiaHediff = null;
            return false;
        }


    }
}
