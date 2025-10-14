using RimWorld;
using Verse;
namespace AlphaArmoury
{
    public class CompProperties_UseEffectWeaponKit_Remover : CompProperties_UseEffect
    {
        public bool doRandom = false;
        public bool removeAll = false;

        public CompProperties_UseEffectWeaponKit_Remover()
        {
            compClass = typeof(CompUseEffect_WeaponKit_Remover);
        }
    }
}
