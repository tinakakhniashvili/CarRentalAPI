using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CarRentalApp.Middlewares;

public class JwtMiddleware
{
     private readonly RequestDelegate _next;
     private readonly IConfiguration _configuration;
    
     public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
     {
         _next = next;
         _configuration = configuration;
     }
    
     public async Task Invoke(HttpContext context)
     {
         var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
    
         if (token != null)
             AttachUserToContext(context, token);
    
         await _next(context);
     }
    
     private void AttachUserToContext(HttpContext context, string token)
     {
         try
         {
             var tokenHandler = new JwtSecurityTokenHandler();
             var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
             tokenHandler.ValidateToken(token, new TokenValidationParameters
             {
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(key),
                 ValidateIssuer = false,
                 ValidateAudience = false
             }, out SecurityToken validatedToken);
    
             var jwtToken = (JwtSecurityToken)validatedToken;
             var userId = jwtToken.Claims.First(x => x.Type == "sub").Value;
    
             context.Items["User"] = userId;
         }
         catch
         {
             // Ignore validation failures
         }
    }
}