using System;
using System.Collections.Generic;
using System.Data;
using Reumed.Data.BusinessObjects;

namespace genmed_data.Database
{
    internal partial class SQLDatabase
    {
        public List<Provincia> GetProvincias()
        {
            var provincias = new List<Provincia>();

            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"SELECT 
                                                [ProvinciaId],
                                                [Nombre]
                                            FROM [Tbl_Provincia]";

                        using(IDataReader dr = cmd.ExecuteReader())
                        {
                            while(dr.Read())
                            {
                                var provincia = new Provincia();
                                provincia.ProvinciaId = dr.GetInt32(dr.GetOrdinal("provinciaid"));
                                provincia.Nombre = dr.GetString(dr.GetOrdinal("nombre"));
                                provincias.Add(provincia);
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

            return provincias;
        }

        public List<Ciudad> GetCiudades(int provinciaId)
        {
            var ciudades = new List<Ciudad>();

            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"SELECT [CiudadId]
                                                ,[Nombre]
                                            FROM [Tbl_Ciudad]
                                            WHERE ProvinciaId = " + provinciaId;

                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var ciudad = new Ciudad();
                                ciudad.CiudadId = dr.GetInt32(dr.GetOrdinal("ciudadid"));
                                ciudad.Nombre = dr.GetString(dr.GetOrdinal("nombre"));
                                ciudades.Add(ciudad);
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

            return ciudades;
        }

        public List<Sector> GetSectores()
        {
            var sectores = new List<Sector>();
            try
            {
                using (IDbConnection connection = GetConfigurationConnection())
                {
                    connection.Open();

                    using (IDbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"SELECT [SectorId]
                                            ,[Nombre]
                                        FROM [Tbl_Sector]";

                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var sector = new Sector();
                                sector.SectorId = dr.GetInt32(dr.GetOrdinal("sectorid"));
                                sector.Nombre = dr.GetString(dr.GetOrdinal("nombre"));
                                sectores.Add(sector);
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

            return sectores;
        }
    }
}