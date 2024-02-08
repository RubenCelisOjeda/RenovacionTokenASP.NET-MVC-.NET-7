using RenovarTokenMiddleware.Util.SessionData.BaseSession;

namespace RenovarTokenMiddleware.Util.SessionData
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Creación:       HARTRODT 21/09/2023 - Ruben C.  <br />
    /// Modificación:									<br />
    /// </remarks>
    [Serializable]
    public class HartrodtSession
    {
        /// <summary>
        /// Constructor por Defecto de implementación de la clase
        /// </summary>
        public HartrodtSession()
        {
 
        }

        /// <summary>
        /// Informacion necesaria del usuario
        /// </summary>
        public SecuritySession UserInfo { get; set; }
    }
}
