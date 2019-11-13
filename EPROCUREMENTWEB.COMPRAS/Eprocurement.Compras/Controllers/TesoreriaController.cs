using Eprocurement.Compras.Business;
using EPROCUREMENT.GAPPROVEEDOR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eprocurement.Compras.Controllers
{
    public class TesoreriaController : Controller
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
        // GET: Tesoreria
        private void CargarCatalogos()
        {
            BusinessLogic businessLogic = new BusinessLogic();
            aeropuertoList = businessLogic.GetAeropuertosList();
            giroList = businessLogic.GetGirosList();
            tipoProveedorList = businessLogic.GetTipoProveedorList();
        }


        public ActionResult AprobarTesoreria()
        {
            CargarCatalogos();

            ViewBag.AeropuertoList = aeropuertoList;
            ViewBag.GiroList = giroList;
            ViewBag.TipoProveedorList = tipoProveedorList;
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
                var proveedorEstatus = (from t in response.ProveedorList
                                       where t.Estatus.Contains("5,6,7,8")
                                       select t).ToList();
                return Json(proveedorEstatus, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return null;
            }
        }


    }
}