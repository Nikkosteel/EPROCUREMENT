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
            var aeropuertos = business.GetAeropuertosList();
            ProveedorDetalleRequestDTO request = new ProveedorDetalleRequestDTO();
            request.IdProveedor = idProveedor;
            var response = business.GetProveedorElemento(request).Proveedor;
            var aeropuertosAsignados = response.EmpresaList;
            ProveedorCuentaDTO cuenta = new ProveedorCuentaDTO();
            cuenta.AeropuertoList = (from aeropuerto in aeropuertos
                                     join aeropuertoA in aeropuertosAsignados on aeropuerto.Id equals aeropuertoA.IdCatalogoAeropuerto
                                     select new AeropuertoDTO { Id = aeropuerto.Id, Nombre = aeropuerto.Nombre, Checado = false }).ToList();
            return View(cuenta);
        }

        [HttpPost, ActionName("InformacionBF")]
        public ActionResult CargarArchivo(HttpPostedFileBase file)
        {
            var contenido = new byte[file.ContentLength];
            return RedirectToAction("InformacionBF");
        }

    }
}