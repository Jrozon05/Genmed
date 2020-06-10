using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using Reumed.DataAccess.BusinessObjects;

namespace genmed_data.Database
{
    internal partial class SQLDatabase
    {
        public List<Usuario> GetUsuarios()
        {
            var resultados = new List<Usuario>();

            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_GetUsuarios";

                        object response = cmd.ExecuteScalar();

                        if (response != null)
                            resultados = (List<Usuario>)JsonConvert.DeserializeObject((string)response, typeof(List<Usuario>));
                        else
                            resultados = null;
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // TODO: Log las excepciones o errores que vienen de la base de datos
                throw ex;
            }

            return resultados;
        }
    }
}