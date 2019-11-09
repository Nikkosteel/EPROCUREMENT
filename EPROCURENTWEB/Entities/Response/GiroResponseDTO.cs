using EprocurementWeb.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EprocurementWeb.Entities.Response
{
    /// <summary>
    /// Representa el objeto de respuesta de Giro
    /// </summary>
    public class GiroResponseDTO : ResponseBaseDTO
    {
        /// <summary>
        /// Representa una lista de Giros
        /// </summary>
        public List<GiroDTO> GiroList { get; set; }
    }
}