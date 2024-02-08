namespace RenovarTokenMiddleware.Util
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Creación:       HARTRODT 21/09/2023 - Ruben C.  <br />
    /// Modificación:									<br />
    /// </remarks>
    public class Constant
    {
        /// <summary>
        /// 
        /// </summary>
        public struct ResponseCode
        {
            /// <summary>
            /// Codigo correcto
            /// </summary>
            public const int SuccessCode = 0;

            /// <summary>
            /// Codigo de error
            /// </summary>
            public const int ErrorCode = 2;

            /// <summary>
            /// Codigo de advertencia
            /// </summary>
            public const int WarningCode = 1;

            /// <summary>
            /// Error no controlado
            /// </summary>
            public const int ErrorUnhandledCode = 3;

            /// <summary>
            /// Codigo correcto
            /// </summary>
            public const string ErrorCodeStringUrlRoot = "2";
        }

        /// <summary>
        /// Mensajes de respuesta
        /// </summary>
        public struct ResponseMessage
        {
            public const string SuccessAddMessage = "Se registro correctamente.";
            public const string SuccessGetMessage = "Se consulto correctamente.";
            public const string SuccessDeleteMessage = "Se elimino correctamente.";
            public const string SuccessUpdateMessage = "Se actualizo correctamente.";
            public const string SuccessDefaultMessage = "Se ejecuto correctamente.";

            public const string SuccessMessage = "Se ejecuto correctamente.";
            public const string WarningMessage = "No se pudo ejecutar la consulta.";
            public const string ErrorMessage = "Error al ejecutar la consulta.";
            public const string WarningDeleteMessage = "No se pudo eliminar,intente nuevamente.";
            public const string WarningAddMessage = "No se pudo registrar,intente nuevamente.";
            public const string WarningUpdateMessage = "No se pudo actualizar,intente nuevamente.";

            public const string LoginSuccess = "Se autentico corretamente.";
            public const string LoginWarning = "No se puedo autenticar,usuario y/o contraseña incorrecta.";

            public const string TokenInvalid = "El token es inválido";
            public const string TokenExpire = "El token a expirado.";
            public const string Unauthorized = "El token es inválido y/o esta expirado.";

            public const string NoHayRegistros = "No se encotraron registros.";
            public const string WarningCerrarSesion = "No se pudo cerrar la sesion";
        }

        /// <summary>
        /// 
        /// </summary>
        public struct CustomClaimTypes
        {
            public const string User = "http://schemas.myapp.com/identity/claims/user";
            public const string IdRolUser = "http://schemas.myapp.com/identity/claims/idRolUser";
            public const string RolUser = "http://schemas.myapp.com/identity/claims/rolUser";
            public const string RolUserDescripcion = "http://schemas.myapp.com/identity/claims/rolUserDescripcion";
            public const string DateTimeExpiredToken = "http://schemas.myapp.com/identity/claims/dateTimeExpiredToken";
        }

        /// <summary>
        /// 
        /// </summary>
        public struct Url
        {
            /// <summary>
            /// 
            /// </summary>
            public const string AccountLogin = "/Account/Login";
        }

        public struct Session
        {
            /// <summary>
            /// 
            /// </summary>
            public const string BreadCrumb = "BreadcrumbHartrodtSession";
        }

        /// <summary>
        /// 
        /// </summary>
        public struct Atributo
        {
            public const string TipoEndose = "Tipo de Endose";
            public const string Sector = "Sector";
            public const string NivelComercial = "Nivel Comercial";
            public const string PagoWeb = "Pago Web";
            public const string TipoPago = "Tipo Pago";
            public const string ControlPercepcion = "Control Percepcion";
            public const string EstadoCliente = "Estado Cliente";
            public const string FormatoDamEmail = "Formato DAM Email";
        }

        /// <summary>
        /// 
        /// </summary>
        public struct Prefijo
        {
            public const string TipoEndose = "TIP_END";
            public const string Sector = "PRE_SEC";
            public const string NivelComercial = "NIV_COM";
            public const string PagoWeb = "PAG_WEB";
            public const string TipoPago = "TIP_PAG";
            public const string ControlPercepcion = "CON_PER";
            public const string EstadoCliente = "EST_CLI";
            public const string FormatoDamEmail = "FOR_DM_EMA";
        }

        /// <summary>
        /// 
        /// </summary>
        public struct OrigenCreacion
        {
            public const string Rebecca = "REBECCA";
        }

        /// <summary>
        /// 
        /// </summary>
        public struct Estado
        {
            public const int NoDelete = 0;
            public const int Delete = 1;
            public const int Activo = 1;
            public const int NoAnulado = 0;

            public const int ProcesadoSintad = 1;
            public const int ProcesadoInsertarSintad = 2;
            public const int ProcesadoActualizarSintad = 3;
            public const int ProcesadoEliminarSintad = 4;
            public const int ProcesadoAnularSintad = 5;
        }

        /// <summary>
        /// Tipo de perfil del gestor
        /// </summary>
        public struct Gestor
        {
            public const string Sectorista = "P01";
            public const string Liquidador = "P02";
            public const string Revisor = "P03";
            public const string Coordinador = "P09";
        }

        /// <summary>
        /// Tipo de perfil del gestor
        /// </summary>
        public struct FileName
        {
            public const string ReporteSeguimiento = "Reporte_SeguimientoOrden";
        }

        public struct Extension
        {
            public const string Excel = ".xlsx";
        }

        public struct ContentType
        {
            public const string Excel = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        }

        public struct SucursalHartrodt
        {
            public const int Callao = 2;
        }
    }
}
