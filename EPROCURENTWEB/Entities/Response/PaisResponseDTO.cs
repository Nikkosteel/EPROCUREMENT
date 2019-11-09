using EprocurementWeb.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EprocurementWeb.Entities.Response
{
    public class PaisResponseDTO : ResponseBaseDTO
    {
        public List<PaisDTO> PaisList { get; set; }
    }
}