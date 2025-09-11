using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace AlphaArmoury;

public class AlphaArmoury_Mod : Mod
{
    public AlphaArmoury_Mod(ModContentPack content) : base(content)
    {
      
        settings = GetSettings<AlphaArmoury_Settings>();
    }

    public static AlphaArmoury_Settings settings;

    public override string SettingsCategory()
    {
        return "Alpha Armoury";
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        settings.DoWindowContents(inRect);
    }

}
