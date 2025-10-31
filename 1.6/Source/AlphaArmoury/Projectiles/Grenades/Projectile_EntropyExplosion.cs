using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanillaPlantsExpanded;
using Verse;

namespace AlphaArmoury
{
    public class Projectile_EntropyExplosion : Projectile_Explosive
    {

        protected override void Explode()
        {

            IntVec3 position = Position;
            float explosionRadius = def.projectile.explosionRadius;

            Pawn pawnLauncher = launcher as Pawn;

            if (pawnLauncher != null) {

                int num = GenRadial.NumCellsInRadius(explosionRadius);
                for (int i = 0; i < num; i++)
                {
                    IntVec3 intVec = position + GenRadial.RadialPattern[i];
                    if (!intVec.InBounds(Map))
                    {
                        continue;
                    }
                    List<Pawn> pawnsToDamage = new List<Pawn>();
                    foreach (Thing victim in intVec.GetThingList(Map))
                    {
                        Pawn pawn = victim as Pawn;

                        if (pawn != null && pawn.psychicEntropy?.IsPsychicallySensitive == true)
                        {
                            if (pawnLauncher.psychicEntropy?.EntropyValue > 0)
                            {
                                pawnsToDamage.Add(pawn);
                                
                            }

                        }

                    }
                    if (pawnsToDamage.Count > 0) { 
                        foreach( Pawn pawn in pawnsToDamage)
                        {
                            var battleLogEntry_RangedImpact = new BattleLogEntry_RangedImpact(pawnLauncher, pawn,
                                intendedTarget.Thing, launcher.def, def, targetCoverDef);
                            Find.BattleLog.Add(battleLogEntry_RangedImpact);
                            var dinfo = new DamageInfo(InternalDefOf.AArmoury_PsychicDamage, 8 * pawnLauncher.psychicEntropy.EntropyRelativeValue, ArmorPenetration, ExactRotation.eulerAngles.y, launcher, null, equipmentDef,
                                DamageInfo.SourceCategory.ThingOrUnknown, intendedTarget.Thing);
                            pawn.TakeDamage(dinfo).AssociateWithLog(battleLogEntry_RangedImpact);
                            pawnLauncher.psychicEntropy.TryAddEntropy(-1f);
                        }
                    }

                }

            }

            
            base.Explode();
        }


    }
}
