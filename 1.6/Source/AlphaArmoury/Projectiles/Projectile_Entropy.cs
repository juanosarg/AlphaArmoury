using System;
using UnityEngine;
using Verse;
using Verse.Sound;
using RimWorld;

namespace AlphaArmoury
{
    public class Projectile_Entropy : Projectile_ZigZag
    {
        public override float Amplitude => 1f;

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            Map map = base.Map;
            base.Impact(hitThing);

            Pawn pawn = launcher as Pawn;
            Pawn victim = hitThing as Pawn;
            if (pawn != null && victim!=null && victim.psychicEntropy?.IsPsychicallySensitive ==true) {

                if (pawn.psychicEntropy?.EntropyValue>0)
                {
                   
                    var battleLogEntry_RangedImpact = new BattleLogEntry_RangedImpact(launcher, victim,
                    intendedTarget.Thing, launcher.def, def, targetCoverDef);
                    Find.BattleLog.Add(battleLogEntry_RangedImpact);
                    var dinfo = new DamageInfo(InternalDefOf.AArmoury_PsychicDamage, DamageAmount* pawn.psychicEntropy.EntropyRelativeValue, ArmorPenetration, ExactRotation.eulerAngles.y, launcher, null, equipmentDef,
                        DamageInfo.SourceCategory.ThingOrUnknown, intendedTarget.Thing);
                    victim.TakeDamage(dinfo).AssociateWithLog(battleLogEntry_RangedImpact);
                    pawn.psychicEntropy.TryAddEntropy(-1f);
                }
            
            }

        }
    }
}