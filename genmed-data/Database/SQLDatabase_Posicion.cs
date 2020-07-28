using System;
using System.Collections.Generic;
using System.Data;
using genmed_data.BusinessObject;

namespace genmed_data.Database
{
    internal partial class SQLDatabase
    {
        public List<Posicion> GetPosiciones()
        {
            var posiciones = new List<Posicion>();

            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_GetPosiciones";

                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                var posicion = new Posicion();
                                posicion.PosicionId = dr.GetInt32(dr.GetOrdinal("posicionid"));
                                posicion.Descripcion = dr.GetString(dr.GetOrdinal("descripcion"));
                                posicion.Activo = dr.GetBoolean(dr.GetOrdinal("activo"));
                                posiciones.Add(posicion);
                            }

                            dr.Close();
                        }
                    }

                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return posiciones;
        }
    }
}