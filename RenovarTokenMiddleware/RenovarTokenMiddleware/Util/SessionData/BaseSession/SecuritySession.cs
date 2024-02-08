namespace RenovarTokenMiddleware.Util.SessionData.BaseSession
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Creación:       HARTRODT 21/09/2023 - Ruben C.  <br />
    /// Modificación:									<br />
    /// </remarks>
    [Serializable]
    public class SecuritySession
    {
        /// <summary>
        /// Id de Token de Seguridad
        /// </summary>
        public string? TokenSecurity { get; set; }

        /// <summary>
        /// Tiempo de Expiración del Token de Seguridad
        /// </summary>
        public DateTime? TokenExpirationTime { get; set; }

        /// <summary>
        /// Usuario
        /// </summary>
        public string? User { get; set; }

        /// <summary>
        /// Nombre de Usuario
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// ID Perfil de Usuario
        /// </summary>
        public int? IdRolUser { get; set; }

        /// <summary>
        /// Perfil de Usuario
        /// </summary>
        public string? RolUser { get; set; }

        /// <summary>
        /// Código de Sistema
        /// </summary>
        public string? SystemCode { get; set; }

        /// <summary>
        /// Url de la Aplicación Principal
        /// </summary>
        public string? UrlAppRoot { get; set; }

        /// <summary>
        /// Tiempo de Espera de Session
        /// </summary>
        public int SessionTimeOut { get; set; }

        /// <summary>
        /// Nombre de Terminal
        /// </summary>
        public string? HostName { get; set; }

        /// <summary>
        /// Ip de Terminal
        /// </summary>
        public string? IpNumber { get; set; }
    }
}
