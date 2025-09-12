using RimWorld;
using UnityEngine;
using Verse;
using System;
using System.Collections.Generic;
using System.Linq;
using AlphaArmoury;



namespace AlphaArmoury
{


    public class AlphaArmoury_Settings : ModSettings

    {

        public const int minWeaponTraitsBase = 1;
        public const int maxWeaponTraitsBase = 3;
        public static int minWeaponTraits = minWeaponTraitsBase;
        public static int maxWeaponTraits = maxWeaponTraitsBase;




        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<int>(ref minWeaponTraits, "minWeaponTraits", minWeaponTraitsBase, true);
            Scribe_Values.Look<int>(ref maxWeaponTraits, "maxWeaponTraits", maxWeaponTraitsBase, true);


        }

        public void DoWindowContents(Rect inRect)
        {
            Listing_Standard ls = new Listing_Standard();       

            ls.Begin(inRect);

            var minLabel = ls.LabelPlusButton("AArmoury_MinWeaponTraits".Translate() + ": " + minWeaponTraits, "AArmoury_MinWeaponTraitsDesc".Translate());
            minWeaponTraits = (int)Math.Round(ls.Slider(minWeaponTraits, 1, maxWeaponTraits-1), 0);

            if (ls.Settings_Button("AArmoury_Reset".Translate(), new Rect(0f, minLabel.position.y + 35, 250f, 29f)))
            {
                minWeaponTraits = minWeaponTraitsBase;
            }

            var maxLabel = ls.LabelPlusButton("AArmoury_MaxWeaponTraits".Translate() + ": " + maxWeaponTraits, "AArmoury_MaxWeaponTraitsDesc".Translate());
            maxWeaponTraits = (int)Math.Round(ls.Slider(maxWeaponTraits, minWeaponTraits+1, 10f), 0);

            if (ls.Settings_Button("AArmoury_Reset".Translate(), new Rect(0f, maxLabel.position.y + 35, 250f, 29f)))
            {
                maxWeaponTraits = maxWeaponTraitsBase;
            }

            ls.End();
          
            Write();
        }
    }
}
