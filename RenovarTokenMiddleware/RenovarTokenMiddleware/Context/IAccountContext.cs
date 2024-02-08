using RenovarTokenMiddleware.Dto.Account.LoginIn.Request;
using RenovarTokenMiddleware.Dto.Account.LoginIn.Response;
using RenovarTokenMiddleware.Dto.Base;
using RenovarTokenMiddleware.Models.Login;

namespace RenovarTokenMiddleware.Context
{
    public interface IAccountContext
    {
        public LoginModel LoginIn();
        public Task<BaseApiResponse<LoginInResponseDto>> LoginIn(LoginInRequestDto itemRequest);
    }
}
