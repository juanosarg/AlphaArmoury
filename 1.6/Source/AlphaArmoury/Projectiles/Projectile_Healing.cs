using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using static HarmonyLib.Code;

namespace AlphaArmoury
{
    public class Projectile_Healing : Bullet
    {

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            Map map = base.Map;
            base.Impact(hitThing, blockedByShield);
            Pawn pawn = hitThing as Pawn;
            if (pawn?.health != null)
            {

                List<Hediff_Injury> injuries = GetInjuries(pawn);
                if (injuries.Count > 0)
                {
                    Hediff_Injury injury = injuries.RandomElement();
                    FleckMaker.ThrowMetaIcon(pawn.Position, pawn.Map, FleckDefOf.HealingCross);
                    injury.Heal(0.5f);
                }
            }
        }

        public List<Hediff_Injury> GetInjuries(Pawn pawn)
        {
            List<Hediff_Injury> injuries = new List<Hediff_Injury>();
            for (int i = 0; i < pawn.health.hediffSet.hediffs.Count; i++)
            {
                if (pawn.health.hediffSet.hediffs[i] is Hediff_Injury hediff_Injury)
                {
                    
                    injuries.Add(hediff_Injury);

                }
            }
            return injuries;
        }
    }
}
