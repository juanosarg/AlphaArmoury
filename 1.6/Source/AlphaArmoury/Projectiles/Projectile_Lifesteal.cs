using System.Collections.Generic;
using RimWorld;
using System.Linq;
using Verse;
using UnityEngine.UIElements;
using UnityEngine;
using VEF.Weapons;
using System.Security.Cryptography;
namespace AlphaArmoury
{
    public class Projectile_Lifesteal : Bullet
    {
        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            Map map = base.Map;
            base.Impact(hitThing, blockedByShield);
            Pawn pawn = hitThing as Pawn;
            if (pawn != null)
            {
                int amount = 1;
                FeedbackExtension extension = this.def.GetModExtension<FeedbackExtension>();
                if (extension != null)
                {
                    amount = extension.feedbackAmount;
                }
                for (int i = 0; i < amount; i++)
                {
                    Thing feedbackProjectile = ThingMaker.MakeThing(InternalDefOf.AArmoury_LifestealFeedback);

                    Thing feedbackProjectileLaunched = GenSpawn.Spawn(feedbackProjectile, ExactPosition.ToIntVec3().RandomAdjacentCell8Way(), map);
                    if (feedbackProjectileLaunched is Projectile piece) piece.Launch(pawn, launcher, launcher, ProjectileHitFlags.All);
                }


                


            }
        }


    }

}



