using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EprocurementWeb.Business;
using EPROCUREMENT.GAPPROVEEDOR.Entities;

namespace EprocurementWeb.Controllers
{
    public class AltaProveedorController : Controller
    {
        public ActionResult InformacionBF(int idProveedor)
        {
            BusinessLogic business = new BusinessLogic();
            ProveedorDetalleRequestDTO request = new ProveedorDetalleRequestDTO();
            request.IdProveedor = idProveedor;
            var response = business.GetProveedorElemento(request).Proveedor;
            return View(response);
        }

        [HttpPost, ActionName("InformacionBF")]
        public ActionResult CargarArchivo(HttpPostedFileBase file)
        {
            var contenido = new byte[file.ContentLength];
            return RedirectToAction("InformacionBF");
        }

    }
}