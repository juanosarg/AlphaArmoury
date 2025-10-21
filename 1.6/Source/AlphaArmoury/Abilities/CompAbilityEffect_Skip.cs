
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace AlphaArmoury
{
    public class CompAbilityEffect_Skip : CompAbilityEffect_Teleport
    {

        public new CompProperties_AbilityTeleport Props => (CompProperties_AbilityTeleport)props;

        public override string ExtraLabelMouseAttachment(LocalTargetInfo target)
        {
            return CanSkipTarget(target).Reason;
        }

        private AcceptanceReport CanSkipTarget(LocalTargetInfo target)
        {
            Pawn pawn;
            if ((pawn = target.Thing as Pawn) != null)
            {
                if (parent.pawn == pawn)
                {
                    return "AArmoury_NeedToTargetNotCaster".Translate();
                }
            }
            return true;
        }

        public override bool Valid(LocalTargetInfo target, bool showMessages = true)
        {
            Pawn pawn;
            if ((pawn = target.Thing as Pawn) != null)
            {

                if (parent.pawn == pawn)
                {
                    Messages.Message("AArmoury_NeedToTargetNotCaster".Translate(), pawn, MessageTypeDefOf.RejectInput, historical: false);
                    return false;
                }
            }
            return true;

        }


    }
}