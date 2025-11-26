using HarmonyLib;
using RimWorld;
using RimWorld.QuestGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using VEF.Abilities;
using Verse;
using static HarmonyLib.Code;

namespace AlphaArmoury
{

    [HarmonyPatch(typeof(QuestNode_Root_AncientMercenaries), "RunInt")]
    public static class AlphaArmoury_QuestNode_Root_AncientMercenaries_RunInt_Patch
    {

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            var codes = codeInstructions.ToList();

            var addequipment = AccessTools.Method(typeof(Pawn_EquipmentTracker), "AddEquipment");
            var addMoreEquipment = AccessTools.Method(typeof(AlphaArmoury_QuestNode_Root_AncientMercenaries_RunInt_Patch), "AddMoreEquipment");
            var addMoreEquipmentToAll = AccessTools.Method(typeof(AlphaArmoury_QuestNode_Root_AncientMercenaries_RunInt_Patch), "AddMoreEquipmentToAll");


            for (var i = 0; i < codes.Count; i++)
            {

                if (codes[i].opcode == OpCodes.Callvirt && codes[i].OperandIs(addequipment)
                  )
                {
                    yield return codes[i];
                    yield return new CodeInstruction(OpCodes.Ldloc, 7);
                    yield return new CodeInstruction(OpCodes.Ldloc, 2);
                    yield return new CodeInstruction(OpCodes.Call, addMoreEquipment);

                }
                else if (codes[i].opcode == OpCodes.Stloc_S && codes[i].operand is LocalBuilder lb && lb.LocalIndex == 8
                  )
                {
                    yield return codes[i];
                    yield return new CodeInstruction(OpCodes.Ldloc, 8);
                    yield return new CodeInstruction(OpCodes.Ldloc, 2);
                    yield return new CodeInstruction(OpCodes.Call, addMoreEquipmentToAll);
                    yield return new CodeInstruction(OpCodes.Stloc_S, 8);
                }

                else { yield return codes[i]; }

            }
        }


        public static void AddMoreEquipment(Pawn pawn, float num)
        {
            int numberOfTools = new IntRange(2, 4).RandomInRange;
            for (int i = 0; i < numberOfTools; i++)
            {

                ThingWithComps thing = (ThingWithComps)ThingMaker.MakeThing(DefDatabase<ThingDef>.AllDefs.Where(x => x.thingCategories?.Contains(InternalDefOf.AArmoury_WeaponKits) == true).RandomElementByWeight(x => x.GetModExtension<WeaponKitExtension>().commonality));
                pawn.inventory.innerContainer.TryAdd(thing);
            }


        }
        public static IEnumerable<Pawn> AddMoreEquipmentToAll(IEnumerable<Pawn> collection, float num)
        {
            if (AlphaArmoury_Settings.addWeaponsToMoreMercs || AlphaArmoury_Settings.addKitsToMoreMercs)
            {
                List<Pawn> originalList = collection.ToList();
                List<Pawn> modifiedList = new List<Pawn>();
                for (int i = 0; i < originalList.Count(); i++)
                {
                    ThingWithComps weaponToAdd = null;
                    ThingWithComps kitToAdd = null;

                    if (AlphaArmoury_Settings.addWeaponsToMoreMercs)
                    {
                        ThingSetMakerParams thingSetMakerParams = default(ThingSetMakerParams);
                        thingSetMakerParams.makingFaction = Faction.OfAncientsHostile;
                        thingSetMakerParams.countRange = new IntRange(1, 1);
                        thingSetMakerParams.totalMarketValueRange = new FloatRange(0.7f, 1.3f) * QuestTuning.PointsToRewardMarketValueCurve.Evaluate(num);
                        ThingSetMakerParams parms = thingSetMakerParams;
                        List<Thing> list = ThingSetMakerDefOf.Reward_UniqueWeapon.root.Generate(parms);
                        if (list.Count != 1)
                        {
                            Log.Error("Expected 1 unique weapon, got " + list.Count);
                        }
                        weaponToAdd = list.First() as ThingWithComps;
                    }
                    if (AlphaArmoury_Settings.addKitsToMoreMercs)
                    {

                        kitToAdd = (ThingWithComps)ThingMaker.MakeThing(DefDatabase<ThingDef>.AllDefs.Where(x => x.thingCategories?.Contains(InternalDefOf.AArmoury_WeaponKits) == true).RandomElementByWeight(x => x.GetModExtension<WeaponKitExtension>().commonality));
                        

                    }
                                     
                    originalList[i].equipment.DestroyAllEquipment();
                    if (weaponToAdd != null) { originalList[i].equipment.AddEquipment(weaponToAdd); }
                    if (kitToAdd != null) { originalList[i].inventory.innerContainer.TryAdd(kitToAdd); }
                    
                    modifiedList.Add(originalList[i]);
                }
                return modifiedList;
            }
            return collection;
            


        }

    }
}