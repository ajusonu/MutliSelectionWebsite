using log4net;
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
        protected static readonly ILog _log = LogManager.GetLogger(typeof(DolphinStoreBase));
        private static string ConnectionStringName => "Dolphin";

        /// <summary>
        /// Get Dolphin the connection 
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
                _log.Error($" Connection Error {ex.ToString()}");
            }
            return null;
        }
    }
}