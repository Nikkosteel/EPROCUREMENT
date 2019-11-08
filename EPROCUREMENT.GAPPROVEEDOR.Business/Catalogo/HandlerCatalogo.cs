using EPROCUREMENT.GAPPROVEEDOR.Data;
using EPROCUREMENT.GAPPROVEEDOR.Entities;
using EPROCUREMENT.GAPPROVEEDOR.Entities.Comun;
using System.Collections.Generic;

namespace EPROCUREMENT.GAPPROVEEDOR.Business.Catalogo
{
    public class HandlerCatalogo
    {
        private readonly CatalogoData catalogoData;
        //  private readonly InputDataValidator inputDataValidator;
        /// <summary>
        /// Constructor para la inicializacion de los accesos a datos
        /// </summary>
        /// <param name="client">Acceso a datos de tarjetas</param>
        public HandlerCatalogo()
        {
            catalogoData = new CatalogoData();
        }

        /// <summary>
        /// Metodo para obtener una lista de paises
        /// </summary>
        /// <returns></returns>
        public PaisResponseDTO GetPaisList()
        {
            PaisResponseDTO response = new PaisResponseDTO()
            {
                Success = false,
                PaisList = new List<PaisDTO>()
            };

            response = catalogoData.GetPaisList();
            response.Success = true;
            if (!response.Success)
            {
                response.ErrorList = new List<ErrorDTO> { new ErrorDTO { Codigo = "", Mensaje = string.Format("No fue posible recuperar datos disponibles o no se encontro alguna solicitud en proceso") } };
            }
            return response;
        }

        /// <summary>
        /// Metodo para obtener una lista de Aeropuertos
        /// </summary>
        /// <returns>Un objeto de tipo AeropuertoResponseDTO</returns>
        public AeropuertoResponseDTO GetAeropuertoList()
        {
            AeropuertoResponseDTO response = new AeropuertoResponseDTO()
            {
                AeropuertoList = new List<AeropuertoDTO>()
            };

            response = catalogoData.GetAeropuertoList();
            response.Success = true;
            if (!response.Success)
            {
                response.ErrorList = new List<ErrorDTO> { new ErrorDTO { Codigo = "", Mensaje = string.Format("No fue posible recuperar datos disponibles o no se encontro alguna solicitud en proceso") } };
            }
            return response;
        }

        /// <summary>
        /// Metodo para obtener una lista de Aeropuertos
        /// </summary>
        /// <returns>Un objeto de tipo GiroResponseDTO</returns>
        public GiroResponseDTO GetGiroList()
        {
            GiroResponseDTO response = new GiroResponseDTO()
            {
                GiroList = new List<GiroDTO>()
            };

            response = catalogoData.GetGiroList();
            response.Success = true;
            if (!response.Success)
            {
                response.ErrorList = new List<ErrorDTO> { new ErrorDTO { Codigo = "", Mensaje = string.Format("No fue posible recuperar datos disponibles o no se encontro alguna solicitud en proceso") } };
            }
            return response;
        }

        /// <summary>
        /// Metodo para obtener una lista de Nacionalidades
        /// </summary>
        /// <returns>Un objeto de tipo NacionalidadResponseDTO</returns>
        public NacionalidadResponseDTO GetNacionalidadList()
        {
            NacionalidadResponseDTO response = new NacionalidadResponseDTO()
            {
                NacionalidadList = new List<NacionalidadDTO>()
            };

            response = catalogoData.GetNacionalidadList();
            response.Success = true;
            if (!response.Success)
            {
                response.ErrorList = new List<ErrorDTO> { new ErrorDTO { Codigo = "", Mensaje = string.Format("No fue posible recuperar datos disponibles o no se encontro alguna solicitud en proceso") } };
            }
            return response;
        }

        /// <summary>
        /// Metodo para obtener una lista de ZonaHoraria
        /// </summary>
        /// <returns>Un objeto de tipo ZonaHorariaResponseDTO</returns>
        public ZonaHorariaResponseDTO GetZonaHorariaList()
        {
            ZonaHorariaResponseDTO response = new ZonaHorariaResponseDTO()
            {
                ZonaHorariaList = new List<ZonaHorariaDTO>()
            };

            response = catalogoData.GetZonaHorariaList();
            response.Success = true;
            if (!response.Success)
            {
                response.ErrorList = new List<ErrorDTO> { new ErrorDTO { Codigo = "", Mensaje = string.Format("No fue posible recuperar datos disponibles o no se encontro alguna solicitud en proceso") } };
            }
            return response;
        }
    }
}
