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
            var paisResponseDTO = new HandlerCatalogo().GetPaisList();

            return paisResponseDTO;
        }

        // GET: api/Aeropuerto
        [HttpGet]
        [Route("AeropuertoGetList")]
        public AeropuertoResponseDTO GetAeropuertoList()
        {
            var aeropuertoResponseDTO = new HandlerCatalogo().GetAeropuertoList();

            return aeropuertoResponseDTO;
        }

        // GET: api/Giro
        [HttpGet]
        [Route("GiroGetList")]
        public GiroResponseDTO GetGiroList()
        {
            var giroResponseDTO = new HandlerCatalogo().GetGiroList();

            return giroResponseDTO;
        }

        // GET: api/Nacionalidad
        [HttpGet]
        [Route("NacionalidadGetList")]
        public NacionalidadResponseDTO GetNacionalidadList()
        {
            var nacionalidadResponseDTO = new HandlerCatalogo().GetNacionalidadList();

            return nacionalidadResponseDTO;
        }

        // GET: api/ZonaHoraria
        [HttpGet]
        [Route("ZonaHorariaGetList")]
        public ZonaHorariaResponseDTO GetZonaHorariaList()
        {
            var zonaHorariaResponseDTO = new HandlerCatalogo().GetZonaHorariaList();

            return zonaHorariaResponseDTO;
        }

        // GET: api/Estado
        [HttpGet]
        [Route("EstadoGetList")]
        public EstadoResponseDTO GetEstadoList([FromBody]EstadoRequesteDTO request)
        {
            var estadoResponse = new HandlerCatalogo().GetEstadoList(request);

            return estadoResponse;
        }

        // GET: api/Municipio
        [HttpGet]
        [Route("MunicipioGetList")]
        public MunicipioResponseDTO GetMunicipioList([FromBody]MunicipioRequesteDTO request)
        {
            var municipioResponse = new HandlerCatalogo().GetMunicipioList(request);

            return municipioResponse;
        }

        // GET: api/Idioma
        [HttpGet]
        [Route("IdiomaGetList")]
        public IdiomaResponseDTO GetIdiomaList()
        {
            var idiomaResponse = new HandlerCatalogo().GetIdiomaList();

            return idiomaResponse;
        }

        // GET: api/TipoProveedor
        [HttpGet]
        [Route("TipoProveedorGetList")]
        public TipoProveedorResponseDTO GetTipoProveedorList()
        {
            var tipoProveedorResponse = new HandlerCatalogo().GetTipoProveedorList();

            return tipoProveedorResponse;
        }
    }
}
