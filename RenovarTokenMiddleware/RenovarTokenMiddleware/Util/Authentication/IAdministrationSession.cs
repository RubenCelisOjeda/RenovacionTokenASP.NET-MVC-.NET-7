using RenovarTokenMiddleware.Util.SessionData;

namespace RenovarTokenMiddleware.Util.Authentication
{
    public interface IAdministrationSession
    {
        public T Get<T>(string sessionId);
        public void Add(string sessionId, string value, int timeout);
        public void KillSession(string sessionId, int codigo = 0);
        public void RemoveSession();
        public HartrodtSession GetHartrodtSession();
        public void SetHartrodtSession(HartrodtSession hartrodtSession);
        public string GetCookie();
        public string GetTokenCookie();
        public void SignOutAsync();
    }
}
