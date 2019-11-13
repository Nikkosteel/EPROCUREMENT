using System.Collections.Generic;

namespace EPROCUREMENT.GAPPROVEEDOR.Entities.Proveedor
{
    public class ProveedorInformacionFinanciera
    {
        public string RFC { get; set; }
        public List<ProveedorCuentaDTO> ProveedorCuentaList { get; set; }
        public List<CatalogoDocumentoDTO> CatalogoDocumentoList { get; set; }
    }
}
