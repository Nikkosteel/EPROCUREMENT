using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using EprocurementWeb.Models;
using System.Configuration;
using EPROCUREMENT.GAPPROVEEDOR.Entities;

namespace EprocurementWeb.Business
{
    public class BusinessLogic
    {
        string urlApi = ConfigurationManager.AppSettings["urlApi"].ToString();
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
            var lstGiros= new List<GiroDTO>();
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
    }
}