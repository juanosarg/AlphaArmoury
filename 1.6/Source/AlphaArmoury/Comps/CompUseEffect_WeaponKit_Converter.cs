using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Linq;
using static UnityEngine.GraphicsBuffer;
using LudeonTK;
using System.Security.Cryptography;
using static UnityEngine.ParticleSystem;
using Verse.Sound;
using static RimWorld.PsychicRitualRoleDef;
using VEF.Weapons;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using Verse.Noise;
using UnityEngine.UIElements;
namespace AlphaArmoury
{
    public class CompUseEffect_WeaponKit_Converter : CompTargetEffect
    {
        public ThingDef weapon;

        public CompProperties_UseEffectWeaponKit_Converter Props => (CompProperties_UseEffectWeaponKit_Converter)props;

        public override void DoEffectOn(Pawn user, Thing thing)
        {
            UniqueConversionExtension extension = thing.def.GetModExtension<UniqueConversionExtension>();       
            
            if (extension != null) {
                if (thing.def == weapon)
                {
                    IntVec3 loc = thing.Position;
                    Map map = thing.Map;
                    QualityCategory quality = thing.TryGetComp<CompQuality>()?.Quality ?? QualityCategory.Normal;
                    thing.Destroy();
                    Thing newWeapon = GenSpawn.Spawn(ThingMaker.MakeThing(extension.uniqueWeaponEquivalent), loc, map);
                    if (newWeapon.def.CanHaveFaction)
                    {
                        newWeapon.SetFaction(user.Faction);
                    }
                    newWeapon.TryGetComp<CompQuality>()?.SetQuality(quality,null);
                }
                else
                {
                    Messages.Message("AArmoury_NotForThisGun".Translate(weapon.LabelCap), thing,
                      MessageTypeDefOf.RejectInput, false);
                    return;
                }
            
            }
            else
            {
                Messages.Message("AArmoury_NoUniqueEquivalent".Translate(thing.LabelCap), thing,
                       MessageTypeDefOf.RejectInput, false);
                return;

            }


            user.carryTracker.DestroyCarriedThing();
        }





    }
}
