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
        public List<GiroDTO> giroList;
        public List<TipoProveedorDTO> tipoProveedorList;

        private void CargarCatalogos()
        {
            BusinessLogic businessLogic = new BusinessLogic();
            aeropuertoList = businessLogic.GetAeropuertosList();
            giroList = businessLogic.GetGirosList();
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

        public JsonResult GetProveedorEstatusList(int idTipoProveedor, int idGiroProveedor, string idAeropuerto, string nombreEmpresa, string rfc, string email)
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}