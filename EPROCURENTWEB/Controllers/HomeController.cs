﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EprocurementWeb.Models;
using EprocurementWeb.Content.Texts;
using System.Net.Http;
using System.Web.Script.Serialization;
using EprocurementWeb.Business;
using System.Threading.Tasks;
using EPROCUREMENT.GAPPROVEEDOR.Entities;
using EprocurementWeb.Models;

namespace EprocurementWeb.Controllers
{
    public class HomeController : BaseController
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

        public ActionResult Index()
        {
            CargarCatalogos();
            ProveedorRegistro proveedor = new ProveedorRegistro { Contacto = null, Direccion = null };
            proveedor.AeropuertoList = aeropuertoList;
            ViewBag.GiroList = giroList;
            ViewBag.ZonaHorariaList = zonaHorariaList;
            ViewBag.NacionalidadList = nacionalidadList;
            ViewBag.PaisList = paisList;
            ViewBag.IdiomaList = idiomaList;
            ViewBag.EstadoList = estadoList;
            ViewBag.MunicipioList = municipioList;
            ViewBag.TipoProveedorList = tipoProveedorList;
            return View(proveedor);
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarProveedor(ProveedorRegistro proveedor)
        {
            proveedor.Direccion.DireccionValidada = true;
            CargarCatalogos();
            ViewBag.GiroList = giroList;
            ViewBag.ZonaHorariaList = zonaHorariaList;
            ViewBag.NacionalidadList = nacionalidadList;
            ViewBag.PaisList = paisList;
            ViewBag.IdiomaList = idiomaList;
            ViewBag.EstadoList = estadoList;
            ViewBag.MunicipioList = municipioList;
            ViewBag.TipoProveedorList = tipoProveedorList;
            proveedor.EmpresaList = proveedor.AeropuertoList.Where(a => a.Checado).Select(a => new ProveedorEmpresaDTO { IdCatalogoAeropuerto = a.Id }).ToList();
            BusinessLogic businessLogic = new BusinessLogic();
            ProveedorResponseDTO response = businessLogic.PostProveedor(proveedor);
            if (response.Success)
            {
                return RedirectToAction("Contact");
            }
            return View(proveedor);
        }

        private void CargarCatalogos()
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

        public ActionResult About()
        {
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:54535/api/Proveedor/");
            //    //HTTP GET
            //    var responseTask = client.GetAsync("getlist");
            //    responseTask.Wait();


            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsStringAsync();
            //    JavaScriptSerializer JSserializer = new JavaScriptSerializer();

            //    var proveedor = JSserializer.Deserialize<List<ProveedorModel>>(readTask.Result);
            //    var list = proveedor;
            //    //    readTask.Wait();
            //    //    var proveedores = readTask.Result;
            //    }
            //    //else //web api sent error response 
            //    //{
            //    //    //log response status here..

            //    //    students = Enumerable.Empty<StudentViewModel>();

            //    //    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            //    //}
            //}


            ViewBag.Title = RHome.About;
            ViewBag.Message = RHome.AboutMessage;

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Title = RHome.Contact;
            ViewBag.Message = RHome.ContactMessage;
            ViewBag.ContactResult = TempData["ContactResult"];
            ViewBag.ContactResultMessage = TempData["ContactResultMessage"] ?? "";
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            ViewBag.Title = RHome.Contact;
            ViewBag.Message = RHome.ContactMessage;
            if (ModelState.IsValid)
            {
                /* Do something with this information */
                TempData["ContactResult"] = true;
                TempData["ContactResultMessage"] = RHome.ContactMessageSendOk;
                return RedirectToAction("Contact"); /* Post-Redirect-Get Pattern */
            }
            ViewBag.ContactResult = false;
            ViewBag.ContactResultMessage = RHome.ContactMessageSendNok;
            return View(model);
        }
    }
}