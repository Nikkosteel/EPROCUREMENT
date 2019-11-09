using EPROCUREMENT.GAPPROVEEDOR.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EPROCUREMENT.GAPPROVEEDOR.Data
{
    public class CatalogoData
    {
        private readonly TryCatchExecutor tryCatch;

        public CatalogoData()
        {
            tryCatch = new TryCatchExecutor();
        }

        /// <summary>
        /// Obtiene un listado de paises
        /// </summary>
        /// <returns>Un objeto de tipo PaisResponseDTO</returns>
        public PaisResponseDTO GetPaisList()
        {
            PaisResponseDTO response = new PaisResponseDTO()
            {
                PaisList = new List<PaisDTO>()
            };

            PaisDTO pais = null;

            Func<PaisResponseDTO> action = () =>
            {

                using (var conexion = new SqlConnection(Helper.Connection()))
                {
                    conexion.Open();
                    var cmd = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_Pais_GETL, conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        pais = new PaisDTO();
                        pais.IdPais = Convert.ToInt32(reader["IdPais"]);
                        pais.Nombre = reader["Nombre"].ToString();
                        response.PaisList.Add(pais);
                    }
                }
                response.Success = true;
                return response;
            };

            return tryCatch.SafeExecutor(action);
        }

        /// <summary>
        /// Obtiene un listado de Giro
        /// </summary>
        /// <returns>Un objeto de tipo GiroResponseDTO</returns>
        public GiroResponseDTO GetGiroList()
        {
            GiroResponseDTO response = new GiroResponseDTO()
            {                
                GiroList = new List<GiroDTO>()
            };

            GiroDTO giro = null;

            Func<GiroResponseDTO> action = () =>
            {

                using (var conexion = new SqlConnection(Helper.Connection()))
                {
                    conexion.Open();
                    var cmd = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_CatalogoGiro_GETL, conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        giro = new GiroDTO();
                        giro.IdGiro = Convert.ToInt32(reader["IdGiro"]);
                        giro.GiroNombre = reader["GiroNombre"].ToString();
                        response.GiroList.Add(giro);
                    }
                }
                response.Success = true;
                return response;
            };

            return tryCatch.SafeExecutor(action);
        }

        /// <summary>
        /// Obtiene un listado de Aeropuertos
        /// </summary>
        /// <returns>Un objeto de tipo GiroResponseDTO</returns>
        public AeropuertoResponseDTO GetAeropuertoList()
        {
            AeropuertoResponseDTO response = new AeropuertoResponseDTO()
            {
                AeropuertoList = new List<AeropuertoDTO>()
            };

            AeropuertoDTO aeropuertoDTO = null;

            Func<AeropuertoResponseDTO> action = () =>
            {

                using (var conexion = new SqlConnection(Helper.Connection()))
                {
                    conexion.Open();
                    var cmd = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_Aeropuerto_GETL, conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        aeropuertoDTO = new AeropuertoDTO();
                        aeropuertoDTO.Id = reader["Id"].ToString();
                        aeropuertoDTO.Nombre = reader["Nombre"].ToString();
                        response.AeropuertoList.Add(aeropuertoDTO);
                    }
                }
                response.Success = true;
                return response;
            };

            return tryCatch.SafeExecutor(action);
        }

        /// <summary>
        /// Obtiene un listado de Nacionalidades
        /// </summary>
        /// <returns>Un objeto de tipo NacionalidadResponseDTO</returns>
        public NacionalidadResponseDTO GetNacionalidadList()
        {
            NacionalidadResponseDTO response = new NacionalidadResponseDTO()
            {
                NacionalidadList = new List<NacionalidadDTO>()
            };

            NacionalidadDTO nacionalidadDTO = null;

            Func<NacionalidadResponseDTO> action = () =>
            {

                using (var conexion = new SqlConnection(Helper.Connection()))
                {
                    conexion.Open();
                    var cmd = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_Nacionalidad_GETL, conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        nacionalidadDTO = new NacionalidadDTO();
                        nacionalidadDTO.IdNacionalidad = Convert.ToInt32(reader["IdNacionalidad"]);
                        nacionalidadDTO.Nombre = reader["NacionalidadNombre"].ToString();
                        response.NacionalidadList.Add(nacionalidadDTO);
                    }
                }
                response.Success = true;
                return response;
            };

            return tryCatch.SafeExecutor(action);
        }

        /// <summary>
        /// Obtiene un listado de Nacionalidades
        /// </summary>
        /// <returns>Un objeto de tipo NacionalidadResponseDTO</returns>
        public ZonaHorariaResponseDTO GetZonaHorariaList()
        {
            ZonaHorariaResponseDTO response = new ZonaHorariaResponseDTO()
            {
                ZonaHorariaList = new List<ZonaHorariaDTO>()
            };

            ZonaHorariaDTO zonaHorariaDTO = null;

            Func<ZonaHorariaResponseDTO> action = () =>
            {

                using (var conexion = new SqlConnection(Helper.Connection()))
                {
                    conexion.Open();
                    var cmd = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_ZonaHoraria_GETL, conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        zonaHorariaDTO = new ZonaHorariaDTO();
                        zonaHorariaDTO.IdZonaHoraria = Convert.ToInt32(reader["IdZonaHoraria"]);
                        zonaHorariaDTO.Nombre = reader["Nombre"].ToString();
                        response.ZonaHorariaList.Add(zonaHorariaDTO);
                    }
                }
                response.Success = true;
                return response;
            };

            return tryCatch.SafeExecutor(action);
        }
    }
}
