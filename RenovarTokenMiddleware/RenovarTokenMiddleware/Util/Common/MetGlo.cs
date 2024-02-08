using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RenovarTokenMiddleware.Util.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Creación:       HARTRODT 02/10/2023 - Ruben C.  <br />
    /// Modificación:									<br />
    /// </remarks>
    public class MetGlo : IMetGlo
    {
        #region [Properties]
        #endregion

        #region [Constructor]
        #endregion

        #region [Functions]
        /// <summary>
        /// Funcion que obtiene la fecha de expiracion a partide del token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public DateTime? GetDateTimeExpireToken(string? token)
        {
            DateTime? fechaExpiracion = null;

            //1.Validamos si el token tiene el formato correcto
            var jwtHandler = new JwtSecurityTokenHandler();
            if (jwtHandler.CanReadToken(token))
            {
                //2.Leemos el token
                var securityToken = jwtHandler.ReadToken(token) as JwtSecurityToken;
                if (securityToken != null && securityToken?.ValidTo != null)
                {
                    //3.Obtén la fecha de expiración del token
                    fechaExpiracion = securityToken.ValidTo;
                }
            }
            return fechaExpiracion;
        }

        /// <summary>
        /// Function que encriptar los datos
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(AppSettings.KeyBase64);
                aesAlg.IV = Convert.FromBase64String(AppSettings.IvBase64);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    byte[] encryptedBytes = msEncrypt.ToArray();
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }

        /// <summary>
        /// Funcion que permite desencriptar
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(AppSettings.KeyBase64);
                aesAlg.IV = Convert.FromBase64String(AppSettings.IvBase64);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Funcion que permite obtener el numeo ip de la maquina
        /// </summary>
        /// <returns></returns>
        public string GetClientIpAddress()
        {
            string ipAddress = string.Empty;

            try
            {
                // Obtener todas las interfaces de red disponibles
                NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                foreach (NetworkInterface networkInterface in networkInterfaces)
                {
                    // Filtrar interfaces activas y que no sean loopback
                    if (networkInterface.OperationalStatus == OperationalStatus.Up && !networkInterface.NetworkInterfaceType.Equals(NetworkInterfaceType.Loopback))
                    {
                        // Obtener las propiedades de configuración de la interfaz
                        IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();

                        // Recorrer las direcciones IPv4 asignadas a la interfaz
                        foreach (UnicastIPAddressInformation ipAddressInfo in ipProperties.UnicastAddresses)
                        {
                            if (ipAddressInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                ipAddress = ipAddressInfo.Address.ToString();
                                break; // Salir del bucle cuando se encuentra la primera dirección IPv4
                            }
                        }

                        if (!string.IsNullOrEmpty(ipAddress))
                        {
                            break; // Salir del bucle si se encontró una dirección IPv4
                        }
                    }
                }
            }
            catch
            {

            }
            return ipAddress;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetHostName()
        {
            string hostName = string.Empty;
            hostName = Dns.GetHostName();
            return hostName;
        }

        /// <summary>
        /// Funcion que permote obtener la hora actual.
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetDateTimeNow()
        {
            var response = DateTime.Now;
            var dateTime = response;
            return dateTime;
        }

        /// <summary>
        /// Funcion que permote obtener la hora actual en formato UTC.
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetDateTimeNowUtc()
        {
            var response = DateTime.UtcNow;
            var dateTime = response;
            return dateTime;
        }

        /// <summary>
        /// Funcion que permite decodificar el toke para poder obtener los valores 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ClaimsPrincipal DecodeJwtToken(string? token)
        {
            var claimsPrincipal = new ClaimsPrincipal();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.KeyToken);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
            };

            try
            {
                SecurityToken validatedToken;
                claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
            }
            catch
            {

            }
            return claimsPrincipal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fechaStr"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public DateTime FormatearFecha(string fechaStr)
        {
            CultureInfo cultureInfo = new CultureInfo("es-PE");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            if (DateTime.TryParseExact(fechaStr, "d/M/yyyy H:mm:ss", cultureInfo, DateTimeStyles.None, out DateTime fecha))
            {
                return fecha;
            }
            else
            {
                throw new ArgumentException("La cadena de fecha no es válida.");
            }
        }
        #endregion
    }
}
