using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EprocurementWeb.Models
{
    public class ProveedorModel
    {
        public int Id { get; set; }
        public string NombreEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public string NIF { get; set; }
        public string TelefonoProveedor { get; set; }
        public string FaxProveedor { get; set; }
        public string WebProveedor { get; set; }
        public int ZonaHoraria { get; set; }
        public string AXNumeroProveedor { get; set; }
        public DateTime AXFechaRegistro { get; set; }
        public int TipoProveedor { get; set; }
        public int Nacionalidad { get; set; }
        public ContactModel Contacto { get; set; }
        public List<EmpresaModel> Empresas { get; set; }
        public List<GiroModel> Giros { get; set; }
    }

    public class ContactoModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public int Nacionalidad { get; set; }
        public string TelefonoDirecto { get; set; }
        public string TelefonoMovil { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int ZonaHoraria { get; set; }
        public int Pais { get; set; }
        public int Idioma { get; set; }
    }

    public class EmpresaModel
    {
        public int Id { get; set; }
        public int IdCatalogoAeropuerto { get; set; }

    }

    public class GiroModel
    {
        public int Id { get; set; }
        public int IdCatalogoGiro { get; set; }
    }
}