
using RimWorld;
using Verse;
namespace AlphaArmoury
{
    public class WeaponTraitWorker_NoForcedMiss : WeaponTraitWorker
    {
        public override void Notify_Equipped(Pawn pawn)
        {
            base.Notify_Equipped(pawn);
            StaticCollections.AddNoMissRadiusToList(pawn.equipment.Primary);
        }

        public override void Notify_EquipmentLost(Pawn pawn)
        {
            base.Notify_EquipmentLost(pawn);
            StaticCollections.RemoveNoMissRadiusFromList(pawn.equipment.Primary);
        }
    } 
       
}