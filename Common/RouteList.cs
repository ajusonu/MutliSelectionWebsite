using System;
using System.Collections.Generic;

namespace OrbitCovidConditionConfigurator.Enums
{
    /// <summary>
    /// Get Route List
    /// </summary>
    public class RouteList
    {
        /// <summary>
        /// Get Route  List
        /// </summary>
        /// <returns></returns>
        public static List<Route> GetRoutes()
        {
            List<Route> routeList = new List<Route>();

            foreach (Route route in Enum.GetValues(typeof(Route)))
            {
                routeList.Add(route);
            }
            return routeList;
        }
    }
}