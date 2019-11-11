using Eprocurement.Compras.Business;
using EPROCUREMENT.GAPPROVEEDOR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eprocurement.Compras.Controllers
{

    public class HomeController : Controller
    {
        public List<AeropuertoDTO> aeropuertoList;
        public List<ZonaHorariaDTO> zonaHorariaList;
        public List<NacionalidadDTO> nacionalidadList;
        public List<GiroDTO> giroList;
        public List<PaisDTO> paisList;
        public List<IdiomaDTO> idiomaList;
        public List<EstadoDTO> estadoList;
        public List<MunicipioDTO> municipioList;
        public List<TipoProveedorDTO> tipoProveedorList;

        private void CargarCatalogos()
        {
            BusinessLogic businessLogic = new BusinessLogic();
            aeropuertoList = businessLogic.GetAeropuertosList();
            giroList = businessLogic.GetGirosList();
            tipoProveedorList = businessLogic.GetTipoProveedorList();
        }

        private void CargarCatalogosAceptar()
        {
            BusinessLogic businessLogic = new BusinessLogic();
            aeropuertoList = businessLogic.GetAeropuertosList();
            zonaHorariaList = businessLogic.GetZonaHorariaList();
            nacionalidadList = businessLogic.GetNacionalidadList();
            giroList = businessLogic.GetGirosList();
            paisList = businessLogic.GetPaisesList();
            idiomaList = businessLogic.GetIdiomaList();
            tipoProveedorList = businessLogic.GetTipoProveedorList();
        }

        public ActionResult Index()
        {
            CargarCatalogos();

            ViewBag.AeropuertoList = aeropuertoList;
            ViewBag.GiroList = giroList;
            ViewBag.TipoProveedorList = tipoProveedorList;
            return View();
        }

        public ActionResult AceptaProveedor(int idProveedor)
        {
            return View();
        }

        public JsonResult GetProveedorEstatusList(int? idTipoProveedor, int? idGiroProveedor, string idAeropuerto, string nombreEmpresa, string rfc, string email)
        {
            try
            {
                BusinessLogic businessLogic = new BusinessLogic();
                ProveedorEstatusRequestDTO request = new ProveedorEstatusRequestDTO();
                request.ProveedorFiltro = new ProveedorFiltroDTO { IdTipoProveedor = idTipoProveedor, IdGiroProveedor = idGiroProveedor, IdAeropuerto = idAeropuerto, NombreEmpresa = nombreEmpresa, RFC = rfc, Email = email };

                var response = businessLogic.GetProveedorEstatusList(request);
                return Json(response.ProveedorList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetEstados(int idPais)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            estadoList = businessLogic.GetEstadoList(idPais);
            return Json(estadoList, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetMunicipios(int idEstado)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            municipioList = businessLogic.GetMunicipioList(idEstado);
            return Json(municipioList, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult About(int idProvider)
        {
            try
            {
                BusinessLogic businessLogic = new BusinessLogic();
                ProveedorDetalleRequestDTO request = new ProveedorDetalleRequestDTO();
                request.IdProveedor = idProvider;

                var response = businessLogic.GetProveedorElemento(request);
                return View(response.Proveedor);
              
            }
            catch (Exception ex)
            {

                return View();
            }


            //ViewBag.Message = "Your application description page.";

            //return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}