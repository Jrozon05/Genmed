using System;
using System.Collections.Generic;
using System.Data;
using Reumed.Data.BusinessObjects;

namespace genmed_data.Database
{
    internal partial class SQLDatabase
    {
        public List<Doctor> GetDoctores()
        {
            var resultados = new List<Doctor>();

            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_GetDoctores";

                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var doctor = new Doctor();
                                doctor.DoctorId = dr.GetInt32(dr.GetOrdinal("doctorid"));
                                doctor.Guid = dr.GetGuid(dr.GetOrdinal("guid"));
                                doctor.Nombre = dr.GetString(dr.GetOrdinal("nombre"));
                                doctor.Apellido = dr.GetString(dr.GetOrdinal("apellido"));
                                doctor.Posicion = dr.GetString(dr.GetOrdinal("posicion"));
                                resultados.Add(doctor);
                            }

                            dr.Close();
                        }
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