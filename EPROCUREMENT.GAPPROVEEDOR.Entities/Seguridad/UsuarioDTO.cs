using System;

namespace EPROCUREMENT.GAPPROVEEDOR.Entities
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int IdUsuarioRol { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string Password { get; set; }
        public bool EsActivo { get; set; }
    }
}
