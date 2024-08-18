using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

[assembly: OwinStartup(typeof(CMS_BackEnd.Auth.Auth))]

namespace CMS_BackEnd.Auth
{
    public class Auth
    {
        public void Configuration(IAppBuilder app)
        {
            var issuer = "yourIssuer";
            var audience = "yourAudience";
            var key = Encoding.ASCII.GetBytes("your-very-secret-key");

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }
            });
        }
    }
}