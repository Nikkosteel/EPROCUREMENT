using EprocurementWeb.Business;
using EprocurementWeb.Filters;
using EprocurementWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EprocurementWeb.Controllers
{
    public class MiCuentaController : Controller
    {
        // GET: MiCuenta
        public ActionResult Index()
        {
            ViewBag.Respuesta = "";
            return View();
        }

        [HttpPost]
        public ActionResult ActualizarPassword(ActualizaPasswordModel actualizaPassword)
        {
            if (ModelState.IsValid)
            {
                var usuarioInfo = new ValidaSession().ObtenerUsuarioSession();
                if (new SeguridadBusiness().ResetPasswordUsuario(actualizaPassword, usuarioInfo.IdUsuario))
                {
                    ViewBag.Respuesta = "Se ha actualizado su contraseña";
                }
                else
                {
                    ModelState.AddModelError("ErrorGenerico", "Se genero un error al procesar la solicitud");
                }
            }
            return View("Index");
        }
    }
}