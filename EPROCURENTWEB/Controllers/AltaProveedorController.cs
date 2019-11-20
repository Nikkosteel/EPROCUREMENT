﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EprocurementWeb.Business;
using EPROCUREMENT.GAPPROVEEDOR.Entities;
using EPROCUREMENT.GAPPROVEEDOR.Entities.Proveedor;
using System.IO;
using EprocurementWeb.Filters;

namespace EprocurementWeb.Controllers
{
    public class AltaProveedorController : Controller
    {
        public ProveedorInformacionFinanciera cuenta = null;
        public List<ProveedorCuentaDTO>  ProveedorCuentaListRegistro = new List<ProveedorCuentaDTO>();
        public ActionResult InformacionBF(int idProveedor)
        {
            var usuarioInfo = new ValidaSession().ObtenerUsuarioSession();
            if (usuarioInfo != null)
            {
                idProveedor = usuarioInfo.IdProveedor;
            }
            Session["IdProveedor"] = idProveedor;
            BusinessLogic business = new BusinessLogic();
            Session["ProveedorCuentaListRegistro"] = new List<ProveedorCuentaDTO>();
            var aeropuertos = business.GetAeropuertosList();
            ViewBag.BancoList = business.GetBancoList();
            ViewBag.TipoCuentaList = business.GetTipoCuentaList();
            ProveedorDetalleRequestDTO request = new ProveedorDetalleRequestDTO();
            request.IdProveedor = idProveedor;
            var response = business.GetProveedorElemento(request).Proveedor;
            var aeropuertosAsignados = response.EmpresaList;
            var proveedorDocumento = business.GetCatalogoDocumentoList();
            var formatoDocumento = business.GetFormatoArchivoList();
            if (cuenta == null)
            {
                cuenta = new ProveedorInformacionFinanciera();
                cuenta.ProveedorCuentaList = new List<ProveedorCuentaDTO> { new ProveedorCuentaDTO { Cuenta = null, IdBanco = 0, CLABE = null, IdTipoCuenta = 0, IdProveedor = idProveedor } };
            }
            cuenta.RFC = response.RFC;
            cuenta.ProveedorCuentaList[0].AeropuertoList = (from aeropuerto in aeropuertos
                                                            join aeropuertoA in aeropuertosAsignados on aeropuerto.Id equals aeropuertoA.IdCatalogoAeropuerto
                                                            select new AeropuertoDTO { Id = aeropuerto.Id, Nombre = aeropuerto.Nombre, Checado = false }).ToList();
            cuenta.CatalogoDocumentoList = proveedorDocumento;
            cuenta.ProveedorCuentaListRegistro = new List<ProveedorCuentaDTO>();
            cuenta.CuentaBancaria = new ProveedorCuentaDTO();
            cuenta.CuentaBancaria.AeropuertoList = cuenta.ProveedorCuentaList[0].AeropuertoList;
            return View(cuenta);
        }

        public ActionResult AgregarCuenta(ProveedorInformacionFinanciera cuenta)
        {
            ProveedorCuentaListRegistro = new ValidaSession().RecuperaRegistrosSession();
            if (cuenta.ProveedorCuentaListRegistro == null)
            {
                cuenta.ProveedorCuentaListRegistro = new List<ProveedorCuentaDTO>();
            }
            if (ProveedorCuentaListRegistro == null)
            {
                ProveedorCuentaListRegistro = new List<ProveedorCuentaDTO>();
            }
            ProveedorCuentaListRegistro.Add(cuenta.ProveedorCuentaList.First());
            Session["ProveedorCuentaListRegistro"] = ProveedorCuentaListRegistro;
            cuenta.ProveedorCuentaListRegistro = ProveedorCuentaListRegistro;
            BusinessLogic business = new BusinessLogic();
            var aeropuertos = business.GetAeropuertosList();
            ViewBag.BancoList = business.GetBancoList();
            ViewBag.TipoCuentaList = business.GetTipoCuentaList();
            ProveedorDetalleRequestDTO request = new ProveedorDetalleRequestDTO();
            request.IdProveedor = cuenta.ProveedorCuentaList.First().IdProveedor;
            var response = business.GetProveedorElemento(request).Proveedor;
            var aeropuertosAsignados = response.EmpresaList;
            var proveedorDocumento = business.GetCatalogoDocumentoList();
            var formatoDocumento = business.GetFormatoArchivoList();
            if (cuenta == null)
            {
                cuenta = new ProveedorInformacionFinanciera();
                cuenta.ProveedorCuentaList = new List<ProveedorCuentaDTO> { new ProveedorCuentaDTO { Cuenta = null, IdBanco = 0, CLABE = null, IdTipoCuenta = 0, IdProveedor = cuenta.ProveedorCuentaList.First().IdProveedor } };
            }
            cuenta.RFC = response.RFC;
            cuenta.ProveedorCuentaList[0].AeropuertoList = (from aeropuerto in aeropuertos
                                                            join aeropuertoA in aeropuertosAsignados on aeropuerto.Id equals aeropuertoA.IdCatalogoAeropuerto
                                                            select new AeropuertoDTO { Id = aeropuerto.Id, Nombre = aeropuerto.Nombre, Checado = false }).ToList();
            cuenta.CatalogoDocumentoList = proveedorDocumento;
            //cuenta.ProveedorCuentaListRegistro = new List<ProveedorCuentaDTO>();
            cuenta.CuentaBancaria = new ProveedorCuentaDTO();
            cuenta.CuentaBancaria.AeropuertoList = cuenta.ProveedorCuentaList[0].AeropuertoList;

            return View("InformacionBF", cuenta);
        }

        [HttpPost]
        public ActionResult CargarCuentas(ProveedorInformacionFinanciera proveedor)
        {
            BusinessLogic business = new BusinessLogic();
            ProveedorCuentaListRegistro = new ValidaSession().RecuperaRegistrosSession();
            int idProveedor = new ValidaSession().RecuperaIdProveedorSession();
          
            var aeropuertos = business.GetAeropuertosList();
            ViewBag.BancoList = business.GetBancoList();
            ViewBag.TipoCuentaList = business.GetTipoCuentaList();
            proveedor.ProveedorCuentaListRegistro = ProveedorCuentaListRegistro;
            if (ProveedorCuentaListRegistro.Count > 0)
            {
                foreach (var registro in ProveedorCuentaListRegistro)
                {
                    proveedor.ProveedorCuentaList = new List<ProveedorCuentaDTO>();
                    proveedor.ProveedorCuentaList.Add(registro);
                    ProveedorCuentaRequestDTO requestg = new ProveedorCuentaRequestDTO { IdUsuario = 3, ProveedorCuentaList = proveedor.ProveedorCuentaList };
                    var responseg = business.GuardarProveedorCuenta(requestg);
                }
            }
            //if (response.Success)
            //{
            //    bool respuestaDoc = business.GuardarDocumentos(proveedor.RFC, proveedor.CatalogoDocumentoList);
            //    if (respuestaDoc)
            //    {
            //        ProveedorAprobarRequestDTO requestAprobador = new ProveedorAprobarRequestDTO { EstatusProveedor = new HistoricoEstatusProveedorDTO { IdEstatusProveedor = 5, IdProveedor = proveedor.ProveedorCuentaList[0].IdProveedor, IdUsuario = 3 } };
            //        var responseAprobar = business.SetProveedorEstatus(requestAprobador);
            //        if (responseAprobar.Success)
            //        {
            //            return View(proveedor);
            //        }
            // }
            //}
            ProveedorDetalleRequestDTO request = new ProveedorDetalleRequestDTO();
            request.IdProveedor = idProveedor;
            var response = business.GetProveedorElemento(request).Proveedor;
            var aeropuertosAsignados = response.EmpresaList;
            var proveedorDocumento = business.GetCatalogoDocumentoList();
            var formatoDocumento = business.GetFormatoArchivoList();
            if (cuenta == null)
            {
                cuenta = new ProveedorInformacionFinanciera();
                cuenta.ProveedorCuentaList = new List<ProveedorCuentaDTO> { new ProveedorCuentaDTO { Cuenta = null, IdBanco = 0, CLABE = null, IdTipoCuenta = 0, IdProveedor = idProveedor } };
            }
            cuenta.RFC = response.RFC;
            cuenta.ProveedorCuentaList[0].AeropuertoList = (from aeropuerto in aeropuertos
                                                            join aeropuertoA in aeropuertosAsignados on aeropuerto.Id equals aeropuertoA.IdCatalogoAeropuerto
                                                            select new AeropuertoDTO { Id = aeropuerto.Id, Nombre = aeropuerto.Nombre, Checado = false }).ToList();
            cuenta.CatalogoDocumentoList = proveedorDocumento;
            //cuenta.ProveedorCuentaListRegistro = new List<ProveedorCuentaDTO>();
            cuenta.CuentaBancaria = new ProveedorCuentaDTO();
            cuenta.CuentaBancaria.AeropuertoList = cuenta.ProveedorCuentaList[0].AeropuertoList;
            cuenta.ProveedorCuentaListRegistro = proveedor.ProveedorCuentaListRegistro;
            //cuenta.ProveedorCuentaList = new List<ProveedorCuentaDTO> { new ProveedorCuentaDTO { Cuenta = null, IdBanco = 0, CLABE = null, IdTipoCuenta = 0, IdProveedor = cuenta.ProveedorCuentaList.First().IdProveedor } };
            Session["ProveedorCuentaListRegistro"] = new List<ProveedorCuentaDTO>();
            return View("InformacionBF", cuenta);
        }


        [HttpPost, ActionName("InformacionBF")]
        public ActionResult CargarArchivo(ProveedorInformacionFinanciera proveedor)
        {
            BusinessLogic business = new BusinessLogic();
            int idProveedor = new ValidaSession().RecuperaIdProveedorSession();
            var aeropuertos = business.GetAeropuertosList();
            ViewBag.BancoList = business.GetBancoList();
            ViewBag.TipoCuentaList = business.GetTipoCuentaList();
            //ProveedorCuentaRequestDTO request = new ProveedorCuentaRequestDTO { IdUsuario = 3, ProveedorCuentaList = proveedor.ProveedorCuentaList };
            //var response = business.GuardarProveedorCuenta(request);
            //if (response.Success)
            //{
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
            //}
            ProveedorDetalleRequestDTO request = new ProveedorDetalleRequestDTO();
            request.IdProveedor = idProveedor;
            var response = business.GetProveedorElemento(request).Proveedor;
            var aeropuertosAsignados = response.EmpresaList;
            var proveedorDocumento = business.GetCatalogoDocumentoList();
            var formatoDocumento = business.GetFormatoArchivoList();
            if (cuenta == null)
            {
                cuenta = new ProveedorInformacionFinanciera();
                cuenta.ProveedorCuentaList = new List<ProveedorCuentaDTO> { new ProveedorCuentaDTO { Cuenta = null, IdBanco = 0, CLABE = null, IdTipoCuenta = 0, IdProveedor = idProveedor } };
            }
            cuenta.RFC = response.RFC;
            cuenta.ProveedorCuentaList[0].AeropuertoList = (from aeropuerto in aeropuertos
                                                            join aeropuertoA in aeropuertosAsignados on aeropuerto.Id equals aeropuertoA.IdCatalogoAeropuerto
                                                            select new AeropuertoDTO { Id = aeropuerto.Id, Nombre = aeropuerto.Nombre, Checado = false }).ToList();
            cuenta.CatalogoDocumentoList = proveedorDocumento;
            //cuenta.ProveedorCuentaListRegistro = new List<ProveedorCuentaDTO>();
            //cuenta.CuentaBancaria = new ProveedorCuentaDTO();
            //cuenta.ProveedorCuentaList[0].AeropuertoList = cuenta.ProveedorCuentaList[0].AeropuertoList;
            cuenta.ProveedorCuentaListRegistro = new List<ProveedorCuentaDTO>();

            return View(cuenta);
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