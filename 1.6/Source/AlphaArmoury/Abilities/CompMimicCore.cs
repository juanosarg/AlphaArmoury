
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using VEF.Weapons;
using static RimWorld.PsychicRitualRoleDef;

namespace AlphaArmoury
{
    public class CompMimicCore : CompAbilityEffect
    {

        public new CompProperties_MimicCore Props => (CompProperties_MimicCore)props;


        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);

            ThingWithComps weaponInGround = target.Thing as ThingWithComps;
            CompUniqueWeapon compInGround = weaponInGround?.TryGetComp<CompUniqueWeapon>();

            if (compInGround != null)
            {
                CompUniqueWeapon comp = this.parent.pawn.equipment?.Primary.TryGetComp<CompUniqueWeapon>();
                if (comp != null)
                {
                    List<WeaponTraitDef> traitsToRemove = new List<WeaponTraitDef>();

                    foreach (WeaponTraitDef removeTrait in comp.TraitsListForReading)
                    {
                        if (removeTrait != InternalDefOf.AArmoury_MimicCore)
                        {
                            traitsToRemove.Add(removeTrait);
                        }

                    }
                    foreach (WeaponTraitDef traitToRemove in traitsToRemove)
                    {
                        comp.TraitsListForReading.Remove(traitToRemove);
                    }

                    foreach (WeaponTraitDef addTrait in compInGround.TraitsListForReading)
                    {
                        if (addTrait != InternalDefOf.AArmoury_MimicCore)
                        {
                            if (comp.CanAddTrait(addTrait))
                            {
                                comp.AddTrait(addTrait);
                            }
                            
                        }

                    }

                }

                this.parent.pawn.equipment?.Primary.Notify_ColorChanged();
                CompApplyWeaponTraits compApplyWeaponTraits = this.parent.pawn.equipment?.Primary.TryGetComp<CompApplyWeaponTraits>();
                compApplyWeaponTraits?.DeleteCaches();
                compApplyWeaponTraits?.Notify_ForceRefresh();

                
            }
            


        }

        public override bool Valid(LocalTargetInfo target, bool showMessages = true)
        {
            if (target.Thing != null && StaticCollectionsClass.uniqueWeaponsInGame.Contains(target.Thing.def))
            {
                return true;
            }
            return false;

        }


    }
}