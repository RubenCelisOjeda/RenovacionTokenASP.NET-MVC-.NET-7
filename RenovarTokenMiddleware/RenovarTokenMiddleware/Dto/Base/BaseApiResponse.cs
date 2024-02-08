namespace RenovarTokenMiddleware.Dto.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class BaseApiResponse<T>
    {
        /// <summary>
        /// Codigo de error
        /// </summary>
        public int CodigoResponse { get; set; }

        /// <summary>
        /// Data de la respuesta
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Mensaje de la respuesta
        /// </summary>
        public string Message { get; set; }
    }
}
