using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasisSoa.Api.Jwt
{
    /// <summary>
    /// 要存储的参数
    /// </summary>
    public class TokenModelBeta
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        public string Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 身份
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Organize { get; set; }
        /// <summary>
        /// 令牌类型
        /// </summary>
        public string TokenType { get; set; }
    }
}
