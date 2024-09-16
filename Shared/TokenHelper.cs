using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Shared
{
    public static class TokenHelper
    {
        public static string Issuer = "https://localhost:7296/";
        public static string Key = "authenticationkeyforapp12345678666966";
        public static double ExpiryMinutes = 60;
        public static string GetToken(this string userId)
        {
            var claims = new List<Claim>
             {
                 new Claim("UserId", userId?.ToString()),
             };
            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(TokenHelper.Issuer,
              TokenHelper.Issuer,
              claims, null,
              DateTime.Now.AddMinutes(TokenHelper.ExpiryMinutes),
              signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenHelper.Key)), SecurityAlgorithms.HmacSha256)));
        }
        public static TokenValidationParameters GetValidationParameters(double expiryMin, string issuer, string key)
        {
            return new TokenValidationParameters()
            {
                // Clock skew compensates for server time drift.
                // We recommend 5 minutes or less:
                ClockSkew = TimeSpan.FromMinutes(expiryMin),
                RequireSignedTokens = true,
                // Ensure the token hasn't expired:
                RequireExpirationTime = true,
                ValidateLifetime = true,
                // Ensure the token audience matches our audience value (default true):
                ValidateAudience = true,
                // Ensure the token was issued by a trusted authorization server (default true):
                ValidateIssuer = true,

                ValidIssuer = issuer,
                ValidAudience = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
        }
        private static JwtSecurityToken Validate(this string token)
        {
            SecurityToken validatedToken;
            IPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, GetValidationParameters(TokenHelper.ExpiryMinutes, TokenHelper.Issuer, TokenHelper.Key), out validatedToken);
            return (JwtSecurityToken)validatedToken;
        }
        public static UserProfile GetClaim(this string token)
        {
            try
            {
                JwtSecurityToken validToken = token.Validate();
                return token.ReadToken();
            }
            catch (SecurityTokenValidationException)
            {
                throw new UnauthorizedAccessException();
            }
            catch (ArgumentException)
            {
                throw new UnauthorizedAccessException();
            }
        }
        private static UserProfile ReadToken(this string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            return new UserProfile
            {
                Id = securityToken.Payload.Claims?.FirstOrDefault(x => x.Type.Equals("UserId", StringComparison.OrdinalIgnoreCase))?.Value,
            };
        }
        public static bool ValidateToken(this string token)
        {
            try
            {
                JwtSecurityToken validToken = token.Validate();
                return true;
            }
            catch { return false; }


        }
    }
}
