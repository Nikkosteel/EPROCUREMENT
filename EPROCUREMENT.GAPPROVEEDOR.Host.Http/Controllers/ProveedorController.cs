using EPROCUREMENT.GAPPROVEEDOR.Business.Proveedor;
using EPROCUREMENT.GAPPROVEEDOR.Entities;
using System.Web.Http;

namespace EPROCUREMENT.GAPPROVEEDOR.Host.Http.Controllers
{
    [RoutePrefix("api/Proveedor")]
    public class ProveedorController : ApiController
    {
        /// <summary>
        /// Operacion para insertar un proveedor
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Insertar")]
        public ProveedorResponseDTO Process([FromBody]ProveedorRequesteDTO request)
        {
            var response = new HandlerProveedor().GuardarProveedor(request);
            return response;
        }
    }
}