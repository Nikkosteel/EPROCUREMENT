﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EprocurementWeb.Entities.DTO
{
    /// <summary>
    /// Representa el objeto y propiedades de ZonaHoraria
    /// </summary>
    public class ZonaHorariaDTO
    {
        /// <summary>
        /// Representa el identificador
        /// </summary>
        public int IdZonaHoraria { get; set; }

        /// <summary>
        /// Representa la descripcion
        /// </summary>
        public string Nombre { get; set; }
    }
}