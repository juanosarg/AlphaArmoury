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
    public class WeaponConversionKit : ThingWithComps
    {
        public ThingDef weapon;

        public override bool CanStackWith(Thing other)
        {
            return false;
        }

        public override void PostMake()
        {

            base.PostMake();
            weapon = GenerateRandomWeapon();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Defs.Look(ref weapon, "weapon");
        }

        public ThingDef GenerateRandomWeapon()
        {
            if (weapon is null)
            {
                List<ThingDef> allWeapons = DefDatabase<ThingDef>.AllDefsListForReading.Where(x => x.GetModExtension<UniqueConversionExtension>()!=null && x.GetCompProperties<CompProperties_UniqueWeapon>()==null).ToList();

                return allWeapons.RandomElement();
            }
            return weapon;

        }

        public override string LabelNoCount
        {
            get
            {
                if (weapon != null)
                {
                    return this.def.label + ": " + weapon.LabelCap;
                }
                return base.LabelNoCount;
            }
        }

        public override string GetInspectString()
        {
            string text = base.GetInspectString();

            if (weapon != null)
            {
                if (!text.NullOrEmpty())
                {
                    text += "\n";
                }

                text += "AArmoury_Weapon".Translate(weapon.LabelCap);
            }
            return text;
        }

    

        public override IEnumerable<DefHyperlink> DescriptionHyperlinks
        {
            get
            {

                if (weapon != null)
                {
                    yield return new DefHyperlink(weapon);

                }


            }
        }
    }
}
