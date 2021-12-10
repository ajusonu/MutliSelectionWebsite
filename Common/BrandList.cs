using System;
using System.Collections.Generic;

namespace OrbitCovidConditionConfigurator.Enums
{
    /// <summary>
    /// Get Brand List
    /// </summary>
    public class BrandList
    {
        /// <summary>
        /// Get Brand List
        /// </summary>
        /// <returns></returns>
        public static List<Brand> GetBrands()
        {
            List<Brand> brands = new List<Brand>();

            foreach (Brand segmentType in Enum.GetValues(typeof(Brand)))
            {
                brands.Add(segmentType);
            }
            return brands;
        }
    }
}