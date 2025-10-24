using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.AI;

namespace AlphaArmoury
{
    public class Projectile_Repair : Projectile_Sinusoid
    {

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            Map map = base.Map;
          
            Building building = hitThing as Building;
            if (building != null)
            {
                building.HitPoints+=10;
                building.HitPoints = Mathf.Min(building.HitPoints, building.MaxHitPoints);
                base.Map.listerBuildingsRepairable.Notify_BuildingRepaired(building);
                if (building.HitPoints == building.MaxHitPoints)
                {
                    Pawn launcherPawn = launcher as Pawn;
                    if (launcherPawn != null) {
                        launcherPawn.records.Increment(RecordDefOf.ThingsRepaired);
                        
                    }
                    
                }
               

            }
            Destroy();
        }
    }
}
