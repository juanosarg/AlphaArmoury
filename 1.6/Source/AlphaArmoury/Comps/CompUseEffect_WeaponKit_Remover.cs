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
namespace AlphaArmoury
{
    public class CompUseEffect_WeaponKit_Remover : CompTargetEffect
    {


        public CompProperties_UseEffectWeaponKit_Remover Props => (CompProperties_UseEffectWeaponKit_Remover)props;

        public override void DoEffectOn(Pawn user, Thing thing)
        {
            CompUniqueWeapon comp = thing.TryGetComp<CompUniqueWeapon>();

            if (comp != null)
            {
                if (comp.TraitsListForReading.Count > 0)
                {
                    if (Props.doRandom)
                    {
                        WeaponTraitDef trait = comp.TraitsListForReading.RandomElement();
                        comp.TraitsListForReading.Remove(trait);
                        Messages.Message("AArmoury_RemovedTrait".Translate(trait.LabelCap, thing.LabelCap), thing,
                        MessageTypeDefOf.PositiveEvent, false);
                    }
                    if (Props.removeAll)
                    {
                        List<WeaponTraitDef> traitsToRemove = new List<WeaponTraitDef>();

                        foreach (WeaponTraitDef removeTrait in comp.TraitsListForReading)
                        {
                            traitsToRemove.Add(removeTrait);
                        }
                        foreach (WeaponTraitDef traitToRemove in traitsToRemove)
                        {
                            comp.TraitsListForReading.Remove(traitToRemove);
                        }
                    }

                    thing.Notify_ColorChanged();
                    CompApplyWeaponTraits compApplyWeaponTraits = thing.TryGetComp<CompApplyWeaponTraits>();
                    if (compApplyWeaponTraits != null)
                    {
                        compApplyWeaponTraits.DeleteCaches();
                        compApplyWeaponTraits.Notify_ForceRefresh();
                    }
                    
                }
                else
                {

                    Messages.Message("AArmoury_NoTraits".Translate(thing.LabelCap), thing,
                        MessageTypeDefOf.RejectInput, false);
                    return;
                }
            }


            user.carryTracker.DestroyCarriedThing();
        }





    }
}
