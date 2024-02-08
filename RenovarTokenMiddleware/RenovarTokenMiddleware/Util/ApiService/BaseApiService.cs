using Newtonsoft.Json;
using RenovarTokenMiddleware.Util.Authentication;
using System.Net.Http.Headers;
using System.Text;

namespace RenovarTokenMiddleware.Util.ApiService
{
    public class BaseApiService : IBaseApiService
    {
        #region [Properties]
        /// <summary>
        /// 
        /// </summary>
        private const string _mediaType = "application/json";

        #region [ApiUrl]
        /// <summary>
        /// 
        /// </summary>
        private readonly IAdministrationSession _sessionManager;
        private readonly string token = string.Empty;
        #endregion

        #endregion

        #region [Constructor]
        /// <summary>
        /// 
        /// </summary>
        public BaseApiService(IAdministrationSession sessionManager)
        {
            _sessionManager = sessionManager;
            token = _sessionManager.GetTokenCookie();
        }
        #endregion

        #region [Api Service]
        /// <summary>
        /// Devuelve una Lista Generica con los datos solicitado desde la URL que se especifico
        /// </summary>
        /// <param name="pPath">Ruta interna del servicio</param>
        /// <returns>Retorna los datos de la entidad.</returns>
        public async Task<T> GetAsync<T>(string pPath)
        {
            try
            {
                var responseBody = string.Empty;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));

                    var response = await client.GetAsync(pPath);

                    if (!response.IsSuccessStatusCode || response.IsSuccessStatusCode)
                    {
                        responseBody = await response.Content.ReadAsStringAsync();
                    }
                }

                var list = JsonConvert.DeserializeObject<T>(responseBody);
                return list;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("RestClient.GetAsync error " + ex.Message);
            }
        }

        /// <summary>
        /// Envia los datos de la entidad para ser guardado
        /// </summary>
        /// <param name="pPath">Ruta interna del servicio</param>
        /// <param name="pObj">Objeto de datos</param>
        /// <returns>Retorna mensaje de confirmacion.</returns>
        public async Task<T> PostAsync<T>(string pPath, object pObj)
        {
            try
            {
                var responseBody = string.Empty;
                T item;

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));

                    var postBody = new StringContent(JsonConvert.SerializeObject(pObj), Encoding.UTF8, _mediaType);
                    var response = await client.PostAsync(pPath, postBody);

                    if (!response.IsSuccessStatusCode || response.IsSuccessStatusCode)
                    {
                        responseBody = await response.Content.ReadAsStringAsync();
                    }
                }

                item = JsonConvert.DeserializeObject<T>(responseBody);
                return item;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("RestClient.PostAsync error " + ex.Message);
            }
        }
        #endregion
    }
}
