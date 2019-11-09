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
                    var cmd = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_Proveedor_INS, conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    request.Proveedor.AXFechaRegistro = DateTime.Now;
                    cmd.Parameters.Add(new SqlParameter("@NombreEmpresa", SqlDbType.NVarChar, 500)).Value = request.Proveedor.NombreEmpresa;
                    cmd.Parameters.Add(new SqlParameter("@RazonSocial", SqlDbType.NVarChar, 500)).Value = request.Proveedor.RazonSocial;
                    cmd.Parameters.Add(new SqlParameter("@RFC", SqlDbType.NVarChar, 40)).Value = request.Proveedor.RFC;
                    cmd.Parameters.Add(new SqlParameter("@NIF", SqlDbType.NVarChar, 30)).Value = request.Proveedor.NIF;
                    cmd.Parameters.Add(new SqlParameter("@ProvTelefono", SqlDbType.NVarChar, 50)).Value = request.Proveedor.ProvTelefono;
                    cmd.Parameters.Add(new SqlParameter("@ProvFax", SqlDbType.NVarChar, 50)).Value = request.Proveedor.ProvFax;
                    cmd.Parameters.Add(new SqlParameter("@PaginaWeb", SqlDbType.NVarChar, 500)).Value = request.Proveedor.PaginaWeb;
                    cmd.Parameters.Add(new SqlParameter("@IdZonaHoraria", request.Proveedor.IdZonaHoraria));
                    cmd.Parameters.Add(new SqlParameter("@IdTipoProveedor", request.Proveedor.IdTipoProveedor));
                    cmd.Parameters.Add(new SqlParameter("@AXNumeroProveedor", SqlDbType.NVarChar, 30)).Value = request.Proveedor.AXNumeroProveedor;
                    cmd.Parameters.Add(new SqlParameter("@AXFechaRegistro", request.Proveedor.AXFechaRegistro));
                    cmd.Parameters.Add(new SqlParameter("@IdNacionalidad", request.Proveedor.IdNacionalidad));
                    var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        cmd.ExecuteNonQuery();
                        var idProveedor = Convert.ToInt32(returnParameter.Value);
                        if (idProveedor > 0)
                        {
                            var resultado = 0;
                            foreach (var giro in request.Proveedor.GiroList)
                            {
                                var cmdGiro = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_ProveedorGiro_INS, conexion)
                                {
                                    CommandType = CommandType.StoredProcedure
                                };
                                cmdGiro.Parameters.Add(new SqlParameter("@IdProveedor", idProveedor));
                                cmdGiro.Parameters.Add(new SqlParameter("@IdCatalogoGiro", giro.IdGiro));
                                cmdGiro.Parameters.Add(new SqlParameter("Result", SqlDbType.BigInt) { Direction = ParameterDirection.ReturnValue });
                                cmdGiro.ExecuteNonQuery();
                                resultado = Convert.ToInt32(cmdGiro.Parameters["Result"].Value);
                                if (resultado < 1)
                                {
                                    response.Success = false;
                                    return response;
                                }
                            }

                            foreach (var empresa in request.Proveedor.EmpresaList)
                            {
                                var cmdEmpresa = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_ProveedorEmpresa_INS, conexion)
                                {
                                    CommandType = CommandType.StoredProcedure
                                };
                                cmdEmpresa.Parameters.Add(new SqlParameter("@IdProveedor", idProveedor));
                                cmdEmpresa.Parameters.Add(new SqlParameter("@IdCatalogoAeropuerto", SqlDbType.NVarChar, 50)).Value = empresa.IdCatalogoAeropuerto;
                                cmdEmpresa.Parameters.Add(new SqlParameter("Result", SqlDbType.BigInt) { Direction = ParameterDirection.ReturnValue });
                                cmdEmpresa.ExecuteNonQuery();
                                resultado = Convert.ToInt32(cmdEmpresa.Parameters["Result"].Value);
                                if (resultado < 1)
                                {
                                    response.Success = false;
                                    return response;
                                }
                            }

                            var cmdContacto = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_ProveedorContacto_INS, conexion)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            cmdContacto.Parameters.Add(new SqlParameter("@IdProveedor", idProveedor));
                            cmdContacto.Parameters.Add(new SqlParameter("@NombreContacto", SqlDbType.NVarChar, 300)).Value = request.Proveedor.Contacto.NombreContacto;
                            cmdContacto.Parameters.Add(new SqlParameter("@Cargo", SqlDbType.NVarChar, 250)).Value = request.Proveedor.Contacto.Cargo;
                            cmdContacto.Parameters.Add(new SqlParameter("@IdNacionalidad", request.Proveedor.Contacto.IdNacionalidad));
                            cmdContacto.Parameters.Add(new SqlParameter("@TelefonoDirecto", SqlDbType.NVarChar, 50)).Value = request.Proveedor.Contacto.TelefonoDirecto;
                            cmdContacto.Parameters.Add(new SqlParameter("@TelefonoMovil", SqlDbType.NVarChar, 50)).Value = request.Proveedor.Contacto.TelefonoMovil;
                            cmdContacto.Parameters.Add(new SqlParameter("@Fax", SqlDbType.NVarChar, 50)).Value = request.Proveedor.Contacto.Fax;
                            cmdContacto.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 250)).Value = request.Proveedor.Contacto.Email;
                            cmdContacto.Parameters.Add(new SqlParameter("@IdZonaHoraria", request.Proveedor.Contacto.IdZonaHoraria));
                            cmdContacto.Parameters.Add(new SqlParameter("@IdPais", request.Proveedor.Contacto.IdPais));
                            cmdContacto.Parameters.Add(new SqlParameter("@IdIdioma", request.Proveedor.Contacto.IdIdioma));
                            cmdContacto.Parameters.Add(new SqlParameter("Result", SqlDbType.BigInt) { Direction = ParameterDirection.ReturnValue });
                            cmdContacto.ExecuteNonQuery();
                            resultado = Convert.ToInt32(cmdContacto.Parameters["Result"].Value);

                            if (resultado < 1)
                            {
                                response.Success = false;
                                return response;
                            }

                            var cmdDireccion = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_ProveedorDireccion_INS, conexion)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            cmdDireccion.Parameters.Add(new SqlParameter("@IdProveedor", idProveedor));
                            cmdDireccion.Parameters.Add(new SqlParameter("@CodigoPostal", SqlDbType.NVarChar, 50)).Value = request.Proveedor.Direccion.CodigoPostal;
                            cmdDireccion.Parameters.Add(new SqlParameter("@Colonia", SqlDbType.NVarChar, 150)).Value = request.Proveedor.Direccion.Colonia;
                            cmdDireccion.Parameters.Add(new SqlParameter("@IdMunicipio", request.Proveedor.Direccion.IdMunicipio));
                            cmdDireccion.Parameters.Add(new SqlParameter("@Calle", SqlDbType.NVarChar, 150)).Value = request.Proveedor.Direccion.Calle;
                            cmdDireccion.Parameters.Add(new SqlParameter("@IdPais", request.Proveedor.Direccion.IdPais));
                            cmdDireccion.Parameters.Add(new SqlParameter("@Estado", SqlDbType.NVarChar, 150)).Value = request.Proveedor.Direccion.Estado;
                            cmdDireccion.Parameters.Add(new SqlParameter("@Municipio", SqlDbType.NVarChar, 250)).Value = request.Proveedor.Direccion.Municipio;
                            cmdDireccion.Parameters.Add(new SqlParameter("@DireccionValidada", request.Proveedor.Direccion.DireccionValidada));
                            cmdDireccion.Parameters.Add(new SqlParameter("Result", SqlDbType.BigInt) { Direction = ParameterDirection.ReturnValue });
                            cmdDireccion.ExecuteNonQuery();
                            resultado = Convert.ToInt32(cmdDireccion.Parameters["Result"].Value);

                            if (resultado < 1)
                            {
                                response.Success = false;
                                return response;
                            }

                            transactionScope.Complete();
                        }
                        response.Success = true;
                    }
                }
                response.Success = true;
                return response;
            };

            return tryCatch.SafeExecutor(action);
        }
    }
}
