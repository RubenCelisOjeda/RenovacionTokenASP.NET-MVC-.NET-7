using RenovarTokenMiddleware.Dto.Account.LoginIn.Request;
using RenovarTokenMiddleware.Dto.Account.LoginIn.Response;
using RenovarTokenMiddleware.Dto.Base;

namespace RenovarTokenMiddleware.Service
{
    public interface IAccountService
    {
        public Task<BaseApiResponse<LoginInResponseDto>> LoginIn(LoginInRequestDto itemRequest);
    }
}
