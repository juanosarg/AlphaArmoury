using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace AlphaArmoury
{
    public class DamageWorker_AnomalyKiller : DamageWorker_AddInjury
    {

        public override DamageResult Apply(DamageInfo dinfo, Thing thing)
        {

            Pawn pawn = thing as Pawn;
            if (pawn != null)
            {
                if (pawn.RaceProps.IsAnomalyEntity)
                {
                    return base.Apply(dinfo, thing);
                }
            }
            return new DamageWorker.DamageResult();
        }
    }
}
