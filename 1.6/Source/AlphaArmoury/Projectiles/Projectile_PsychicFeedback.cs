using System.Collections.Generic;
using RimWorld;
using System.Linq;
using Verse;
using UnityEngine.UIElements;
using UnityEngine;
using VEF.Weapons;
using System.Security.Cryptography;
using static HarmonyLib.Code;
using System;
namespace AlphaArmoury
{
    public class Projectile_PsychicFeedback : Bullet
    {
       
        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {

            base.Impact(hitThing, blockedByShield);
            Pawn pawn = hitThing as Pawn;
            if (pawn != null && pawn.GetPsylinkLevel()>0)
            {
                pawn.psychicEntropy.OffsetPsyfocusDirectly(0.01f);

            }



        }
    }
}