using System.Data;

namespace genmed_data.Database
{
    internal interface IDatabaseAccess
    {
        IDbConnection GetConfigurationConnection();
    }
}