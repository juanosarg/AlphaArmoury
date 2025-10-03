using System.Collections.Generic;
using RimWorld;
using System.Linq;
using Verse;
using UnityEngine.UIElements;
using UnityEngine;
using VEF.Weapons;
using System.Security.Cryptography;
using static HarmonyLib.Code;
namespace AlphaArmoury
{
    public class Projectile_HemovoricFeedback : Bullet
    {
        // Perlin parameters
        float perlinScale = 2.5f;
        float ampLat = 2f;  // lateral amplitude
        float ampVert = 1f;   // vertical amplitude
      

        protected new float DistanceCoveredFraction
        {
            get
            {
                if (StartingTicksToImpact <= 0f) return 1f;
                return Mathf.Clamp01(1f - (float)ticksToImpact / (float)StartingTicksToImpact);
            }
        }

        static Vector3 FlattenY(Vector3 v) => new Vector3(v.x, 0f, v.z);

        public override Vector3 ExactPosition
        {
            get
            {
               
                float t = DistanceCoveredFraction;
                Vector3 start = FlattenY(origin);
                Vector3 end = FlattenY(destination);

                Vector3 forward = (end - start);
                float totalDist = forward.magnitude;
                if (totalDist <= 0.0001f) return origin + Vector3.up * def.Altitude;

                Vector3 dir = forward / totalDist;
                Vector3 basePosXZ = start + dir * (totalDist * t);
                Vector3 right = Vector3.Cross(Vector3.up, dir).normalized;

                float seed = 0.5f;
                float sampleX = seed + t * perlinScale;
                float sampleY = seed + 100f + t * perlinScale; 

                float perlin1 = Mathf.PerlinNoise(sampleX, 0f) - 0.5f; 
                float perlin2 = Mathf.PerlinNoise(sampleY, 0f) - 0.5f;

                float lateralOffset = ampLat * perlin1 * 2f;
                float verticalOffset = ampVert * perlin2 * 2f;

                return basePosXZ + right * lateralOffset + Vector3.up * (def.Altitude + verticalOffset);
            }
        }
        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            
            base.Impact(hitThing, blockedByShield);
            Pawn pawn = hitThing as Pawn;
            if (pawn != null && pawn.IsBloodfeeder())
            {
                GeneUtility.OffsetHemogen(pawn, 0.01f);

            }


                
        }
    }
}