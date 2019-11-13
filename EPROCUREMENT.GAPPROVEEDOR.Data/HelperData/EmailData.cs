using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EPROCUREMENT.GAPPROVEEDOR.Entities;

namespace EPROCUREMENT.GAPPROVEEDOR.Data
{
    public class EmailData
    {
        /// <summary>
        /// Envia el correo electronico a los destinatarios
        /// </summary>
        /// <param name="emailEntity">Representa la información del correo a enviar</param>
        public void Enviar(int idProveedor, int idUsuario)
        {
            string EmailOrigen = "GAPProveedoresTest@gmail.com";
           // string Contraseña = "Kal08Test";
            //string url = "http://localhost:7886//Access/Recovery/?token=aab6714c9f6328f8dea4210141369515fa4f6ba40b31d0eaea9880c93d7d162f";
            string url = "http://localhost:7886//Access/Recovery/?token=aab6714c9f6328f8dea4210141369515fa4f6ba40b31d0eaea9880c93d7d162f";
            string hrefUrl = "<a href='" + url + "'>Click para ingresar</a>";
            var proveedorUsuario = new ProveedorData().GetProvedorUsuarioItem(idProveedor, idUsuario);
            EmailDTO emailEntidad = new EmailDTO
            {
                Origin = EmailOrigen,
                Subject = "Login de proveedor",
                Html = true,
                RecipientsList = new List<DireccionEmailDTO>(),
                Prioridad = EmailPrioridadDTO.Normal
            };
        
            emailEntidad.Message = GetMaailBody(proveedorUsuario, hrefUrl);
            emailEntidad.RecipientsList.Add(new DireccionEmailDTO { Address = proveedorUsuario.Email, DisplayName = proveedorUsuario.NombreEmpresa, UserIdentifier = 1 });             
            var mailMessage = ObtenerMensajeEmail(emailEntidad);
            var cliente = ObtenerClienteSmtp();
            try
            {
                cliente.Send(mailMessage);
            }
            catch (SmtpFailedRecipientException smtpFailedException)
            {
            }
        }

        /// <summary>
        /// Prepara el detalle del email para logueo del proveedor
        /// </summary>
        /// <param name="nombreCompañia">Nombre de la compañia</param>
        /// <param name="urlLogin">Url para el logueo</param>
        /// <returns>La estructura del correo</returns>
        private string GetMaailBody(ProveedorUsuarioDTO proveedorUsuario, string urlLogin)
        {
            string layoutName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, App_GlobalResources.ResourceConstants.EmailLayout, "UserLogin.htm");
            string message = File.ReadAllText(layoutName);
            var details = new StringBuilder();
            message = message.Replace("<!--NombreCompania-->", proveedorUsuario.NombreEmpresa);
            message = message.Replace("<!--RFCCompania-->", proveedorUsuario.RFC);
            message = message.Replace("<!--ContaseñaCompania-->", proveedorUsuario.Password);
            message = message.Replace("<!--urlAction-->", urlLogin);
            return message;
        }

        /// <summary>
        /// Obtiene una instancia de la entidad SmtpClient
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private SmtpClient ObtenerClienteSmtp()
        {
            string EmailOrigen = "GAPProveedoresTest@gmail.com";
            string Contraseña = "Kal08Test";
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PORT"]);
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);
            return oSmtpClient;
            //return new SmtpClient
            //{
            //    Host = ConfigurationManager.AppSettings["SERVER"],
            //    Port = Convert.ToInt32(ConfigurationManager.AppSettings["PORT"]),
            //    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailOrigen"], "Kal08Test"),
            //    EnableSsl = true,
            //    UseDefaultCredentials = false
            //};
            //return new SmtpClient
            //{
            //    Host = ConfigurationManager.AppSettings["SERVER"],
            //    Port = Convert.ToInt32(ConfigurationManager.AppSettings["PORT"]),
            //    DeliveryMethod = SmtpDeliveryMethod.Network
            //};
        }

        /// <summary>
        /// Obtiene una instancia de la entidad MailMessage
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        private MailMessage ObtenerMensajeEmail(EmailDTO entidad)
        {
            var mailMessage = new MailMessage();
            var mensaje = new StringBuilder();
            try
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["EmailOrigen"], ConfigurationManager.AppSettings["TituloEmail"]);
                mailMessage.Subject = entidad.Subject;
                mailMessage.Body = entidad.Message;
                mailMessage.IsBodyHtml = entidad.Html;

                if (entidad.RecipientsList != null && entidad.RecipientsList.Count > 0)
                {
                    foreach (var address in entidad.RecipientsList.Where(address => !string.IsNullOrWhiteSpace(address.Address)))
                    {
                        if (EsEmailValido(address.Address))
                        {
                            mailMessage.To.Add(new MailAddress(address.Address, address.DisplayName));
                        }
                    }
                }

                switch (entidad.Prioridad)
                {
                    case EmailPrioridadDTO.High:
                        mailMessage.Priority = MailPriority.High;
                        break;
                    case EmailPrioridadDTO.Normal:
                        mailMessage.Priority = MailPriority.Normal;
                        break;
                    case EmailPrioridadDTO.Low:
                        mailMessage.Priority = MailPriority.Low;
                        break;
                    default:
                        mailMessage.Priority = MailPriority.Normal;
                        break;
                }
            }
            catch (Exception ex)
            {
            }

            return mailMessage;
        }

        /// <summary>
        /// Expresion para validar el correo electronico
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool EsEmailValido(string email)
        {
            // Retorna true si es un email valido.
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        
    }
}
