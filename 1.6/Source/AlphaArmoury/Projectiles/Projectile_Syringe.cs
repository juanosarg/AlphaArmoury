using System.Collections.Generic;
using RimWorld;
using System.Linq;
using Verse;
namespace AlphaArmoury
{
    public class Projectile_Syringe : Bullet
    {
        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            Map map = base.Map;
            base.Impact(hitThing, blockedByShield);
            Pawn pawn = hitThing as Pawn;
            if (pawn != null) { 
            
                List<GeneDef> genesChosen = DefDatabase<GeneDef>.AllDefsListForReading.Where(x => x.canGenerateInGeneSet && x.biostatMet>0 && x.prerequisite is null).InRandomOrder().TakeRandomDistinct(2);
                foreach (GeneDef gene in genesChosen) {
                    pawn.genes?.AddGene(gene,true);
                
                }
            }
        }
    }
}