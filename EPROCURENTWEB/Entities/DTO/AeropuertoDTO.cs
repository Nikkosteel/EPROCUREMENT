using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EprocurementWeb.Entities.DTO
{
    /// <summary>
    /// Representa el objeto y propiedades de Aeropuerto
    /// </summary>
    public class AeropuertoDTO
    {
        /// <summary>
        /// Representa el Id del aeropuerto
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Representa El nombre del aeropuerto
        /// </summary>
        public string Nombre { get; set; }
    }
}