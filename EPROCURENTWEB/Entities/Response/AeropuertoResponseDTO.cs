﻿using EprocurementWeb.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EprocurementWeb.Entities.Response
{
    /// <summary>
    /// Representa el objeto de respuesta de Aeropuerto
    /// </summary>
    public class AeropuertoResponseDTO : ResponseBaseDTO
    {
        /// <summary>
        /// Representa una lista de Aeropuerto
        /// </summary>
        public List<AeropuertoDTO> AeropuertoList { get; set; }
    }
}