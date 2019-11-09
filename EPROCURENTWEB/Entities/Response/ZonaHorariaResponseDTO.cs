using EprocurementWeb.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EprocurementWeb.Entities.Response
{
    /// <summary>
    /// Representa el objeto de respuesta de ZonaHoraria
    /// </summary>
    public class ZonaHorariaResponseDTO : ResponseBaseDTO
    {
        /// <summary>
        /// Representa una lista de ZonaHoraria
        /// </summary>
        public List<ZonaHorariaDTO> ZonaHorariaList { get; set; }
    }
}