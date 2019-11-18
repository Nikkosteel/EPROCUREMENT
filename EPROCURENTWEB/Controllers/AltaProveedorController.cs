﻿using System;
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
                bool respuestaDoc = business.GuardarDocumentos(proveedor.RFC, proveedor.CatalogoDocumentoList);
                if (respuestaDoc)
                {
                    ProveedorAprobarRequestDTO requestAprobador = new ProveedorAprobarRequestDTO { EstatusProveedor = new HistoricoEstatusProveedorDTO { IdEstatusProveedor = 5, IdProveedor = proveedor.ProveedorCuentaList[0].IdProveedor, IdUsuario = 3 } };
                    var responseAprobar = business.SetProveedorEstatus(requestAprobador);
                    if (responseAprobar.Success)
                    {
                        return View(proveedor);
                    }
                }
            }
            return View(proveedor);
        }

        public ActionResult DocumentacionBF(int idProveedor)
        {
            BusinessLogic business = new BusinessLogic();
            var proveedorDocumento = business.GetCatalogoDocumentoList();
            ProveedorDetalleRequestDTO request = new ProveedorDetalleRequestDTO();
            request.IdProveedor = idProveedor;
            var response = business.GetProveedorElemento(request).Proveedor;
            ProveedorInformacionFinanciera cuenta = new ProveedorInformacionFinanciera();
            cuenta.RFC = response.RFC;
            cuenta.CatalogoDocumentoList = business.ObtenerDocumentos(cuenta.RFC, proveedorDocumento);
            return View(cuenta);
        }

        public FileResult DescargarArchivo(string ruta, string nombre)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@ruta);
            string fileName = nombre;

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

    }
}