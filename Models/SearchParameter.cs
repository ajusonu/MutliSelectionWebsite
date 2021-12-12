using OrbitCovidConditionConfigurator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrbitCovidConditionConfigurator.Models
{
    /// <summary>
    /// Used in for SearchParameter in UI
    /// </summary>
    public class SearchParameter
    {
        public string SearchText { get; set; }
        /// <summary>
        /// Brand - Orbit Or Retail
        /// </summary>
        public Brand Brand { get; set; }

        /// <summary>
        /// Outlet Code
        /// </summary>
        public String OutletCode { get; set; }
        /// <summary>
        /// Branch Code
        /// </summary>
        /// <summary>
        /// Airline Code
        /// </summary>
        public String Airline { get; set; }
        /// <summary>
        /// Item type - Hotel, Air etc
        /// </summary>
        public SegmentType SegmentType { get; set; }
        /// <summary>
        /// Domestic/ International
        /// </summary>
        public Route Route { get; set; }
    }
}