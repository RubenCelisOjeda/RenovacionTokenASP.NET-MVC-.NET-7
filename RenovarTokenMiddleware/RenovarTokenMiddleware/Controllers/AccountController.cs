using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RenovarTokenMiddleware.Context;
using RenovarTokenMiddleware.Dto.Account.LoginIn.Request;
using RenovarTokenMiddleware.Util;
using RenovarTokenMiddleware.Util.Authentication;
using RenovarTokenMiddleware.Util.SessionData.BaseSession;
using System.Reflection.Metadata;
using System.Security.Claims;
using Constant = RenovarTokenMiddleware.Util.Constant;

namespace RenovarTokenMiddleware.Controllers
{
    public class AccountController : Controller
    {
        #region [Properites]
        private readonly IAccountContext _accountContext;
        private readonly IAdministrationSession _administrationSession;
        #endregion

        #region [Constructor]
        public AccountController(IAccountContext accountContext, IAdministrationSession administrationSession)
        {
            _accountContext = accountContext;
            _administrationSession = administrationSession;
        }
        #endregion

        #region [ActionResult]
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            var response = _accountContext.LoginIn();
            return View(response);
        }
        #endregion

        #region [JsonResult]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> LoginIn([FromBody] LoginInRequestDto itemRequest)
        {
            this.RemoveSession();

            var response = await _accountContext.LoginIn(itemRequest);
            if (response.CodigoResponse == Constant.ResponseCode.SuccessCode)
                this.InfoCookieCreate(_administrationSession.GetHartrodtSession().UserInfo);

            return Json(response);
        }
        #endregion

        #region [Functions]
        /// <summary>
        /// Functions que permite crear un cookie y asigar datos
        /// </summary>
        /// <param name="userInfoSession"></param>
        private void InfoCookieCreate(SecuritySession userInfoSession)
        {
            try
            {
                //1.Pasamos los datos obtenidos a formato json
                var userInfoJson = JsonConvert.SerializeObject(userInfoSession);

                //2.Asignar los datos obtenidos dese el api de seguridad
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,userInfoSession.User),
                    new Claim(ClaimTypes.Role,userInfoSession.RolUser),
                    new Claim(ClaimTypes.Hash,userInfoSession.TokenSecurity)
                };

                //3.Agregamos los datos a la cookie.
                var clainIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrincipal = new ClaimsPrincipal(clainIdentity);

                //4.Configurar la authorizacion
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    //ExpiresUtc = userInfoSession.TokenExpirationTime
                };

                //5.Nos autenticamos
                this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, authProperties);
            }
            catch
            {

            }
        }

        /// <summary>
        /// Funcion que limpia la session.
        /// </summary>
        private void RemoveSession()
        {
            //_administrationSession.RemoveSession();
        }
        #endregion
    }
}
