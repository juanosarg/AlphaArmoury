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
    public class WeaponKit: ThingWithComps
    {
        public WeaponTraitDef trait;


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
            if(trait is null)
            {
                List<WeaponTraitDef> allTraits = DefDatabase<WeaponTraitDef>.AllDefsListForReading.Where(x => x.weaponCategory != InternalDefOf.BladeLink&&x.abilityProps is null).ToList();

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
                    return this.def.label +": "+trait.label;
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
