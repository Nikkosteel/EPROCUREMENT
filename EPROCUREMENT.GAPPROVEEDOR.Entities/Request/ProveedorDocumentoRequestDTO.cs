﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPROCUREMENT.GAPPROVEEDOR.Entities.Request
{
    public class ProveedorDocumentoRequestDTO : RequestBaseDTO
    {
        public List<ProveedorDocumentoDTO> ProveedorDocumentoList { get; set; }
    }
}
