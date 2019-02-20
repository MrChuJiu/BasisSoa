using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BasisSoa.Api.Jwt
{
    public class JwtToken
    {
        /// <summary>
        /// 获取基于JWT的Token
        /// </summary>
        /// <param name="claims">需要在登陆的时候配置</param>
        /// <param name="permissionRequirement">在startup中定义的参数</param>
        /// <returns></returns>
        public static dynamic BuildJwtToken(Claim[] claims, PermissionRequirement permissionRequirement)
        {
            try
            {
                var now = DateTime.Now;
                // 实例化JwtSecurityToken
                var jwt = new JwtSecurityToken(
                    issuer: permissionRequirement.Issuer,
                    audience: permissionRequirement.Audience,
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(permissionRequirement.Expiration),
                    signingCredentials: permissionRequirement.SigningCredentials
                );
                // 生成 Token
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                //打包返回前台
                var responseJson = new
                {
                    success = true,
                    token = encodedJwt,
                    expires_in = permissionRequirement.Expiration.TotalSeconds,
                    token_type = "Bearer"
                };
                return responseJson;
            }
            catch (Exception ex)
            {
                return "";
            }


        }


        /// <summary>
        /// 解析JWT
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModelBeta SerializeJWT(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role = new object();
            object organize = new object();
            object name = new object();
            object tokentype = new object();
            object isAdmin = new object();
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.GroupSid, out organize);
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
                jwtToken.Payload.TryGetValue(ClaimTypes.Name, out name);
                jwtToken.Payload.TryGetValue(ClaimTypes.Gender, out tokentype);
                jwtToken.Payload.TryGetValue(ClaimTypes.Authentication, out isAdmin);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var tm = new TokenModelBeta
            {
                Id = jwtToken.Id,
                Role = role.ToString(),
                Organize = organize.ToString(),
                Name = name.ToString(),
                TokenType = tokentype.ToString(),
                IsAdmin = isAdmin.ToString() == "1" ? true : false
            };
            return tm;
        }

        /// <summary>
        /// 授权解析jwt
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static TokenModelBeta ParsingJwtToken(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
                return null;
            var tokenHeader = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            TokenModelBeta tm = SerializeJWT(tokenHeader);
            return tm;
        }
    }
}
