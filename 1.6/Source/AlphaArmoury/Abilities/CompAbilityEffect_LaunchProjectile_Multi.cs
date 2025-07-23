
using RimWorld;
using Verse;
using static UnityEngine.UI.Image;
namespace AlphaArmoury
{
    public class CompAbilityEffect_LaunchProjectile_Multi : CompAbilityEffect_LaunchProjectile
    {
        public new CompProperties_AbilityLaunchProjectile_Multi Props => (CompProperties_AbilityLaunchProjectile_Multi)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            for (int i = 0; i < Props.count-1; i++) { LaunchProjectile(target, dest); }
           
        }
       

        private void LaunchProjectile(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (Props.projectileDef != null)
            {
                Pawn pawn = parent.pawn;
                IntVec3 forcedMissTarget = GetForcedMissTarget(3, target);
                ((Projectile)GenSpawn.Spawn(Props.projectileDef, pawn.Position, pawn.Map)).Launch(pawn, pawn.DrawPos, forcedMissTarget, target, ProjectileHitFlags.IntendedTarget, parent.verb.preventFriendlyFire);
            }
        }

        public IntVec3 GetForcedMissTarget(float forcedMissRadius,LocalTargetInfo target)
        {
            
            int maxExclusive = GenRadial.NumCellsInRadius(forcedMissRadius);
            int num = Rand.Range(0, maxExclusive);
            return target.Cell + GenRadial.RadialPattern[num];
        }


    }
}
