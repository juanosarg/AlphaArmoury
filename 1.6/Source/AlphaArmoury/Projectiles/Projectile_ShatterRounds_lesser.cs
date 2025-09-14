using System.Collections.Generic;
using RimWorld;
using System.Linq;
using Verse;
using static HarmonyLib.Code;
using UnityEngine;
using VEF.Weapons;
namespace AlphaArmoury
{
    public class Projectile_ShatterRounds_lesser : Bullet
    {
        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            Map map = base.Map;
            for (var i = 0; i < 10; i++)
            {
                var angle = Rand.Range(-180, 180);
                var dist = 6;
                var shrapnel = ThingMaker.MakeThing(InternalDefOf.AArmoury_Bullet_Shrapnel_Lesser);
                var dest = ExactPosition + (Vector3.right * dist).RotatedBy(angle);
                GenSpawn.Spawn(shrapnel, ExactPosition.ToIntVec3().RandomAdjacentCell8Way(), map);
                if (shrapnel is Projectile_ShrapnelPiece piece) piece.Launch(launcher, ExactPosition.ToIntVec3().RandomAdjacentCell8Way().ToVector3(), dest, equipmentDef, equipment);
            }
            base.Impact(hitThing, blockedByShield);




        }
    }
}