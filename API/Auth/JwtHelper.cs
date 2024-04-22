using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;


namespace BookLendApi.API.Auth
{
    public class JwtHelper
    {
        public static string GetCurrentUserIdFromToken(HttpContext httpContext)
    {
        // Obtener el token JWT del encabezado de autorización
        var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token == null)
        {
            return null;
        }

        // Leer el ID del usuario del token JWT
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken == null)
        {
            return null;
        }

        var userId = jwtToken.Claims.First(claim => claim.Type == "userId").Value;
        return userId;
    }
    }
}