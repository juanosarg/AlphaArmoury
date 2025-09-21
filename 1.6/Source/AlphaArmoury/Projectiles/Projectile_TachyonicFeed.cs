using System.Collections.Generic;
using RimWorld;
using System.Linq;
using Verse;
namespace AlphaArmoury
{
    public class Projectile_TachyonicFeed : Bullet
    {
        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            Map map = base.Map;
            base.Impact(hitThing, blockedByShield);
            Pawn pawn = hitThing as Pawn;
            if (pawn?.pather!=null)
            {
                float movementSpeed = pawn.pather.MovePercentage;
                if (movementSpeed > 0) {
                    Pawn wielder = launcher as Pawn;
                    bool instigatorGuilty = wielder == null || !wielder.Drafted;
                    DamageInfo dinfo = new DamageInfo(base.DamageDef, DamageAmount * movementSpeed, ArmorPenetration, ExactRotation.eulerAngles.y, launcher, null, equipmentDef, DamageInfo.SourceCategory.ThingOrUnknown, intendedTarget.Thing, instigatorGuilty);
                    BattleLogEntry_RangedImpact battleLogEntry_RangedImpact = new BattleLogEntry_RangedImpact(launcher, hitThing, intendedTarget.Thing, equipmentDef, def, targetCoverDef);
                    hitThing.TakeDamage(dinfo).AssociateWithLog(battleLogEntry_RangedImpact);
                }
                
            }
        }
    }
}