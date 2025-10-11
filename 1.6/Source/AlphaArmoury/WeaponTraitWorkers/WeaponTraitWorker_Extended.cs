using RimWorld;
using Verse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaArmoury
{
    public class WeaponTraitWorker_Extended: WeaponTraitWorker
    {

        public void Notify_Added(Thing thing)
        {
           
            StatDefOf.MaxHitPoints.Worker.ClearCacheForThing(thing);
            thing.HitPoints = thing.MaxHitPoints;
        }


    }
}
