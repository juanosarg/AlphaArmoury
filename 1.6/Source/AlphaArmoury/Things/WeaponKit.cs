using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using static System.Collections.Specialized.BitVector32;

namespace AlphaArmoury
{
    public class WeaponKit : ThingWithComps
    {
        public WeaponTraitDef trait;

        public override bool CanStackWith(Thing other)
        {
            return false;
        }

        public override void PostMake()
        {

            base.PostMake();
            trait = GenerateRandomTrait();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Defs.Look(ref trait, "trait");
        }

        public WeaponTraitDef GenerateRandomTrait()
        {
            if (trait is null)
            {
                List<WeaponTraitDef> allTraits = DefDatabase<WeaponTraitDef>.AllDefsListForReading.Where(x => x.weaponCategory != InternalDefOf.BladeLink && x.abilityProps is null).ToList();

                return allTraits.RandomElement();
            }
            return trait;

        }

        public override string LabelNoCount
        {
            get
            {
                if (trait != null)
                {
                    return this.def.label + ": " + trait.label;
                }
                return base.LabelNoCount;
            }
        }

        public override string GetInspectString()
        {
            string text = base.GetInspectString();

            if (trait != null)
            {
                if (!text.NullOrEmpty())
                {
                    text += "\n";
                }

                text += "AArmoury_Trait".Translate(trait.LabelCap);
            }
            return text;
        }

        public override string DescriptionFlavor => DescriptionDetailed;

        public override string DescriptionDetailed
        {
            get
            {

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine(def.description);
                stringBuilder.AppendLine();

                if (trait != null)
                {

                    stringBuilder.AppendLine(trait.LabelCap.Colorize(ColorLibrary.Yellow));
                    stringBuilder.AppendLine(trait.description);
                    if (!trait.statOffsets.NullOrEmpty())
                    {
                        stringBuilder.Append(trait.statOffsets.Select((StatModifier x) => $"{x.stat.LabelCap} {x.stat.Worker.ValueToString(x.value, finalized: false, ToStringNumberSense.Offset)}").ToLineList(" - "));
                        stringBuilder.AppendLine();
                    }
                    if (!trait.statFactors.NullOrEmpty())
                    {
                        stringBuilder.Append(trait.statFactors.Select((StatModifier x) => $"{x.stat.LabelCap} {x.stat.Worker.ValueToString(x.value, finalized: false, ToStringNumberSense.Factor)}").ToLineList(" - "));
                        stringBuilder.AppendLine();
                    }
                    if (!Mathf.Approximately(trait.burstShotCountMultiplier, 1f))
                    {
                        stringBuilder.AppendLine(string.Format(" - {0} {1}", "StatsReport_BurstShotCountMultiplier".Translate(), trait.burstShotCountMultiplier.ToStringByStyle(ToStringStyle.PercentZero, ToStringNumberSense.Factor)));
                    }
                    if (!Mathf.Approximately(trait.burstShotSpeedMultiplier, 1f))
                    {
                        stringBuilder.AppendLine(string.Format(" - {0} {1}", "StatsReport_BurstShotSpeedMultiplier".Translate(), trait.burstShotSpeedMultiplier.ToStringByStyle(ToStringStyle.PercentZero, ToStringNumberSense.Factor)));
                    }
                    if (!Mathf.Approximately(trait.additionalStoppingPower, 0f))
                    {
                        stringBuilder.AppendLine(string.Format(" - {0} {1}", "StatsReport_AdditionalStoppingPower".Translate(), trait.additionalStoppingPower.ToStringByStyle(ToStringStyle.FloatOne, ToStringNumberSense.Offset)));
                    }
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine("AArmoury_GunsSupportingTrait".Translate().Colorize(ColorLibrary.Yellow));
                    stringBuilder.AppendLine(GunsSupportingTrait(trait));
                }


                return stringBuilder.ToString();
            }
        }


        public string GunsSupportingTrait(WeaponTraitDef trait)
        {
            List<ThingDef> guns = new List<ThingDef>();
            foreach (ThingDef gun in VEF.Weapons.StaticCollectionsClass.uniqueWeaponsInGame)
            {
                CompProperties_UniqueWeapon comp = gun.GetCompProperties<CompProperties_UniqueWeapon>();
                if (comp != null) {
                    if (comp.weaponCategories.Contains(trait.weaponCategory))
                    {
                        guns.Add(gun);
                    }
                             
                }
            }
            return guns.Select(x => x.LabelCap.ToString()).ToLineList("  - ", capitalizeItems: true);
            
        }



        public override IEnumerable<DefHyperlink> DescriptionHyperlinks
        {
            get
            {

                if (trait != null)
                {
                    yield return new DefHyperlink(trait);

                }


            }
        }
    }
}
