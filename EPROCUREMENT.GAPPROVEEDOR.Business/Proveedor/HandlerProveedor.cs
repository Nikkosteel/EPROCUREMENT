using EPROCUREMENT.GAPPROVEEDOR.Data;
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
    }
}
