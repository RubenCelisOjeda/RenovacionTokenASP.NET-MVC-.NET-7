using RenovarTokenMiddleware.Dto.Account.LoginIn.Request;
using RenovarTokenMiddleware.Dto.Account.LoginIn.Response;
using RenovarTokenMiddleware.Dto.Base;
using RenovarTokenMiddleware.Util.ApiService;
using Constant = RenovarTokenMiddleware.Util.Constant;

namespace RenovarTokenMiddleware.Service
{
    public class AccountService : IAccountService
    {
        #region [Properties]
        private readonly IBaseApiService _baseApiService;
        #endregion

        #region [Constructor]
        public AccountService(IBaseApiService baseApiService)
        {
            _baseApiService = baseApiService;
        }
        #endregion

        #region [Service]
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
                #region [Url Api]
                var ApiUrl = "" + "authUserInt/loginInRbc";
                #endregion

                #region [Response]
                responseApi = await _baseApiService.PostAsync<BaseApiResponse<LoginInResponseDto>>(ApiUrl, itemRequest);
                #endregion
            }
            catch (Exception ex)
            {
                responseApi = new BaseApiResponse<LoginInResponseDto>();
                responseApi.CodigoResponse = Constant.ResponseCode.ErrorCode;
                responseApi.Message = ex.Message;
            }
            return responseApi;
        }
        #endregion
    }
}
