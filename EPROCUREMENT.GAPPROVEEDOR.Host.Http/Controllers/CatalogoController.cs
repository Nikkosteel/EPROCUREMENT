using EPROCUREMENT.GAPPROVEEDOR.Business.Catalogo;
using EPROCUREMENT.GAPPROVEEDOR.Entities;
using System.Web.Http;

namespace EPROCUREMENT.GAPPROVEEDOR.Host.Http.Controllers
{
    [RoutePrefix("api/Catalogo")]
    public class CatalogoController : ApiController
    {
        // GET: api/Pais
        [HttpGet]
        [Route("PaisGetList")]
        public PaisResponseDTO GetPaisList()
        {
            HandlerCatalogo handlerCatalogo = new HandlerCatalogo();
            var paisResponseDTO = new PaisResponseDTO();
            paisResponseDTO = handlerCatalogo.GetPaisList();

            return paisResponseDTO;
        }

        // GET: api/Aeropuerto
        [HttpGet]
        [Route("AeropuertoGetList")]
        public AeropuertoResponseDTO GetAeropuertoList()
        {
            HandlerCatalogo handlerCatalogo = new HandlerCatalogo();
            var aeropuertoResponseDTO = new AeropuertoResponseDTO();
            aeropuertoResponseDTO = handlerCatalogo.GetAeropuertoList();

            return aeropuertoResponseDTO;
        }

        // GET: api/Giro
        [HttpGet]
        [Route("GiroGetList")]
        public GiroResponseDTO GetGiroList()
        {
            HandlerCatalogo handlerCatalogo = new HandlerCatalogo();
            var giroResponseDTO = new GiroResponseDTO();
            giroResponseDTO = handlerCatalogo.GetGiroList();

            return giroResponseDTO;
        }

        // GET: api/Nacionalidad
        [HttpGet]
        [Route("NacionalidadGetList")]
        public NacionalidadResponseDTO GetNacionalidadList()
        {
            HandlerCatalogo handlerCatalogo = new HandlerCatalogo();
            var nacionalidadResponseDTO = new NacionalidadResponseDTO();
            nacionalidadResponseDTO = handlerCatalogo.GetNacionalidadList();

            return nacionalidadResponseDTO;
        }

        // GET: api/ZonaHoraria
        [HttpGet]
        [Route("ZonaHorariaGetList")]
        public NacionalidadResponseDTO GetZonaHorariaList()
        {
            HandlerCatalogo handlerCatalogo = new HandlerCatalogo();
            var nacionalidadResponseDTO = new NacionalidadResponseDTO();
            nacionalidadResponseDTO = handlerCatalogo.GetNacionalidadList();

            return nacionalidadResponseDTO;
        }
    }
}
