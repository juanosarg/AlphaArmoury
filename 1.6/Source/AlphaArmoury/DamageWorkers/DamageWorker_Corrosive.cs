using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace AlphaArmoury
{
    public class DamageWorker_Corrosive: DamageWorker_AddInjury
    {

        public override DamageResult Apply(DamageInfo dinfo, Thing thing)
        {
            
            Pawn pawn = thing as Pawn;
            if (pawn?.apparel != null)
            {
                foreach(Apparel apparel in pawn.apparel.WornApparel)
                {
                    DamageInfo info2 = dinfo;
                    info2.SetAmount(dinfo.Amount*3);
                    apparel.TakeDamage(info2);

                }
            }
            return base.Apply(dinfo, thing);
        }
    }
}
