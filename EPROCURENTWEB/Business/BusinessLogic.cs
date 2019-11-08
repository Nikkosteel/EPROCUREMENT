using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using EprocurementWeb.Models;
using EprocurementWeb.Entities.Response;
using EprocurementWeb.Entities;

namespace EprocurementWeb.Business
{
    public class BusinessLogic
    {

        public List<PaisDTO> getPaises()
        {
            var lstPaises = new List<PaisDTO> ();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54535/api/Catalogo/");
                //HTTP GET
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
    }
}