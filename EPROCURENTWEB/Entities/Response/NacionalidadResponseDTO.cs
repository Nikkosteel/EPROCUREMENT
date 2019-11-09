using EprocurementWeb.Entities.DTO;
using EprocurementWeb.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EprocurementWeb.Entities.Response
{
    /// <summary>
    /// Representa el objeto de respuesta de Nacionalidad
    /// </summary>
    public class NacionalidadResponseDTO : ResponseBaseDTO
    {
        /// <summary>
        /// Representa una lista de nacionalidades
        /// </summary>
        public List<NacionalidadDTO> NacionalidadList { get; set; }
    }
}