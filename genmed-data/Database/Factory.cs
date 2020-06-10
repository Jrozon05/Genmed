using System.Data.SqlClient;
using genmed_data.Services;

namespace genmed_data.Database
{
    public static class Factory
    {
        public static IService GetService()
        {
            return new ServiceManager();
        }

        public static IDatabase GetDatabase()
        {
            return GetSQLDatabase();
        }

        internal static SQLDatabase GetSQLDatabase()
        {
            SqlConnectionStringBuilder sConnection = new SqlConnectionStringBuilder()
            {
                DataSource = "69.10.33.34",
                InitialCatalog = "GenmedDb",
                UserID = "genmedrd",
                Password = "Santiago05@"
            };

            return new SQLDatabase(sConnection);
        }
    }
}