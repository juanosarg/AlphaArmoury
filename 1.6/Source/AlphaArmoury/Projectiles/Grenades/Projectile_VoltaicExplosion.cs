using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanillaPlantsExpanded;
using Verse;

namespace AlphaArmoury
{
    public class Projectile_VoltaicExplosion : Projectile_Explosive
    {

        protected override void Explode()
        {

            IntVec3 position = Position;
            float explosionRadius = def.projectile.explosionRadius;

            int num = GenRadial.NumCellsInRadius(explosionRadius);
            for (int i = 0; i < num; i++)
            {
                IntVec3 intVec = position + GenRadial.RadialPattern[i];
                if (!intVec.InBounds(Map))
                {
                    continue;
                }
                foreach (Thing victim in intVec.GetThingList(Map))
                {
                    Pawn pawn = victim as Pawn;

                    if (pawn != null)
                    {
                        if (!pawn.health.hediffSet.HasHediff(InternalDefOf.AArmoury_Electric))
                        {
                            pawn.health.AddHediff(InternalDefOf.AArmoury_Electric);
                        }

                        Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.AArmoury_Electric);
                        hediff.Severity += 0.2f;

                    }




                }

            }
            base.Explode();
        }


    }
}
