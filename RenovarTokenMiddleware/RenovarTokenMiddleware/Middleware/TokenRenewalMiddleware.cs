namespace RenovarTokenMiddleware.Middleware
{
    public class TokenRenewalMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenRenewalMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var accessToken = context.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(accessToken))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token de acceso no proporcionado");
                return;
            }

            //if (TokenHaExpirado(accessToken))
            //{
            //    var refreshToken = context.Request.Cookies["RefreshToken"]; // Obtén el token de actualización de donde sea necesario

            //    if (!string.IsNullOrEmpty(refreshToken))
            //    {
            //        try
            //        {
            //            var newAccessToken = RenovarToken(refreshToken);
            //            context.Request.Headers["Authorization"] = newAccessToken; // Actualiza el token en la solicitud
            //        }
            //        catch (Exception ex)
            //        {
            //            context.Response.StatusCode = 401;
            //            await context.Response.WriteAsync($"Error al renovar el token: {ex.Message}");
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        context.Response.StatusCode = 401;
            //        await context.Response.WriteAsync("Token de acceso expirado y no hay token de actualización disponible");
            //        return;
            //    }
            //}

            await _next(context);
        }

        //private bool TokenHaExpirado(string accessToken)
        //{
        //    // Lógica para verificar la expiración del token
        //    // Devuelve true si ha expirado, false si no ha expirado
        //}

        //private string RenovarToken(string refreshToken)
        //{
        //    // Lógica para enviar una solicitud al servidor de autorización
        //    // con el token de actualización y obtener un nuevo token de acceso
        //    // Devuelve el nuevo token de acceso
        //}
    }
}
