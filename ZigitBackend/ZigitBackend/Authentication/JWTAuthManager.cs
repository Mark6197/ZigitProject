using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ZigitBackend.Authentication
{
    public class JwtAuthManager : IJwtAuthManager
    {
        // getter for the _userRefreshTokens ConcurrentDictionary
        // Immutable means that the state of the object cannot be changed after it's created
        private readonly JwtTokenConfig _jwtTokenConfig;//DI for the configuration of the JWT that is registered as singleton
        private readonly byte[] _secret;//The secret key that is used to sign the tokens

        public JwtAuthManager(JwtTokenConfig jwtTokenConfig)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _secret = Encoding.UTF8.GetBytes(jwtTokenConfig.Secret);//Get the secret from the configuration and encode it
        }

        /// <summary>
        /// Generate the JWT access token and the refresh token
        /// </summary>
        /// <param name="username">Name of the user for which to generate the token</param>
        /// <param name="claims">The claims to include in the JWT access token</param>
        /// <param name="now">Current time</param>
        /// <returns>JwtAuthResult containing the JWT access token and the refresh token</returns>
        public JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now)
        {
            bool shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);//checks whether the Audience is not defined in the claims
            var jwtToken = new JwtSecurityToken(
                _jwtTokenConfig.Issuer,//Provide the issuer from the configuration
                shouldAddAudienceClaim ? _jwtTokenConfig.Audience : string.Empty,//If the Audience is not provided in the claims, add audience from configuration else leave it empty, it will be added later
                claims,//the provided claims (might include the audience)
                expires: now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),//set experation time as per the value from configuration
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));//Sign the token with the secret key
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);//Write the token into a string

            return new JwtAuthResult //Return the result containing both JWT token and refresh token
            {
                AccessToken = accessToken,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))//If the token not provided throw exception
                throw new SecurityTokenException("Invalid token");

            var principal = new JwtSecurityTokenHandler()//Read the claims into the claim princiapl
                .ValidateToken(token,                   //Validates the token against the validation parameters
                    new TokenValidationParameters       //The parameters must match those in the startup class    
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _jwtTokenConfig.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(_secret),
                        ValidAudience = _jwtTokenConfig.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    },
                    out var validatedToken);// out SecurityToken
            return (principal, validatedToken as JwtSecurityToken);
        }
    }
}
