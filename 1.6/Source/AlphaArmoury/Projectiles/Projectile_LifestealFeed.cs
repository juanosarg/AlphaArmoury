using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace AlphaArmoury
{
    public class Projectile_LifestealFeed: Projectile_Sinusoid
    {

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            Map map = base.Map;
            base.Impact(hitThing, blockedByShield);
            Pawn pawn = hitThing as Pawn;
            if (pawn?.health != null)
            {

                List<Hediff_Injury> injuries = Projectile_Healing.GetInjuries(pawn);
                if (injuries.Count > 0)
                {
                    Hediff_Injury injury = injuries.RandomElement();
                    FleckMaker.ThrowMetaIcon(pawn.Position, pawn.Map, FleckDefOf.HealingCross);
                    injury.Heal(0.5f);
                }
            }
        }


    }
}
