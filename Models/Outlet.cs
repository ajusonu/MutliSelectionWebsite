using OrbitCovidConditionConfigurator.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrbitCovidConditionConfigurator.Models
{
    public class Outlet
    {
        /// <summary>
        /// Orbit Parent Outlet
        /// </summary>
        public string OutletCode { get; set; }
        /// <summary>
        /// Branch Code
        /// </summary>
        public string BranchCode { get; set; }
        /// <summary>
        /// Get List of all the Outlets
        /// </summary>
        public async static Task<List<Outlet>> GetOutlets()
        {
            List<Outlet> outlets =await OutletStore.GetOutlets();
            return outlets;
        }
    }
}