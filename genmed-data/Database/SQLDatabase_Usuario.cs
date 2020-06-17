using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using Reumed.Data.BusinessObjects;

namespace genmed_data.Database
{
    internal partial class SQLDatabase
    {
        public Usuario GetUsuario(Guid? guid = null, string nombreUsuario = null)
        {
            var usuario = new Usuario();

            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_GetUsuarioByGuidOrNombreUsuario";

                        IDbDataParameter p = cmd.CreateParameter();
                        p.DbType = DbType.Guid;
                        p.ParameterName = "Guid";
                        p.Value = guid;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.String;
                        p.ParameterName = "NombreUsuario";
                        p.Value = nombreUsuario;
                        cmd.Parameters.Add(p);

                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                usuario.Guid = dr.GetGuid(dr.GetOrdinal("guid"));
                                usuario.NombreUsuario = dr.GetString(dr.GetOrdinal("nombreusuario"));
                                usuario.Clave = dr.GetString(dr.GetOrdinal("clave"));
                                usuario.ClaveHash = (byte[])dr["clavehash"];
                                usuario.ClaveSalt = (byte[])dr["clavesalt"];
                                usuario.Activo = dr.GetBoolean(dr.GetOrdinal("activo"));
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

            return usuario;
        }

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

                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var usuario = new Usuario();
                                usuario.Guid = dr.GetGuid(dr.GetOrdinal("guid"));
                                usuario.NombreUsuario = dr.GetString(dr.GetOrdinal("nombreusuario"));
                                usuario.Clave = dr.GetString(dr.GetOrdinal("clave"));
                                resultados.Add(usuario);
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

        public Usuario Login(string nombreUsuario, string clave)
        {
            return null;
        }

        public Usuario CreateUpdateUsuario(Usuario usuario, string clave)
        {
            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_CreateUpdateUsuario";

                        IDbDataParameter p = cmd.CreateParameter();
                        p.DbType = DbType.Guid;
                        p.ParameterName = "Guid";
                        p.Value = usuario.Guid;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.String;
                        p.ParameterName = "NombreUsuario";
                        p.Value = usuario.NombreUsuario;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.String;
                        p.ParameterName = "Clave";
                        p.Value = clave;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.Binary;
                        p.ParameterName = "ClaveHash";
                        p.Value = usuario.ClaveHash;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.Binary;
                        p.ParameterName = "ClaveSalt";
                        p.Value = usuario.ClaveSalt;
                        cmd.Parameters.Add(p);

                        cmd.ExecuteScalar();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }


    }
}