using EPROCUREMENT.GAPPROVEEDOR.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EPROCUREMENT.GAPPROVEEDOR.Data
{
    public class ProveedorData
    {
        private readonly TryCatchExecutor tryCatch;

        public ProveedorData()
        {
            tryCatch = new TryCatchExecutor();
        }

        /// <summary>
        /// Registra la información del proveedor
        /// </summary>
        /// <returns>Un objeto de tipo ProveedorResponseDTO</returns>
        public ProveedorResponseDTO ProveedorInsertar(ProveedorRequesteDTO request)
        {
            ProveedorResponseDTO response = new ProveedorResponseDTO()
            {
                ErrorList = new List<ErrorDTO>()
            };

            Func<ProveedorResponseDTO> action = () =>
            {

                using (var conexion = new SqlConnection(Helper.Connection()))
                {
                    conexion.Open();
                    var cmdProveedor = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_Proveedor_INS, conexion);

                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        var idProveedor = ExecuteComandProveedor(cmdProveedor, request.Proveedor);
                        if (idProveedor > 0)
                        {
                            foreach (var giro in request.Proveedor.GiroList)
                            {
                                var cmdGiro = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_ProveedorGiro_INS, conexion);
                                if (ExecuteComandGiro(cmdGiro, giro.IdGiro, idProveedor) < 1) { return response; }
                            }

                            foreach (var empresa in request.Proveedor.EmpresaList)
                            {
                                var cmdEmpresa = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_ProveedorEmpresa_INS, conexion);
                                empresa.IdProveedor = idProveedor;
                                if (ExecuteComandEmpresa(cmdEmpresa, empresa) < 1) { return response; }
                            }

                            var cmdContacto = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_ProveedorContacto_INS, conexion);
                            request.Proveedor.Contacto.IdProveedor = idProveedor;
                            if (ExecuteComandContacto(cmdContacto, request.Proveedor.Contacto) < 1) { return response; }

                            var cmdDireccion = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_ProveedorDireccion_INS, conexion);
                            request.Proveedor.Direccion.IdProveedor = idProveedor;
                            if (ExecuteComandDireccion(cmdDireccion, request.Proveedor.Direccion) < 1) { return response; }

                            transactionScope.Complete();
                            response.Success = true;
                        }
                    }
                }
                return response;
            };

            return tryCatch.SafeExecutor(action);
        }

        /// <summary>
        /// Obtiene un listado de provedores por filtro
        /// </summary>
        /// <param name="request">Un objeto de tipo ProveedorEstatusRequestDTO con los filtros</param>
        /// <returns>Un obejeto de tipo ProveedorEstatusResponseDTO</returns>
        public ProveedorEstatusResponseDTO GetProveedorEstatusList(ProveedorEstatusRequestDTO request)
        {
            ProveedorEstatusResponseDTO response = new ProveedorEstatusResponseDTO()
            {
                ProveedorList = new List<ProveedorEstatusDTO>()
            };

            ProveedorEstatusDTO proveedorEstatus = null;

            Func<ProveedorEstatusResponseDTO> action = () =>
            {
                using (var conexion = new SqlConnection(Helper.Connection()))
                {
                    conexion.Open();
                    var cmd = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_Proveedor_GETLByFilter, conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@nombreEmpresa", SqlDbType.NVarChar, 50)).Value = request.ProveedorFiltro.NombreEmpresa;
                    cmd.Parameters.Add(new SqlParameter("@idTipoProveedor", request.ProveedorFiltro.IdTipoProveedor));
                    cmd.Parameters.Add(new SqlParameter("@idGiro", request.ProveedorFiltro.IdGiroProveedor));
                    cmd.Parameters.Add(new SqlParameter("@idAeropuerto", request.ProveedorFiltro.IdAeropuerto));
                    cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar, 50)).Value = request.ProveedorFiltro.Email;
                    cmd.Parameters.Add(new SqlParameter("@rfc", SqlDbType.NVarChar, 50)).Value = request.ProveedorFiltro.RFC;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            proveedorEstatus = new ProveedorEstatusDTO();
                            proveedorEstatus.IdProveedor = Convert.ToInt32(reader["IdProveedor"]);
                            proveedorEstatus.RFC = reader["RFC"].ToString();
                            proveedorEstatus.NombreEmpresa = reader["NombreEmpresa"].ToString();
                            proveedorEstatus.Email = reader["Email"].ToString();
                            proveedorEstatus.Estatus = reader["Estatus"].ToString();
                            response.ProveedorList.Add(proveedorEstatus);
                        }
                    }
                }
                response.Success = true;
                return response;
            };

            return tryCatch.SafeExecutor(action);
        }

        /// <summary>
        /// Recupera los parametros para el registro del proveedor
        /// </summary>
        /// <param name="cmdProveedor">Información del comand</param>
        /// <param name="proveedor">Información del proveedor</param>
        /// <returns>La respuesta de la inserción</returns>
        private int ExecuteComandProveedor(SqlCommand cmdProveedor, ProveedorDTO proveedor)
        {
            proveedor.AXFechaRegistro = DateTime.Now;
            cmdProveedor.CommandType = CommandType.StoredProcedure;
            cmdProveedor.Parameters.Add(new SqlParameter("@NombreEmpresa", SqlDbType.NVarChar, 500)).Value = proveedor.NombreEmpresa;
            cmdProveedor.Parameters.Add(new SqlParameter("@RazonSocial", SqlDbType.NVarChar, 500)).Value = proveedor.RazonSocial;
            cmdProveedor.Parameters.Add(new SqlParameter("@RFC", SqlDbType.NVarChar, 40)).Value = proveedor.RFC;
            cmdProveedor.Parameters.Add(new SqlParameter("@NIF", SqlDbType.NVarChar, 30)).Value = proveedor.NIF;
            cmdProveedor.Parameters.Add(new SqlParameter("@ProvTelefono", SqlDbType.NVarChar, 50)).Value = proveedor.ProvTelefono;
            cmdProveedor.Parameters.Add(new SqlParameter("@ProvFax", SqlDbType.NVarChar, 50)).Value = proveedor.ProvFax;
            cmdProveedor.Parameters.Add(new SqlParameter("@PaginaWeb", SqlDbType.NVarChar, 500)).Value = proveedor.PaginaWeb;
            cmdProveedor.Parameters.Add(new SqlParameter("@IdZonaHoraria", proveedor.IdZonaHoraria));
            cmdProveedor.Parameters.Add(new SqlParameter("@IdTipoProveedor", proveedor.IdTipoProveedor));
            cmdProveedor.Parameters.Add(new SqlParameter("@AXNumeroProveedor", SqlDbType.NVarChar, 30)).Value = proveedor.AXNumeroProveedor;
            cmdProveedor.Parameters.Add(new SqlParameter("@AXFechaRegistro", proveedor.AXFechaRegistro));
            cmdProveedor.Parameters.Add(new SqlParameter("@IdNacionalidad", proveedor.IdNacionalidad));
            cmdProveedor.Parameters.Add(new SqlParameter("Result", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });
            cmdProveedor.ExecuteNonQuery();
            var idProveedor = Convert.ToInt32(cmdProveedor.Parameters["Result"].Value);
            return idProveedor;
        }

        /// <summary>
        /// Recupera los parametros para el registro del contacto
        /// </summary>
        /// <param name="cmdContacto">Información del comand</param>
        /// <param name="contacto">Información del contacto</param>
        /// <returns>La respuesta de la inserción</returns>
        private int ExecuteComandContacto(SqlCommand cmdContacto, ProveedorContactoDTO contacto)
        {
            cmdContacto.CommandType = CommandType.StoredProcedure;
            cmdContacto.Parameters.Add(new SqlParameter("@IdProveedor", contacto.IdProveedor));
            cmdContacto.Parameters.Add(new SqlParameter("@NombreContacto", SqlDbType.NVarChar, 300)).Value = contacto.NombreContacto;
            cmdContacto.Parameters.Add(new SqlParameter("@Cargo", SqlDbType.NVarChar, 250)).Value = contacto.Cargo;
            cmdContacto.Parameters.Add(new SqlParameter("@IdNacionalidad", contacto.IdNacionalidad));
            cmdContacto.Parameters.Add(new SqlParameter("@TelefonoDirecto", SqlDbType.NVarChar, 50)).Value = contacto.TelefonoDirecto;
            cmdContacto.Parameters.Add(new SqlParameter("@TelefonoMovil", SqlDbType.NVarChar, 50)).Value = contacto.TelefonoMovil;
            cmdContacto.Parameters.Add(new SqlParameter("@Fax", SqlDbType.NVarChar, 50)).Value = contacto.Fax;
            cmdContacto.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 250)).Value = contacto.Email;
            cmdContacto.Parameters.Add(new SqlParameter("@IdZonaHoraria", contacto.IdZonaHoraria));
            cmdContacto.Parameters.Add(new SqlParameter("@IdPais", contacto.IdPais));
            cmdContacto.Parameters.Add(new SqlParameter("@IdIdioma", contacto.IdIdioma));
            cmdContacto.Parameters.Add(new SqlParameter("Result", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });
            cmdContacto.ExecuteNonQuery();
            var resultado = Convert.ToInt32(cmdContacto.Parameters["Result"].Value);
            return resultado;
        }

        /// <summary>
        /// Recupera los parametros para el registro de la dirección
        /// </summary>
        /// <param name="cmdDireccion">Información del comand</param>
        /// <param name="direccion">Información de la dirección</param>
        /// <returns>La respuesta de la inserción</returns>
        private int ExecuteComandDireccion(SqlCommand cmdDireccion, ProveedorDireccionDTO direccion)
        {
            cmdDireccion.CommandType = CommandType.StoredProcedure;
            cmdDireccion.Parameters.Add(new SqlParameter("@IdProveedor", direccion.IdProveedor));
            cmdDireccion.Parameters.Add(new SqlParameter("@CodigoPostal", SqlDbType.NVarChar, 50)).Value = direccion.CodigoPostal;
            cmdDireccion.Parameters.Add(new SqlParameter("@Colonia", SqlDbType.NVarChar, 150)).Value = direccion.Colonia;
            cmdDireccion.Parameters.Add(new SqlParameter("@IdMunicipio", direccion.IdMunicipio));
            cmdDireccion.Parameters.Add(new SqlParameter("@Calle", SqlDbType.NVarChar, 150)).Value = direccion.Calle;
            cmdDireccion.Parameters.Add(new SqlParameter("@IdPais", direccion.IdPais));
            cmdDireccion.Parameters.Add(new SqlParameter("@Estado", SqlDbType.NVarChar, 150)).Value = direccion.Estado;
            cmdDireccion.Parameters.Add(new SqlParameter("@Municipio", SqlDbType.NVarChar, 250)).Value = direccion.Municipio;
            cmdDireccion.Parameters.Add(new SqlParameter("@DireccionValidada", direccion.DireccionValidada));
            cmdDireccion.Parameters.Add(new SqlParameter("Result", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });
            cmdDireccion.ExecuteNonQuery();
            var resultado = Convert.ToInt32(cmdDireccion.Parameters["Result"].Value);
            return resultado;
        }

        /// <summary>
        /// Recupera los parametros para el registro de la empresa
        /// </summary>
        /// <param name="cmdEmpresa">Información del comand</param>
        /// <param name="empresa">Información de la empresa</param>
        /// <returns>La respuesta de la inserción</returns>
        private int ExecuteComandEmpresa(SqlCommand cmdEmpresa, ProveedorEmpresaDTO empresa)
        {
            cmdEmpresa.CommandType = CommandType.StoredProcedure;
            cmdEmpresa.Parameters.Add(new SqlParameter("@IdProveedor", empresa.IdProveedor));
            cmdEmpresa.Parameters.Add(new SqlParameter("@IdCatalogoAeropuerto", SqlDbType.NVarChar, 50)).Value = empresa.IdCatalogoAeropuerto;
            cmdEmpresa.Parameters.Add(new SqlParameter("Result", SqlDbType.BigInt) { Direction = ParameterDirection.ReturnValue });
            cmdEmpresa.ExecuteNonQuery();
            var resultado = Convert.ToInt32(cmdEmpresa.Parameters["Result"].Value);
            return resultado;
        }

        /// <summary>
        /// Recupera los parametros para el registro del giro
        /// </summary>
        /// <param name="cmdGiro">Información del comand</param>
        /// <param name="idAeropuerto">Identificador del aeropuerto</param>
        /// <param name="idProveedor">Identificador del proovedor</param>
        /// <returns>La respuesta de la inserción</returns>
        private int ExecuteComandGiro(SqlCommand cmdGiro, int idGiro, int idProveedor)
        {
            cmdGiro.CommandType = CommandType.StoredProcedure;
            cmdGiro.Parameters.Add(new SqlParameter("@IdProveedor", idProveedor));
            cmdGiro.Parameters.Add(new SqlParameter("@IdCatalogoGiro", SqlDbType.NVarChar, 50)).Value = idGiro;
            cmdGiro.Parameters.Add(new SqlParameter("Result", SqlDbType.BigInt) { Direction = ParameterDirection.ReturnValue });
            cmdGiro.ExecuteNonQuery();
            var resultado = Convert.ToInt32(cmdGiro.Parameters["Result"].Value);
            return resultado;
        }
    }
}
