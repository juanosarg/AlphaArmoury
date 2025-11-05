using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEF.Abilities;
using VEF.Weapons;
using Verse;
using static HarmonyLib.Code;

namespace AlphaArmoury
{
    public class Projectile_Grapple : LaserBeam
    {

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            Map map = base.Map;
           
            Pawn launcherAsPawn = launcher as Pawn;
            Pawn pawn = hitThing as Pawn;
            IntVec3 position = Position;


            if (pawn != null)
            {
               

                DoJump(pawn, launcherAsPawn);

            }
            else
            {
             
                
                DoJump(launcherAsPawn, position);

            }
            base.Impact(hitThing, blockedByShield);
        }

        public static void DoJump(Pawn pawn, LocalTargetInfo currentTarget,LocalTargetInfo target = default(LocalTargetInfo), ThingDef pawnFlyerOverride = null)
        {
            
           
            IntVec3 position = pawn.Position;
            IntVec3 cell = currentTarget.Cell;
            Map map = pawn.Map;
            bool flag = Find.Selector.IsSelected(pawn);
            PawnFlyer pawnFlyer = PawnFlyer.MakeFlyer(pawnFlyerOverride ?? ThingDefOf.PawnFlyer, pawn, cell,null, null, true, null, null, target);
            if (pawnFlyer != null)
            {
                FleckMaker.ThrowDustPuff(position.ToVector3Shifted() + Gen.RandomHorizontalVector(0.5f), map, 2f);
                GenSpawn.Spawn(pawnFlyer, cell, map);
                if (flag)
                {
                    Find.Selector.Select(pawn, playSound: false, forceDesignatorDeselect: false);
                }
                
            }
           
        }


    }
}
