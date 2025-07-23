
using RimWorld;
using Verse;

namespace AlphaArmoury
{
    public class CompProperties_AbilityLaunchProjectile_Multi:CompProperties_AbilityLaunchProjectile 
    {
        public int count;

        public CompProperties_AbilityLaunchProjectile_Multi()
        {
            compClass = typeof(CompAbilityEffect_LaunchProjectile_Multi);
        }
    }
}