using System.Collections.Generic;
using RimWorld;
using System.Linq;
using Verse;
using UnityEngine.UIElements;
using UnityEngine;
using VEF.Weapons;
using System.Security.Cryptography;
using static HarmonyLib.Code;
namespace AlphaArmoury
{
    public class Projectile_HemovoricFeedback : Projectile_SinusoidWithPerlin
    {
       
        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            
            base.Impact(hitThing, blockedByShield);
            Pawn pawn = hitThing as Pawn;
            if (pawn != null && pawn.IsBloodfeeder())
            {
                GeneUtility.OffsetHemogen(pawn, 0.01f);

            }


                
        }
    }
}