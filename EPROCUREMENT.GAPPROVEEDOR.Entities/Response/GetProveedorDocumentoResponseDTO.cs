using System.Collections.Generic;

namespace EPROCUREMENT.GAPPROVEEDOR.Entities.Response
{
    public class GetProveedorDocumentoResponseDTO : ResponseBaseDTO
    {
        public List<ProveedorDocumentoDTO> ProveedorDocumentoList { get; set; }
    }
}
