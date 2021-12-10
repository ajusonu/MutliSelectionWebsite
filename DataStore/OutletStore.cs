using OrbitCovidConditionConfigurator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrbitCovidConditionConfigurator.DataStore
{
    public class OutletStore : DolphinStoreBase
    {
        /// <summary>
        /// Get all or specific Orbit Outlet from database
        /// </summary>
        public static async Task<List<Outlet>> GetOutlets(int brand = 0)
        {
            List<Outlet> outlets = new List<Outlet>();
            try
            {
                // Connect to the apiconfig database
                using (SqlConnection conn = GetConnection())
                {
                    await conn.OpenAsync();

                    // prepare the SP
                    using (SqlCommand comm = new SqlCommand("DBA.proc_HOT_BranchDetails_Select", conn))
                    {
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Parameters.AddWithValue("Brand", brand);

                        // execute
                        using (SqlDataReader reader = await comm.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                outlets.Add(MapOutlet(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return outlets;
        }
        /// <summary>
        /// Map Outlet to Model field
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>Condition</returns>
        private static Outlet MapOutlet(SqlDataReader reader)
        {
            Outlet outlet = new Outlet();
            outlet.OutletCode = reader["ParentOutletCode"] as string;
            outlet.BranchCode = reader["BranchCode_FD"] as string;
            return outlet;
        }
    }
}