using UnityEngine;
using Verse;
using RimWorld;
namespace AlphaArmoury
{
    public class Projectile_Psychic : Projectile_Sinusoid
    {
       

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            Map map = base.Map;
            base.Impact(hitThing, blockedByShield);
            Pawn pawn = hitThing as Pawn;
            if (pawn != null && pawn.RaceProps.Humanlike)
            {
               

                int amount = 1;
                FeedbackExtension extension = this.def.GetModExtension<FeedbackExtension>();
                if (extension != null)
                {
                    amount = extension.feedbackAmount;
                }
                for (int i = 0; i < amount; i++)
                {
                    Thing feedbackProjectile = ThingMaker.MakeThing(InternalDefOf.AArmoury_Psychic_Feedback);

                    Thing feedbackProjectileLaunched = GenSpawn.Spawn(feedbackProjectile, ExactPosition.ToIntVec3().RandomAdjacentCell8Way(), map);
                    if (feedbackProjectileLaunched is Projectile_PsychicFeedback piece) piece.Launch(pawn, launcher, launcher, ProjectileHitFlags.All);
                }
            }
        }
    }
}