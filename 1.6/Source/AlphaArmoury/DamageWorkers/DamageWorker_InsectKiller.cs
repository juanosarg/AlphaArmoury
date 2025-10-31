using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace AlphaArmoury
{
    public class DamageWorker_InsectKiller : DamageWorker_AddInjury
    {

        public override DamageResult Apply(DamageInfo dinfo, Thing thing)
        {

            Pawn pawn = thing as Pawn;
            if (pawn != null)
            {
                if (pawn.RaceProps.Insect)
                {
                    return base.Apply(dinfo, thing);
                }
            }
            return new DamageWorker.DamageResult();
        }
    }
}
