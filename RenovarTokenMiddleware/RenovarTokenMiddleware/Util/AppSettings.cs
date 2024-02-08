namespace RenovarTokenMiddleware.Util
{
    /// </summary>
    /// <remarks>
    /// Creación:       HARTRODT 21/09/2023 - Ruben C.  <br />
    /// Modificación:									<br />
    /// </remarks>
    public static class AppSettings
    {
        #region [Properties]
        /// <summary>
        /// 
        /// </summary>
        private static IConfiguration? _configuration;
        public static string? ApiAuth { get; set; } = string.Empty;
        public static string? ApiBase { get; set; } = string.Empty;
        public static string? ApiMantenimiento { get; set; } = string.Empty;
        public static string? ApiOperaciones { get; set; } = string.Empty;
        public static string? ApiReportes { get; set; } = string.Empty;
        public static string? ApiBaseRebecca { get; set; } = string.Empty;
        public static string? ApiServiceRebecca { get; set; } = string.Empty;
        public static string? HartrodtAppRebeccaSession { get; set; } = string.Empty;
        public static string? CookieDomain { get; set; } = string.Empty;
        public static string? CookiePath { get; set; } = string.Empty;
        public static string? CookieHartrodtAppRebecca { get; set; } = string.Empty;
        public static string? KeyBase64 { get; set; } = string.Empty;
        public static string? IvBase64 { get; set; } = string.Empty;
        public static string? KeyToken { get; set; } = string.Empty;
        public static int SystemTimeoutSession { get; set; } = 0;
        public static string? SystemCode { get; set; } = string.Empty;
        public static string? CorsHartrodtAppRebecca { get; set; } = string.Empty;
        public static string? SiteKeyCaptcha { get; set; } = string.Empty;
        public static string? SecretKeyCaptcha { get; set; } = string.Empty;
        public static string? ApiCaptcha { get; set; } = string.Empty;
        public static int AutoTokenRefreshTimeOut { get; set; } = 0;

        #endregion

        #region [Constructor]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;

            LoadApiService();
            LoadApiBase();
            LoadApiExternal();
            LoadSettins();
        }
        #endregion

        #region [Url]

        /// <summary>
        /// 
        /// </summary>
        private static void LoadApiBase()
        {
            ApiBaseRebecca = _configuration?["ApiUrl:ApiBaseRebecca"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static void LoadApiService()
        {
            ApiMantenimiento = _configuration?["ApiServiceRebecca:ApiMantenimiento"];
            ApiOperaciones = _configuration?["ApiServiceRebecca:ApiOperaciones"];
            ApiReportes = _configuration?["ApiServiceRebecca:ApiReportes"];
            ApiBase = _configuration?["ApiServiceRebecca:ApiBase"];
        }

        /// <summary>
        /// 
        /// </summary>
        private static void LoadApiExternal()
        {
            ApiAuth = _configuration?["ApiUrlExternal:ApiAuth"];
        }

        /// <summary>
        /// 
        /// </summary>
        private static void LoadSettins()
        {
            HartrodtAppRebeccaSession = _configuration?["SettingsApp:HartrodtAppRebeccaSession"];

            CookieDomain = _configuration["CookieApp:CookieDomain"];
            CookieHartrodtAppRebecca = _configuration["CookieApp:CookieHartrodtAppRebecca"];
            CookiePath = _configuration["CookieApp:CookiePath"];

            KeyBase64 = _configuration["SettingsApp:KeyBase64"];
            IvBase64 = _configuration["SettingsApp:IvBase64"];

            KeyToken = _configuration["Jwt:key"];

            SystemTimeoutSession = Convert.ToInt32(_configuration["SettingsApp:SystemTimeoutSession"]);
            SystemCode = _configuration["SettingsApp:SystemCode"];
            AutoTokenRefreshTimeOut = Convert.ToInt32(_configuration["SettingsApp:AutoTokenRefreshTimeOut"]);

            CorsHartrodtAppRebecca = _configuration["SettingsApp:CorsHartrodtAppRebecca"];

            SecretKeyCaptcha = _configuration["Google:SecretKeyCaptcha"];
            SiteKeyCaptcha = _configuration["Google:SiteKeyCaptcha"];
            ApiCaptcha = _configuration["Google:ApiCaptcha"];
        }
        #endregion
    }
}
