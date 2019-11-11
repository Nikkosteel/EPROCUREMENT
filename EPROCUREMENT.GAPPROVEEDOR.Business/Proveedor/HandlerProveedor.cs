﻿using EPROCUREMENT.GAPPROVEEDOR.Data;
using EPROCUREMENT.GAPPROVEEDOR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPROCUREMENT.GAPPROVEEDOR.Business.Proveedor
{
    public class HandlerProveedor
    {
        private readonly ProveedorData proveedorData;

        /// <summary>
        /// Constructor para la inicializacion de los accesos a datos
        /// </summary>
        public HandlerProveedor()
        {
            proveedorData = new ProveedorData();
        }

        /// <summary>
        /// Metodo para obtener una lista de paises
        /// </summary>
        /// <returns></returns>
        public ProveedorResponseDTO GuardarProveedor(ProveedorRequesteDTO request)
        {
            var response = proveedorData.ProveedorInsertar(request);
            if (!response.Success)
            {
                response.ErrorList = new List<ErrorDTO> { new ErrorDTO { Codigo = "", Mensaje = string.Format("No fue posible recuperar datos disponibles o no se encontro alguna solicitud en proceso") } };
            }
            return response;
        }

        /// <summary>
        /// Obtiene un listado de provedores por filtro
        /// </summary>
        /// <param name="request">Un objeto de tipo ProveedorEstatusRequestDTO con los filtros</param>
        /// <returns>Un obejeto de tipo ProveedorEstatusResponseDTO</returns>
        public ProveedorEstatusResponseDTO GetProveedorEstatusList(ProveedorEstatusRequestDTO request)
        {
            var response = proveedorData.GetProveedorEstatusList(request);
            if (!response.Success)
            {
                response.ErrorList = new List<ErrorDTO> { new ErrorDTO { Codigo = "", Mensaje = string.Format("No fue posible recuperar datos disponibles o no se encontro alguna solicitud en proceso") } };
            }
            return response;
        }

        /// <summary>
        /// Obtiene un listado de provedores por filtro
        /// </summary>
        /// <param name="request">Un objeto que contiene el identificador del proveedor</param>
        /// <returns>Un obejeto con el detalle del proveedor</returns>
        public ProveedorDetalleResponseDTO GetProveedorElemento(ProveedorDetalleRequestDTO request)
        {
            var response = proveedorData.GetProveedorElemento(request);
            if (!response.Success)
            {
                response.ErrorList = new List<ErrorDTO> { new ErrorDTO { Codigo = "", Mensaje = string.Format("No fue posible recuperar datos disponibles o no se encontro alguna solicitud en proceso") } };
            }
            return response;
        }

        /// <summary>
        /// Metodo para insertar el estatus del proveedor
        /// </summary>
        /// <returns>Un objeto de tipo ProveedorResponseDTO con la respuesta</returns>
        public ProveedorEstatusResponseDTO EstatusProveedorInsertar(ProveedorAprobarRequestDTO request)
        {
            var response = proveedorData.EstatusProveedorInsertar(request);
            if (!response.Success)
            {
                response.ErrorList = new List<ErrorDTO> { new ErrorDTO { Codigo = "", Mensaje = string.Format("No fue posible recuperar datos disponibles o no se encontro alguna solicitud en proceso") } };
            }
            return response;
        }
    }
}
