namespace RenovarTokenMiddleware.Util.ApiService
{
    public interface IBaseApiService
    {
        public Task<T> PostAsync<T>(string pPath, object pObj);
    }
}
