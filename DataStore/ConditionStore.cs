using OrbitCovidConditionConfigurator.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrbitCovidConditionConfigurator.DataStore
{
    public class ConditionStore : DolphinStoreBase
    {
     
        /// <summary>
        /// Delete Selected conditions from database
        /// </summary>
        public static async Task<Condition> DeleteSelectedConditions(string listCondidtionIds)
        {
            try
            {
                // Connect to the apiconfig database
                using (SqlConnection conn = GetConnection())
                {
                    await conn.OpenAsync();

                    // prepare the SP
                    using (SqlCommand comm = new SqlCommand("DBA.proc_HOT_Air_Covid_Conditions_Delete", conn))
                    {
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Parameters.AddWithValue("Ids", listCondidtionIds);
                  
                        // execute
                        await comm.ExecuteNonQueryAsync();

                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error($"OrbitCovidConditionConfigurator DeleteSelectedConditions failed {ex.ToString()}");
            }
            return null;
        }
        /// <summary>
        /// Save conditions into database
        /// </summary>
        public static async Task<Condition> SaveCondition(Condition condition)
        {
            try
            {
                // Connect to the apiconfig database
                using (SqlConnection conn = GetConnection())
                {
                    await conn.OpenAsync();

                    // prepare the SP
                    using (SqlCommand comm = new SqlCommand("DBA.proc_HOT_Air_Covid_Conditions_Save", conn))
                    {
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Parameters.AddWithValue("Id", condition.Id);
                        comm.Parameters.AddWithValue("Brand", condition.Brand);
                        comm.Parameters.AddWithValue("ValidFrom", condition.ValidFrom);
                        comm.Parameters.AddWithValue("ValidTo", condition.ValidTo);
                        comm.Parameters.AddWithValue("BranchCode", condition.BranchCode);
                        comm.Parameters.AddWithValue("OutletCode", condition.OutletCode);
                        comm.Parameters.AddWithValue("IsActive", condition.IsActive);
                        comm.Parameters.AddWithValue("Remark", condition.CovidConditon);
                        comm.Parameters.AddWithValue("Airline", condition.Airline == null ? "" : condition.Airline);
                        comm.Parameters.AddWithValue("Flight", "");
                       comm.Parameters.AddWithValue("Route", (int)condition.Route);
                        comm.Parameters.AddWithValue("SegmentType", (int)condition.SegmentType);

                        // execute
                        await comm.ExecuteNonQueryAsync();
                       
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error($" Save Condition Error {ex.ToString()}");
            }
            return null;
        }
        /// <summary>
        /// Get all or specific conditions from database by specifying an id
        /// </summary>
        /// <param name="id"></param>
        public static async Task<List<Condition>> GetConditions(int? id, string searchText="")
        {
            List<Condition> conditions = new List<Condition>();
            try
            {
                // Connect to the apiconfig database
                using (SqlConnection conn = GetConnection())
                {
                    await conn.OpenAsync();

                    // prepare the SP
                    using (SqlCommand comm = new SqlCommand("DBA.proc_HOT_Air_Covid_Conditions_Get", conn))
                    {
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Parameters.AddWithValue("id", id);
                        comm.Parameters.AddWithValue("searchText", searchText);

                        // execute
                        using (SqlDataReader reader = await comm.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                conditions.Add(MapCondition(reader));
                            }
                        }
                    }
                }
                return conditions.Count ==0 ? null : conditions;
            }
            catch (Exception ex)
            {
                _log.Error($" Get Conditions Error {ex.ToString()}");
            }
            return null;
        }
        /// <summary>
        /// Map Condition to Model field
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>Condition</returns>
        private static Condition MapCondition(SqlDataReader reader)
        {
            Condition condition = new Condition();
            condition.Id = int.Parse(reader["Id"].ToString());
            condition.IsActive = bool.Parse(reader["IsActive"].ToString());
            condition.Brand = (Enums.Brand)reader["Brand"];
            condition.SegmentType = (Enums.SegmentType)reader["SegmentType"];
            condition.Route = (Enums.Route)reader["Route"];
            condition.Airline = reader["Airline"] as string;
            condition.OutletCode = reader["ParentCode"] as string;
            condition.BranchCode = reader["Branch"] as string;
            condition.CovidConditon = reader["Remark"] as string;
            condition.ValidFrom = reader["ValidFrom"] != null ? DateTime.Parse(reader["ValidFrom"].ToString()) : DateTime.MinValue ;
            condition.ValidTo = reader["ValidTo"] != null ? DateTime.Parse(reader["ValidTo"].ToString()) : DateTime.MinValue;
            return condition;
        }

        
    }
}