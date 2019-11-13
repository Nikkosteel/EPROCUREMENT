using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPROCUREMENT.GAPPROVEEDOR.Entities
{
    public class CatalogoDocumentoDTO
    {
        public int IdProveedorDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public int IdFormatoArchivo { get; set; }
        public bool EsRequerido { get; set; }
        public int IdFormulario { get; set; }
    }
}
