﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using EprocurementWeb.Models;
using System.Configuration;
using EPROCUREMENT.GAPPROVEEDOR.Entities;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using EprocurementWeb.Properties;
using System.IO;

namespace EprocurementWeb.Business
{
    public class BusinessLogic
    {
        string urlApi = ConfigurationManager.AppSettings["urlApi"].ToString();

        public ProveedorEstatusResponseDTO GetProveedorEstatusList(ProveedorEstatusRequestDTO request)
        {
            ProveedorEstatusResponseDTO response = new ProveedorEstatusResponseDTO();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi + "api/Proveedor/");
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var responseTask = client.PostAsync("ProveedorEstatusList", content);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSSerializer = new JavaScriptSerializer();
                    response = JSSerializer.Deserialize<ProveedorEstatusResponseDTO>(readTask.Result);
                }
            }
            return response;
        }

        public ProveedorDetalleResponseDTO GetProveedorElemento(ProveedorDetalleRequestDTO request)
        {
            ProveedorDetalleResponseDTO response = new ProveedorDetalleResponseDTO();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi + "api/Proveedor/");
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var responseTask = client.PostAsync("ProveedorElemento", content);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSSerializer = new JavaScriptSerializer();
                    response = JSSerializer.Deserialize<ProveedorDetalleResponseDTO>(readTask.Result);

                }
            }
            return response;
        }

        public List<PaisDTO> GetPaisesList()
        {
            var lstPaises = new List<PaisDTO>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi + "api/Catalogo/");
                var responseTask = client.GetAsync("PaisGetList");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();

                    var response = JSserializer.Deserialize<PaisResponseDTO>(readTask.Result);
                    lstPaises = response.PaisList;
                }
            }
            return lstPaises;
        }
        public List<AeropuertoDTO> GetAeropuertosList()
        {
            var lstAeropuertos = new List<AeropuertoDTO>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi + "api/Catalogo/");
                var responseTask = client.GetAsync("AeropuertoGetList");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();

                    var response = JSserializer.Deserialize<AeropuertoResponseDTO>(readTask.Result);
                    lstAeropuertos = response.AeropuertoList;
                }
            }
            return lstAeropuertos;
        }
        public List<GiroDTO> GetGirosList()
        {
            var lstGiros = new List<GiroDTO>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi + "api/Catalogo/");
                var responseTask = client.GetAsync("GiroGetList");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();

                    var response = JSserializer.Deserialize<GiroResponseDTO>(readTask.Result);
                    lstGiros = response.GiroList;
                }
            }
            return lstGiros;
        }
        public List<NacionalidadDTO> GetNacionalidadList()
        {
            var lstNacionalidad = new List<NacionalidadDTO>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi + "api/Catalogo/");
                var responseTask = client.GetAsync("NacionalidadGetList");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();

                    var response = JSserializer.Deserialize<NacionalidadResponseDTO>(readTask.Result);
                    lstNacionalidad = response.NacionalidadList;
                }
            }
            return lstNacionalidad;
        }

        public List<ZonaHorariaDTO> GetZonaHorariaList()
        {
            var lstZonaHoraria = new List<ZonaHorariaDTO>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi + "api/Catalogo/");
                var responseTask = client.GetAsync("ZonaHorariaGetList");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();

                    var response = JSserializer.Deserialize<ZonaHorariaResponseDTO>(readTask.Result);
                    lstZonaHoraria = response.ZonaHorariaList;
                }
            }
            return lstZonaHoraria;
        }

        public List<IdiomaDTO> GetIdiomaList()
        {
            var lstIdioma = new List<IdiomaDTO>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi + "api/Catalogo/");
                var responseTask = client.GetAsync("IdiomaGetList");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();

                    var response = JSserializer.Deserialize<IdiomaResponseDTO>(readTask.Result);
                    lstIdioma = response.IdiomaList;
                }
            }
            return lstIdioma;
        }

        public List<EstadoDTO> GetEstadoList(int idPais)
        {
            List<EstadoDTO> lstEstado = new List<EstadoDTO>();
            EstadoRequesteDTO estado = new EstadoRequesteDTO { idPais = idPais };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi + "api/Catalogo/");
                var json = JsonConvert.SerializeObject(estado);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var responseTask = client.PostAsync("EstadoGetList", content);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSSerializer = new JavaScriptSerializer();
                    var response = JSSerializer.Deserialize<EstadoResponseDTO>(readTask.Result);
                    lstEstado = response.EstadoList;
                }
            }
            return lstEstado;
        }

        public List<MunicipioDTO> GetMunicipioList(int idEstado)
        {
            List<MunicipioDTO> lstMunicipio = new List<MunicipioDTO>();
            MunicipioRequesteDTO estado = new MunicipioRequesteDTO { idEstado = idEstado };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi + "api/Catalogo/");
                var json = JsonConvert.SerializeObject(estado);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var responseTask = client.PostAsync("MunicipioGetList", content);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSSerializer = new JavaScriptSerializer();
                    var response = JSSerializer.Deserialize<MunicipioResponseDTO>(readTask.Result);
                    lstMunicipio = response.MunicipioList;
                }
            }
            return lstMunicipio;
        }

        public List<TipoProveedorDTO> GetTipoProveedorList()
        {
            var lstTipoProveedor = new List<TipoProveedorDTO>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi + "api/Catalogo/");
                var responseTask = client.GetAsync("TipoProveedorGetList");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();

                    var response = JSserializer.Deserialize<TipoProveedorResponseDTO>(readTask.Result);
                    lstTipoProveedor = response.TipoProveedorList;
                }
            }
            return lstTipoProveedor;
        }

        public ProveedorResponseDTO PostProveedor(ProveedorRegistro proveedor)
        {
            ProveedorResponseDTO response = null;
            ProveedorRequesteDTO proveedorRequest = new ProveedorRequesteDTO { IdUsuario = 0, Proveedor = proveedor };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi + "api/Proveedor/");
                var json = JsonConvert.SerializeObject(proveedorRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var responseTask = client.PostAsync("Insertar", content);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSSerializer = new JavaScriptSerializer();
                    response = JSSerializer.Deserialize<ProveedorResponseDTO>(readTask.Result);
                }
            }
            return response;
        }

        public void GuardarDocumentos(string rfc, HttpFileCollectionBase archivos)
        {
            string ruta = Settings.Default["RutaDocumentos"].ToString();
            string rutaF = HttpContext.Current.Server.MapPath(ruta);
            string rutaP = rutaF + "\\" + rfc;

            if (!Directory.Exists(rutaF))
            {
                Directory.CreateDirectory(rutaF);
            }

            if (!Directory.Exists(rutaP))
            {
                Directory.CreateDirectory(rutaP);
            }

            try
            {

            }
            catch (Exception excep)
            {

            }
        }
    }
}