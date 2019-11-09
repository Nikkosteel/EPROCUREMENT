using EPROCUREMENT.GAPPROVEEDOR.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPROCUREMENT.GAPPROVEEDOR.Data.Proveedor
{
    public class ProveedorData
    {
        private readonly TryCatchExecutor tryCatch;

        public ProveedorData()
        {
            tryCatch = new TryCatchExecutor();

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
                        SqlTransaction transaction = conexion.BeginTransaction();
                        var cmd = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_Proveedor_INS, conexion)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        
                        cmd.Parameters.Add(new SqlParameter("@NombreEmpresa", request.Proveedor.NombreEmpresa));
                        cmd.Parameters.Add(new SqlParameter("@RazonSocial", request.Proveedor.RazonSocial));
                        cmd.Parameters.Add(new SqlParameter("@RFC", request.Proveedor.RFC));
                        cmd.Parameters.Add(new SqlParameter("@NIF", request.Proveedor.NIF));
                        cmd.Parameters.Add(new SqlParameter("@ProvTelefono", request.Proveedor.ProvTelefono));
                        cmd.Parameters.Add(new SqlParameter("@ProvFax", request.Proveedor.ProvFax));
                        cmd.Parameters.Add(new SqlParameter("@PaginaWeb", request.Proveedor.PaginaWeb));
                        cmd.Parameters.Add(new SqlParameter("@IdZonaHoraria", request.Proveedor.IdZonaHoraria));
                        cmd.Parameters.Add(new SqlParameter("@IdTipoProveedor", request.Proveedor.IdTipoProveedor));
                        cmd.Parameters.Add(new SqlParameter("@AXNumeroProveedor", request.Proveedor.AXNumeroProveedor));
                        cmd.Parameters.Add(new SqlParameter("@AXFechaRegistro", request.Proveedor.AXFechaRegistro));
                        cmd.Parameters.Add(new SqlParameter("@IdNacionalidad", request.Proveedor.IdNacionalidad));
                        var idProveedor =  Convert.ToInt32(cmd.ExecuteScalar());
                        if(idProveedor > 0)
                        {
                            foreach(var giro in request.Proveedor.GiroList)
                            {
                                var cmdGiro = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_ProveedorGiro_INS, conexion)
                                {
                                    CommandType = CommandType.StoredProcedure
                                };
                                cmdGiro.Parameters.Add(new SqlParameter("@IdProveedor", idProveedor));
                                cmdGiro.Parameters.Add(new SqlParameter("@IdCatalogoGiro", giro.IdGiro));
                                if (Convert.ToInt32(cmdGiro.ExecuteScalar()) < 1)
                                {
                                    transaction.Rollback();
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
                                cmdEmpresa.Parameters.Add(new SqlParameter("@IdCatalogoAeropuerto", empresa.IdCatalogoAeropuerto));
                                if (Convert.ToInt32(cmdEmpresa.ExecuteScalar()) < 1)
                                {
                                    transaction.Rollback();
                                    response.Success = false;
                                    return response;
                                }
                            }

                            var cmdContacto = new SqlCommand(App_GlobalResources.StoredProcedures.usp_EPROCUREMENT_ProveedorContacto_INS, conexion)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            cmdContacto.Parameters.Add(new SqlParameter("@IdProveedor", idProveedor));
                            cmdContacto.Parameters.Add(new SqlParameter("@NombreContacto", request.Proveedor.Contacto.NombreContacto));
                            cmdContacto.Parameters.Add(new SqlParameter("@Cargo", request.Proveedor.Contacto.Cargo));
                            cmdContacto.Parameters.Add(new SqlParameter("@IdNacionalidad", request.Proveedor.Contacto.IdNacionalidad));
                            cmdContacto.Parameters.Add(new SqlParameter("@TelefonoDirecto", request.Proveedor.Contacto.TelefonoDirecto));
                            cmdContacto.Parameters.Add(new SqlParameter("@TelefonoMovil", request.Proveedor.Contacto.TelefonoMovil));
                            cmdContacto.Parameters.Add(new SqlParameter("@Fax", request.Proveedor.Contacto.Fax));
                            cmdContacto.Parameters.Add(new SqlParameter("@Email", request.Proveedor.Contacto.Email));
                            cmdContacto.Parameters.Add(new SqlParameter("@IdZonaHoraria", request.Proveedor.Contacto.IdZonaHoraria));
                            cmdContacto.Parameters.Add(new SqlParameter("@IdPais", request.Proveedor.Contacto.IdPais));
                            cmdContacto.Parameters.Add(new SqlParameter("@IdIdioma", request.Proveedor.Contacto.IdIdioma));

                            if (Convert.ToInt32(cmdContacto.ExecuteScalar()) < 1)
                            {
                                transaction.Rollback();
                                response.Success = false;
                                return response;
                            }

                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }

                    }
                    response.Success = true;
                    return response;
                };

                return tryCatch.SafeExecutor(action);
            }
        }
    }
}
