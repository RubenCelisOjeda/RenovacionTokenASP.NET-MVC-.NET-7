using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Security.Claims;
using RenovarTokenMiddleware.Util.SessionData;

namespace RenovarTokenMiddleware.Util.Authentication
{
    public class AdministrationSession
    {
        #region [Properties]
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region [Constructor]
        public AdministrationSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region [Functions]
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public T? Get<T>(string sessionId)
        {
            T? data = default;

            var serializedValue = _httpContextAccessor?.HttpContext?.Session.GetString(sessionId);
            if (serializedValue != null)
            {
                data = JsonConvert.DeserializeObject<T>(serializedValue);
                return data;
            }

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="value"></param>
        /// <param name="timeout"></param>
        public void Add(string sessionId, string value, int timeout)
        {
            _httpContextAccessor?.HttpContext?.Session?.SetString(sessionId, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="codigo"></param>
        public void KillSession(string sessionId, int codigo = 0)
        {
            if (codigo != 0)
                _httpContextAccessor?.HttpContext?.Session?.Clear();
            else
                _httpContextAccessor?.HttpContext?.Session?.Remove(sessionId);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveSession()
        {
            _httpContextAccessor?.HttpContext?.Session?.Remove("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HartrodtSession GetHartrodtSession()
        {
            var hartrodtSession = Get<HartrodtSession>("");
            return hartrodtSession;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hartrodtSession"></param>
        public void SetHartrodtSession(HartrodtSession hartrodtSession)
        {
            int iTimeOut = hartrodtSession.UserInfo.SessionTimeOut;
            string jsonData = JsonConvert.SerializeObject(hartrodtSession);

            Add("", jsonData, iTimeOut);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetTokenCookie()
        {
            string? tokenCookie = string.Empty;
            var cookieValue = _httpContextAccessor?.HttpContext?.Request.Cookies[""];

            // Crear un claim con el valor de la cookie
            if (!string.IsNullOrEmpty(cookieValue))
            {
                var claimsPrincipal = _httpContextAccessor?.HttpContext?.User;
                tokenCookie = claimsPrincipal?.FindFirstValue(ClaimTypes.Hash);
            }
            return tokenCookie;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string? GetCookie()
        {
            string? cookie = _httpContextAccessor?.HttpContext?.Request?.Cookies[""];
            return cookie;
        }

        /// <summary>
        /// Funcion que permite cerrar session
        /// </summary>
        public void SignOutAsync()
        {
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(0)
            };

            _httpContextAccessor?.HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, authProperties);
            this.RemoveSession();
        }
        #endregion
    }
}
