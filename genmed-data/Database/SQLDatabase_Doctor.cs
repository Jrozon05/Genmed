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
                                doctor.Usuario = new Usuario();
                                doctor.DoctorId = dr.GetInt32(dr.GetOrdinal("doctorid"));
                                doctor.Guid = dr.GetGuid(dr.GetOrdinal("guid"));
                                doctor.Nombre = dr.GetString(dr.GetOrdinal("nombre"));
                                doctor.Apellido = dr.GetString(dr.GetOrdinal("apellido"));
                                doctor.Posicion = dr.GetString(dr.GetOrdinal("posicion"));
                                doctor.Usuario.NombreUsuario = dr.GetString(dr.GetOrdinal("nombreusuario"));
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

        public Doctor GetDoctor(Guid? guid = null) {
            Doctor doctor = new Doctor();
            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_GetDoctorByGuid";

                        IDbDataParameter p = cmd.CreateParameter();
                        p.DbType = DbType.Guid;
                        p.ParameterName = "Guid";
                        p.Value = guid;
                        cmd.Parameters.Add(p);

                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                doctor.Usuario = new Usuario();
                                doctor.Guid = dr.GetGuid(dr.GetOrdinal("guid"));
                                doctor.Nombre = dr.GetString(dr.GetOrdinal("nombre"));
                                doctor.Apellido = dr.GetString(dr.GetOrdinal("apellido"));
                                doctor.Posicion = dr.GetString(dr.GetOrdinal("posicion"));
                                doctor.Usuario.UsuarioId = dr.GetInt32(dr.GetOrdinal("usuarioid"));
                                doctor.Usuario.NombreUsuario = dr.GetString(dr.GetOrdinal("nombreusuario"));

                            }

                            dr.Close();
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return doctor;
        }

        public Doctor CreateUpdateDoctor(Doctor doctor, int usuarioID) 
        {
            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_CreateUpdateDoctor";

                        IDbDataParameter outputId = cmd.CreateParameter();
                        outputId.DbType = DbType.Int32;
                        outputId.Direction = ParameterDirection.Output;
                        outputId.ParameterName = "DoctorId";
                        outputId.Value = doctor.DoctorId;
                        cmd.Parameters.Add(outputId);

                        IDbDataParameter p = cmd.CreateParameter();
                        p.DbType = DbType.Guid;
                        p.ParameterName = "Guid";
                        p.Value = doctor.Guid;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.String;
                        p.ParameterName = "Nombre";
                        p.Value = doctor.Nombre;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.String;
                        p.ParameterName = "Apellido";
                        p.Value = doctor.Apellido;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.String;
                        p.ParameterName = "Posicion";
                        p.Value = doctor.Posicion;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.Int32;
                        p.ParameterName = "UsuarioId";
                        p.Value = usuarioID;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.Boolean;
                        p.ParameterName = "Activo";
                        p.Value = doctor.Activo;
                        cmd.Parameters.Add(p);

                        cmd.ExecuteScalar();
                        doctor.DoctorId = Convert.ToInt32(outputId.Value);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return doctor;

        }

    }
}