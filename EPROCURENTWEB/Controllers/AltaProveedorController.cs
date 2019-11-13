using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EprocurementWeb.Business;
using EPROCUREMENT.GAPPROVEEDOR.Entities;
using EPROCUREMENT.GAPPROVEEDOR.Entities.Proveedor;
using System.IO;

namespace EprocurementWeb.Controllers
{
    public class AltaProveedorController : Controller
    {
        public ActionResult InformacionBF(int idProveedor)
        {
            BusinessLogic business = new BusinessLogic();
            var aeropuertos = business.GetAeropuertosList();
            ViewBag.BancoList = business.GetBancoList();
            ViewBag.TipoCuentaList = business.GetTipoCuentaList();
            ProveedorDetalleRequestDTO request = new ProveedorDetalleRequestDTO();
            request.IdProveedor = idProveedor;
            var response = business.GetProveedorElemento(request).Proveedor;
            var aeropuertosAsignados = response.EmpresaList;
            var proveedorDocumento = business.GetCatalogoDocumentoList();
            var formatoDocumento = business.GetFormatoArchivoList();
            ProveedorInformacionFinanciera cuenta = new ProveedorInformacionFinanciera();
            cuenta.RFC = response.RFC;
            cuenta.ProveedorCuentaList = new List<ProveedorCuentaDTO> { new ProveedorCuentaDTO { Cuenta = null, IdBanco = 0, CLABE = null, IdTipoCuenta = 0, IdProveedor = idProveedor } };
            cuenta.ProveedorCuentaList[0].AeropuertoList = (from aeropuerto in aeropuertos
                                                            join aeropuertoA in aeropuertosAsignados on aeropuerto.Id equals aeropuertoA.IdCatalogoAeropuerto
                                                            select new AeropuertoDTO { Id = aeropuerto.Id, Nombre = aeropuerto.Nombre, Checado = false }).ToList();
            cuenta.CatalogoDocumentoList = proveedorDocumento;
            return View(cuenta);
        }

        [HttpPost, ActionName("InformacionBF")]
        public ActionResult CargarArchivo(ProveedorInformacionFinanciera proveedor)
        {
            BusinessLogic business = new BusinessLogic();
            var aeropuertos = business.GetAeropuertosList();
            ViewBag.BancoList = business.GetBancoList();
            ViewBag.TipoCuentaList = business.GetTipoCuentaList();
            ProveedorCuentaRequestDTO request = new ProveedorCuentaRequestDTO { IdUsuario = 3, ProveedorCuentaList = proveedor.ProveedorCuentaList };
            var response = business.GuardarProveedorCuenta(request);
            if (response.Success)
            {
                business.GuardarDocumentos(proveedor.RFC, proveedor.CatalogoDocumentoList);
            }
            return View(proveedor);
        }
    }
}