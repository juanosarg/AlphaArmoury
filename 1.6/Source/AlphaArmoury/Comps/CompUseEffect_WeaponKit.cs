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
namespace AlphaArmoury
{
    public class CompUseEffect_WeaponKit : CompTargetEffect
    {


        public CompProperties_UseEffectWeaponKit Props => (CompProperties_UseEffectWeaponKit)props;

        public override void DoEffectOn(Pawn user, Thing thing)
        {
            WeaponKit kit = this.parent as WeaponKit;
            if (kit != null)
            {
                string reason = "";
                CompUniqueWeapon comp = thing.TryGetComp<CompUniqueWeapon>();
                if (comp != null && CanAddTrait(kit.trait, comp, out reason))
                {
                    comp.AddTrait(kit.trait);
                    comp.Setup(fromSave: true);
                    comp.Notify_ColorChanged();
                    CompApplyWeaponTraits compApplyWeaponTraits = thing.TryGetComp<CompApplyWeaponTraits>();
                    compApplyWeaponTraits.DeleteCaches();
                    compApplyWeaponTraits.Notify_ForceRefresh();

                }
                else
                {
                    Messages.Message("AArmoury_CantApply".Translate(kit.trait.LabelCap, thing.LabelCap, reason), thing,
                        MessageTypeDefOf.RejectInput, false);
                    return;
                }

            }
            InternalDefOf.Standard_Reload.PlayOneShot(new TargetInfo(parent.PositionHeld, parent.MapHeld));
            user.carryTracker.DestroyCarriedThing();
        }

        public bool CanAddTrait(WeaponTraitDef trait, CompUniqueWeapon comp, out string reason)
        {

            if (!comp.Props.weaponCategories.Contains(trait.weaponCategory))
            {
                reason = "AArmoury_CantAcceptCategory".Translate();
                return false;
            }
            if (comp.TraitsListForReading.Empty() && !trait.canGenerateAlone)
            {
                reason = "AArmoury_CantGenerateAlone".Translate();
                return false;
            }
            if (!comp.TraitsListForReading.NullOrEmpty())
            {
                foreach (WeaponTraitDef trait2 in comp.TraitsListForReading)
                {
                    if (trait.Overlaps(trait2))
                    {
                        reason = "AArmoury_TraitConflicts".Translate(trait2.LabelCap);
                        return false;
                    }
                }
            }
            reason = "";
            return true;
        }




    }
}
