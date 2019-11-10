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
            //estadoList = businessLogic.GetEstadoList();
            //municipioList = businessLogic.GetMunicipioList();
        }

        /// <summary>
        /// Consumir API Método Traer Paises
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPaisesList()
        {
            var bslogic = new BusinessLogic();
            return Json(bslogic.GetPaisesList(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Consumir API Método Traer Aeropuertos
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAeropuertosList()
        {
            var bslogic = new BusinessLogic();
            return Json(bslogic.GetAeropuertosList(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Consumir API Método Traer Giros
        /// </summary>
        /// <returns></returns>
        public JsonResult GetGirosList()
        {
            var bslogic = new BusinessLogic();
            return Json(bslogic.GetGirosList(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Consumir API Método Traer Nacionalidad
        /// </summary>
        /// <returns></returns>
        public JsonResult GetNacionalidadList()
        {
            var bslogic = new BusinessLogic();
            return Json(bslogic.GetNacionalidadList(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Consumir API Método Traer ZonaHoraria
        /// </summary>
        /// <returns></returns>
        public JsonResult GetZonaHorariaList()
        {
            var bslogic = new BusinessLogic();
            return Json(bslogic.GetZonaHorariaList(), JsonRequestBehavior.AllowGet);

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