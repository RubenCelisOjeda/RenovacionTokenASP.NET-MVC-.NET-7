using RenovarTokenMiddleware.Dto.Account.LoginIn.Request;
using RenovarTokenMiddleware.Dto.Account.LoginIn.Response;
using RenovarTokenMiddleware.Dto.Base;
using RenovarTokenMiddleware.Models.Login;
using RenovarTokenMiddleware.Service;
using RenovarTokenMiddleware.Util.Authentication;
using RenovarTokenMiddleware.Util.Common;
using RenovarTokenMiddleware.Util.SessionData;
using RenovarTokenMiddleware.Util.SessionData.BaseSession;
using System.Security.Claims;
using Constant = RenovarTokenMiddleware.Util.Constant;

namespace RenovarTokenMiddleware.Context
{
    public class AccountContext : IAccountContext
    { 
        #region [Properites]
        private readonly ILogger<AccountContext> _logger;
        private readonly IAccountService _accountService;
        private readonly IAdministrationSession _administrationSession;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMetGlo _metGlo;
        #endregion

        #region [Constructor]
        /// <summary>
        /// 
        /// </summary>
        public AccountContext(ILogger<AccountContext> logger, IAccountService accountService, IAdministrationSession administrationSession, IHttpContextAccessor httpContextAccessor, IMetGlo metGlo)
        {
            _logger = logger;
            _accountService = accountService;
            _administrationSession = administrationSession;
            _httpContextAccessor = httpContextAccessor;
            _metGlo = metGlo;
        }
        #endregion

        #region [Methods]
        public LoginModel LoginIn()
        {
            LoginModel model = new LoginModel();

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemRequest"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse<LoginInResponseDto>> LoginIn(LoginInRequestDto itemRequest)
        {
            BaseApiResponse<LoginInResponseDto> responseApi;

            try
            {
                #region [Login]
                var responseService = await _accountService.LoginIn(itemRequest);
                if (responseService.CodigoResponse != Constant.ResponseCode.SuccessCode)
                    return responseService;
                #endregion

                #region [Get values the token]
                var claimPrincipal = _metGlo.DecodeJwtToken(responseService.Data.Token);
                var clUser = claimPrincipal.FindFirstValue(Constant.CustomClaimTypes.User);
                var clIdRolUser = Convert.ToInt32(claimPrincipal.FindFirstValue(Constant.CustomClaimTypes.IdRolUser));
                var clRolUser = claimPrincipal.FindFirstValue(Constant.CustomClaimTypes.RolUser);
                var clRolUserDescripcion = claimPrincipal.FindFirstValue(Constant.CustomClaimTypes.RolUserDescripcion);
                var clDateTimeExpiredToken = claimPrincipal.FindFirstValue(Constant.CustomClaimTypes.DateTimeExpiredToken);
                #endregion

                #region [Set SecuritySession]
                var securitySession = new SecuritySession()
                {
                    TokenSecurity = responseService.Data.Token,
                    TokenExpirationTime = _metGlo.GetDateTimeExpireToken(responseService.Data.Token),
                    User = clUser,
                    IdRolUser = clIdRolUser,
                    RolUser = clRolUser,
                    SystemCode = "",
                    UrlAppRoot = this.GetUrlAppRoot(_httpContextAccessor),
                    SessionTimeOut = 0,
                    HostName = _metGlo.GetHostName(),
                    IpNumber = _metGlo.GetClientIpAddress()
                };
                #endregion

                #region [Set HartrodtSession]
                var _hartrodtSession = new HartrodtSession();
                _hartrodtSession.UserInfo = securitySession;
                #endregion

                #region [Set Data Session]
                _administrationSession.SetHartrodtSession(_hartrodtSession);
                #endregion

                #region [Response]
                responseApi = new BaseApiResponse<LoginInResponseDto>();
                responseApi = responseService;
                #endregion
            }
            catch (Exception ex)
            {
                responseApi = new BaseApiResponse<LoginInResponseDto>();
                responseApi.CodigoResponse = Constant.ResponseCode.ErrorCode;
                responseApi.Message = ex.Message;

                _logger.LogError(ex.Message);
            }
            return responseApi;
        }
        #endregion

        #region [Functions]
        /// <summary>
        /// /Obtiene la url actual de la web.
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        private string GetUrlAppRoot(IHttpContextAccessor httpContextAccessor)
        {
            string urlAppRoot = string.Empty;

            try
            {
                urlAppRoot = string.Empty;
                string? scheme = httpContextAccessor?.HttpContext?.Request.Scheme;
                var host = httpContextAccessor?.HttpContext?.Request.Host;
                var pathBase = httpContextAccessor?.HttpContext?.Request.PathBase;

                urlAppRoot = $"{scheme}://{host}{pathBase}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return urlAppRoot;
        }
        #endregion
    }
}
