using System;
using System.Configuration;
using System.Data.SqlClient;

namespace OrbitCovidConditionConfigurator.DataStore
{
    /// <summary>
    /// Dolphin Store
    /// </summary>
    public class DolphinStoreBase
    {
        private static string ConnectionStringName => "Dolphin";

        /// <summary>
        /// Helper function to get the connection 
        /// </summary>
        /// <returns></returns>
        protected static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException($"Application connection string {ConnectionStringName} not found or empty");
            }
            try
            {
                return new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}