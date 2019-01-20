using System;
using System.Collections.Generic;
using System.Text;

namespace BasisSoa.Common.EncryptionHelper
{
    public static class CreateRandom
    {
        /// <summary>
        /// 生成随机编号
        /// </summary>
        /// <returns></returns>
        public static string CreateNo()
        {
            Random random = new Random();
            string strRandom = random.Next(1000, 10000).ToString(); //生成编号 
            string code = DateTime.Now.ToString("yyyyMMddHHmmss") + strRandom;//形如
            return code;
        }
    }
}
