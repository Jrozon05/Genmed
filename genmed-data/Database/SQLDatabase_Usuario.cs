using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Newtonsoft.Json;
using Reumed.Data.BusinessObjects;

namespace genmed_data.Database
{
    internal partial class SQLDatabase
    {
        public Usuario GetUsuario(Guid? guid = null, string nombreUsuario = null, int? usuarioId = null)
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
                        p.DbType = DbType.Int32;
                        p.ParameterName = "UsuarioId";
                        p.Value = usuarioId;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.String;
                        p.ParameterName = "NombreUsuario";
                        p.Value = nombreUsuario;
                        cmd.Parameters.Add(p);

                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            usuario = null;
                            while (dr.Read())
                            {
                                usuario = new Usuario();
                                usuario.Rol = new Rol();
                                usuario.Guid = dr.GetGuid(dr.GetOrdinal("guid"));
                                usuario.UsuarioId = dr.GetInt32(dr.GetOrdinal("usuarioid"));
                                usuario.NombreUsuario = dr.GetString(dr.GetOrdinal("nombreusuario"));
                                usuario.Email = dr.GetString(dr.GetOrdinal("email"));
                                usuario.Activo = dr.GetBoolean(dr.GetOrdinal("activo"));
                                usuario.Rol.RolId = dr.GetInt32(dr.GetOrdinal("rolid"));
                                usuario.Rol.Nombre = dr.GetString(dr.GetOrdinal("nombrerol"));
                                usuario.Clave = dr.IsDBNull(dr.GetOrdinal("clave")) ? "" : dr.GetString(dr.GetOrdinal("clave"));
                                if (!Convert.IsDBNull(dr["clavehash"]))
                                    usuario.ClaveHash = (byte[])dr["clavehash"];
                                else
                                    usuario.ClaveHash = new byte[0];
                                if (!Convert.IsDBNull(dr["clavesalt"]))
                                    usuario.ClaveSalt = (byte[])dr["clavesalt"];
                                else
                                    usuario.ClaveSalt = new byte[0];
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
            var usuarios = new List<Usuario>();
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
                                usuario.Rol = new Rol();

                                usuario.UsuarioId = dr.GetInt32(dr.GetOrdinal("usuarioid"));
                                usuario.Guid = dr.GetGuid(dr.GetOrdinal("guid"));
                                usuario.NombreUsuario = dr.GetString(dr.GetOrdinal("nombreusuario"));
                                usuario.Email = dr.GetString(dr.GetOrdinal("email"));
                                usuario.Activo = dr.GetBoolean(dr.GetOrdinal("activo"));
                                usuario.Rol.RolId = dr.GetInt32(dr.GetOrdinal("rolid"));
                                usuario.Rol.Nombre = dr.GetString(dr.GetOrdinal("nombrerol"));
                                usuarios.Add(usuario);
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

            return usuarios;
        }

        public List<Usuario> GetUsuariosNoAsignado()
        {
            var usuarios = new List<Usuario>();
            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_GetUsuariosNoAsignado";

                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var usuario = new Usuario();
                                usuario.Rol = new Rol();

                                usuario.UsuarioId = dr.GetInt32(dr.GetOrdinal("usuarioid"));
                                usuario.Guid = dr.GetGuid(dr.GetOrdinal("guid"));
                                usuario.NombreUsuario = dr.GetString(dr.GetOrdinal("nombreusuario"));
                                usuario.Email = dr.GetString(dr.GetOrdinal("email"));
                                usuario.Activo = dr.GetBoolean(dr.GetOrdinal("activo"));
                                usuario.Rol.RolId = dr.GetInt32(dr.GetOrdinal("rolid"));
                                usuario.Rol.Nombre = dr.GetString(dr.GetOrdinal("nombrerol"));
                                usuarios.Add(usuario);
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

            return usuarios;
        }

        public Usuario CreateUpdateUsuario(Usuario usuario, int rolId)
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

                        IDbDataParameter outputId = cmd.CreateParameter();
                        outputId.DbType = DbType.Int32;
                        outputId.Direction = ParameterDirection.Output;
                        outputId.ParameterName = "UsuarioId";
                        outputId.Value = usuario.UsuarioId;
                        cmd.Parameters.Add(outputId);

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
                        p.ParameterName = "Email";
                        p.Value = usuario.Email;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.Int32;
                        p.ParameterName = "RolId";
                        p.Value = rolId;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.Boolean;
                        p.ParameterName = "Activo";
                        p.Value = usuario.Activo;
                        cmd.Parameters.Add(p);

                        p = cmd.CreateParameter();
                        p.DbType = DbType.Boolean;
                        p.ParameterName = "Asignado";
                        p.Value = usuario.Asignado;
                        cmd.Parameters.Add(p);

                        cmd.ExecuteScalar();
                        usuario.UsuarioId = Convert.ToInt32(outputId.Value);
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

        public Usuario UpdateClaveUsuario(Usuario usuario, string clave)
        {
            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_UpdateClaveUsuario";

                        IDbDataParameter p = cmd.CreateParameter();
                        p.DbType = DbType.Guid;
                        p.ParameterName = "Guid";
                        p.Value = usuario.Guid;
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

                        p = cmd.CreateParameter();
                        p.DbType = DbType.String;
                        p.ParameterName = "Clave";
                        p.Value = clave;
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