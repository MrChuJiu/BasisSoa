using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasisSoa.Api.ViewModels.Sys
{
    public class DetailsSysOrganizeDto: EditSysOrganizeDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatorTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatorUserName { get; set; }

    }
}
