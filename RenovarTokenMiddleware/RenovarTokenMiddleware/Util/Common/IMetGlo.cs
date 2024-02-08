using System.Security.Claims;

namespace RenovarTokenMiddleware.Util.Common
{
    public interface IMetGlo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Creación:       HARTRODT 02/10/2023 - Ruben C.  <br />
        /// Modificación:									<br />
        /// </remarks>
        public string Encrypt(string plainText);
        public string Decrypt(string cipherText);
        public string GetClientIpAddress();
        public string GetHostName();
        public DateTime? GetDateTimeExpireToken(string? token);
        public ClaimsPrincipal DecodeJwtToken(string? token);
        public DateTime FormatearFecha(string fechaStr);
    }
}
