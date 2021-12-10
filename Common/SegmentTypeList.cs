using System;
using System.Collections.Generic;

namespace OrbitCovidConditionConfigurator.Enums
{
    /// <summary>
    /// Get Segment Type List
    /// </summary>
    public class SegmentTypeList
    {
        /// <summary>
        /// Get Segment Type List
        /// </summary>
        /// <returns></returns>
        public static List<SegmentType> GetSegmentTypes()
        {
            List<SegmentType> segmentTypes = new List<SegmentType>();

            foreach (SegmentType segmentType in Enum.GetValues(typeof(SegmentType)))
            {
                segmentTypes.Add(segmentType);
            }
            return segmentTypes;
        }
    }
}