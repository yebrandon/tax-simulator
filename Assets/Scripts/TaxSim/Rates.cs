using System;
using System.Collections.Generic;

namespace TaxSim
{
    public class Rates
    {
        public static float cppRate = 0.0495F;
        public static float cppExemption = 3500F;
        public static float cppMaximumContribution = 2593.80F;

        public static float eiRate = 0.0166F;
        public static float eiMaximumContribution = 858.22F;

        public static float baseFederalExemption = 12069F;
        public static float baseProvincialExemption = 10582F;

        public static float maxRRSPContributionRate = 0.18F;
        public static float maxRRSPContribution = 27230F;

        public static Tuple<int, float>[] federalTaxLevels =
        {
            Tuple.Create( 47630, 0.15F ),
            Tuple.Create( 95259, 0.205F ),
            Tuple.Create( 147667, 0.26F ),
            Tuple.Create( 210371, 0.29F ),
            Tuple.Create( int.MaxValue, 0.33F )
        };

        public static Tuple<int, float>[] provincialTaxLevels =
        {
            Tuple.Create( 43906, 0.0505F ),
            Tuple.Create( 87813, 0.0915F ),
            Tuple.Create( 150000, 0.1116F ),
            Tuple.Create( 220000, 0.1216F ),
            Tuple.Create( int.MaxValue, 0.1316F )
        };

        public static float ComputeProvincialTax (float taxableIncome)
        {
            return ComputeProgressiveTax(taxableIncome, provincialTaxLevels);
        }

        public static float ComputeFederalTax (float taxableIncome)
        {
            return ComputeProgressiveTax(taxableIncome, federalTaxLevels);
        }

        private static float ComputeProgressiveTax (float taxableIncome, Tuple<int, float>[] taxTable)
        {
            float totalTax = 0;

            // Find index of Tuple where upper bound of tax bracket is greater than income.
            int topRealizedBracket = 0;
            for (; topRealizedBracket < taxTable.Length; topRealizedBracket++)
            {
                if (!(taxTable[topRealizedBracket].Item1 < taxableIncome))
                {
                    break;
                }

            }

            // Add all tax brackets where full taxable amount is paid
            for (int index = 0; index < topRealizedBracket; index++)
            {
                if (index == 0)
                    totalTax += taxTable[index].Item1 * taxTable[index].Item2;
                else
                    totalTax += (taxTable[index].Item1 - taxTable[index - 1].Item1) * taxTable[index].Item2;
            }

            // Compute tax for top tax bracket the taxpayer reaches where only part of the bracket is realized
            int alreadyTaxedIncome = 0;
            if (topRealizedBracket > 0)
            {
                alreadyTaxedIncome = taxTable[topRealizedBracket - 1].Item1;
            }


            totalTax += taxTable[topRealizedBracket].Item2 * (taxableIncome - alreadyTaxedIncome);

            return totalTax;
        }
    }
}
