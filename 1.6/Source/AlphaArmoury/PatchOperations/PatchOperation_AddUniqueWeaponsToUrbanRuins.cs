using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Verse;

namespace AlphaArmoury
{
    public class PatchOperation_AddUniqueWeaponsToUrbanRuins : PatchOperation
    {

        private PatchOperation match;

        private PatchOperation nomatch;

        protected override bool ApplyWorker(XmlDocument xml)
        {

            if (AlphaArmoury_Settings.addUniquesToAUR)
            {
                if (match != null)
                {
                    return match.Apply(xml);
                }
            }
            else if (nomatch != null)
            {
                return nomatch.Apply(xml);
            }
            return true;
        }


    }
}