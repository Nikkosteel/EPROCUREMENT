﻿using EPROCUREMENT.GAPPROVEEDOR.Entities;
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

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pais = new PaisDTO();
                            pais.IdPais = Convert.ToInt32(reader["IdPais"]);
                            pais.Nombre = reader["Nombre"].ToString();
                            response.PaisList.Add(pais);
                        }
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

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            giro = new GiroDTO();
                            giro.IdGiro = Convert.ToInt32(reader["IdGiro"]);
                            giro.GiroNombre = reader["GiroNombre"].ToString();
                            response.GiroList.Add(giro);
                        }
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

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            aeropuertoDTO = new AeropuertoDTO();
                            aeropuertoDTO.Id = reader["Id"].ToString();
                            aeropuertoDTO.Nombre = reader["Nombre"].ToString();
                            response.AeropuertoList.Add(aeropuertoDTO);
                        }
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

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nacionalidadDTO = new NacionalidadDTO();
                            nacionalidadDTO.IdNacionalidad = Convert.ToInt32(reader["IdNacionalidad"]);
                            nacionalidadDTO.Nombre = reader["NacionalidadNombre"].ToString();
                            response.NacionalidadList.Add(nacionalidadDTO);
                        }
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

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            zonaHorariaDTO = new ZonaHorariaDTO();
                            zonaHorariaDTO.IdZonaHoraria = Convert.ToInt32(reader["IdZonaHoraria"]);
                            zonaHorariaDTO.Nombre = reader["Nombre"].ToString();
                            response.ZonaHorariaList.Add(zonaHorariaDTO);
                        }
                    }
                }
                response.Success = true;
                return response;
            };

            return tryCatch.SafeExecutor(action);
        }

        /// <summary>
        /// Obtiene un listado de Estados
        /// </summary>
        /// <param name="request">Un objeto de tipo EstadoRequesteDTO</param>
        /// <returns>Un obejeto de tipo EstadoResponseDTO</returns>
        public EstadoResponseDTO GetEstadoList(EstadoRequesteDTO request)
        {
            EstadoResponseDTO response = new EstadoResponseDTO()
            {
                EstadoList = new List<EstadoDTO>()
            };

            EstadoDTO estadoDTO = null;

            Func<EstadoResponseDTO> action = () =>
            {

                using (var conexion = new SqlConnection(Helper.Connection()))
                {
                    conexion.Open();
                    var cmd = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_Estado_GETL, conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@idPais", request.idPais));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            estadoDTO = new EstadoDTO();
                            estadoDTO.IdEstado = Convert.ToInt32(reader["IdEstado"]);
                            estadoDTO.Nombre = reader["Nombre"].ToString();
                            response.EstadoList.Add(estadoDTO);
                        }
                    }
                }
                response.Success = true;
                return response;
            };

            return tryCatch.SafeExecutor(action);
        }

        /// <summary>
        /// Obtiene un listado de Estados
        /// </summary>
        /// <param name="request">Un objeto de tipo MunicipioRequesteDTO</param>
        /// <returns>Un obejeto de tipo MunicipioResponseDTO</returns>
        public MunicipioResponseDTO GetMunicipioList(MunicipioRequesteDTO request)
        {
            MunicipioResponseDTO response = new MunicipioResponseDTO()
            {
                MunicipioList = new List<MunicipioDTO>()
            };

            MunicipioDTO municipioDTO = null;

            Func<MunicipioResponseDTO> action = () =>
            {

                using (var conexion = new SqlConnection(Helper.Connection()))
                {
                    conexion.Open();
                    var cmd = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_Municipio_GETL, conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@idEstado", request.idEstado));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            municipioDTO = new MunicipioDTO();
                            municipioDTO.IdMunicipio = Convert.ToInt32(reader["IdMunicipio"]);
                            municipioDTO.Nombre = reader["Nombre"].ToString();
                            response.MunicipioList.Add(municipioDTO);
                        }
                    }
                }
                response.Success = true;
                return response;
            };

            return tryCatch.SafeExecutor(action);
        }

        /// <summary>
        /// Obtiene un listado de Tipo de Proveedor
        /// </summary>
        /// <returns>Un obejeto de tipo TipoProveedorResponseDTO</returns>
        public TipoProveedorResponseDTO GetTipoProveedorList()
        {
            TipoProveedorResponseDTO response = new TipoProveedorResponseDTO()
            {
                TipoProveedorList = new List<TipoProveedorDTO>()
            };

            TipoProveedorDTO tipoProveedorDTO = null;

            Func<TipoProveedorResponseDTO> action = () =>
            {

                using (var conexion = new SqlConnection(Helper.Connection()))
                {
                    conexion.Open();
                    var cmd = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_TipoProveedor_GETL, conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tipoProveedorDTO = new TipoProveedorDTO();
                            tipoProveedorDTO.IdTipoProveedor = Convert.ToInt32(reader["IdTipoProveedor"]);
                            tipoProveedorDTO.Tipo = reader["Tipo"].ToString();
                            response.TipoProveedorList.Add(tipoProveedorDTO);
                        }
                    }
                }
                response.Success = true;
                return response;
            };

            return tryCatch.SafeExecutor(action);
        }

        /// <summary>
        /// Obtiene un listado de Idiomas
        /// </summary>
        /// <returns>Un obejeto de tipo IdiomaResponseDTO</returns>
        public IdiomaResponseDTO GetIdiomaList()
        {
            IdiomaResponseDTO response = new IdiomaResponseDTO()
            {
                IdiomaList = new List<IdiomaDTO>()
            };

            IdiomaDTO idiomaDTO = null;

            Func<IdiomaResponseDTO> action = () =>
            {

                using (var conexion = new SqlConnection(Helper.Connection()))
                {
                    conexion.Open();
                    var cmd = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_Idioma_GETL, conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idiomaDTO = new IdiomaDTO();
                            idiomaDTO.IdIdioma = Convert.ToInt32(reader["IdIdioma"]);
                            idiomaDTO.NombreIdioma = reader["NombreIdioma"].ToString();
                            idiomaDTO.Origen = reader["Origen"].ToString();
                            idiomaDTO.Status = Convert.ToBoolean(reader["Status"]); 
                            response.IdiomaList.Add(idiomaDTO);
                        }
                    }
                }
                response.Success = true;
                return response;
            };

            return tryCatch.SafeExecutor(action);
        }
    }
}