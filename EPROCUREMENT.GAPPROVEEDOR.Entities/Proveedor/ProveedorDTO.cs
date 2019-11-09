using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPROCUREMENT.GAPPROVEEDOR.Entities
{
    public class ProveedorDTO
    {
        public int IdProveedor { get; set; }
        public string NombreEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public string NIF { get; set; }
        public string ProvTelefono { get; set; }
        public string ProvFax { get; set; }
        public string PaginaWeb { get; set; }
        public int IdZonaHoraria { get; set; }
        public int IdTipoProveedor { get; set; }
        public string AXNumeroProveedor { get; set; }
        public DateTime? AXFechaRegistro { get; set; }
        public int IdNacionalidad { get; set; }
        public ProveedorDireccionDTO Direccion { get; set; }
        public List<ProveedorEmpresaDTO> EmpresaList { get; set; }
        public ProveedorContactoDTO Contacto { get; set; }
        public List<GiroDTO> GiroList { get; set; }
    }
}
